using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ReasnAPI.Models.Database;
using ReasnAPI.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ReasnAPI.Mappers;
using ReasnAPI.Models.Recomendation;
using Npgsql;
using ReasnAPI.Models.API;
using ReasnAPI.Models.Enums;

namespace ReasnAPI.Services
{
    public class RecomendationService(ReasnContext context,EventService eventService, ImageService imageService, IConfiguration configuration)
    {
        private readonly string? _connectionString = configuration.GetConnectionString("DefaultValue");

        public async Task<List<EventSugestion>> GetEventsByInterest(List<UserInterestDto> interestsDto, string username, RecomendationPageRequest pageRequest)
        {
            var interests = interestsDto.Select(i => i.Interest.Name).ToList();
            var interestsLevels = interestsDto.Select(i => i.Level).ToList();

            try
            {
                var similarTags = await GetSimilarTagsFromDb(interests);

                var tagNames = similarTags.Select(t => t.Tag_Name).ToList();
                var interestNames = similarTags.Select(t => t.Interest_Name).ToList();
                var values = similarTags.Select(t => t.Value).ToList();

                for (int i = 0; i < values.Count; i++)
                {
                    values[i] *= interestsLevels[interestNames.IndexOf(similarTags[i].Interest_Name)];
                }

                var userEvents = eventService.GetUserEvents(username);
                var userEventsList = userEvents.ToList();
                var userEventTags = userEventsList.SelectMany(e => e.Tags!).Select(t => t.Name).Distinct().ToList();

                for (int i = 0; i < values.Count; i++)
                {
                    if (userEventTags.Contains(tagNames[i]))
                    {
                        values[i] *= 1.25; 
                    }
                }
                var userEventSlugs = userEventsList.Select(e => e.Slug).ToList();
                var events = await context.Events
                    .Include(e => e.Tags)
                    .Include(e => e.Parameters)
                    .Where(e => !userEventSlugs.Contains(e.Slug) && (e.Status == EventStatus.Approved || e.Status == EventStatus.Ongoing))
                    .Select(e => new
                    {
                        Event = e,
                        TotalTagValue = e.Tags.Where(t => tagNames.Contains(t.Name))
                            .Sum(t => values[tagNames.IndexOf(t.Name)])
                    })
                    .OrderByDescending(e => e.TotalTagValue)
                    .Select(e => e.Event)
                    .Skip((pageRequest.Offset))
                    .Take(pageRequest.Limit)
                    .ToListAsync();

                var eventDtos = events.Select(e => e.ToDto()).ToList();
                
                var eventSugestions = new List<EventSugestion>();
                foreach (var eventDto in eventDtos)
                {
                    var participating = eventService.GetEventParticipantsCountBySlugAndStatus(eventDto.Slug, ParticipantStatus.Participating);
                    var interested = eventService.GetEventParticipantsCountBySlugAndStatus(eventDto.Slug, ParticipantStatus.Interested);
                    var participants = new Participants(participating, interested);
                    var images = eventService.GetEventImages(eventDto.Slug);
                    eventSugestions.Add(eventDto.ToSugestion(participants, images));
                }

                return eventSugestions;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred while fetching events: {ex.Message}");
                throw;
            }
        }

        public async Task<List<TagInfo>> GetSimilarTagsFromDb(List<string> interests)
        {
            var similarTags = new List<TagInfo>();

            await using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new NpgsqlCommand();
            cmd.Connection = conn;

    
            var query = @"
                            SELECT DISTINCT tag, interest, simularity
                            FROM common.tag_interest_simularity
                            WHERE interest = any(@interests) AND simularity > 0.3
                        ";

            cmd.CommandText = query;
            cmd.Parameters.AddWithValue("interests", interests.ToArray());

            await using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var tagName = reader.GetString(reader.GetOrdinal("tag"));
                var interestName = reader.GetString(reader.GetOrdinal("interest"));
                var value = reader.GetDecimal(reader.GetOrdinal("simularity"));

                similarTags.Add(new TagInfo
                {
                    Tag_Name = tagName,
                    Interest_Name = interestName,
                    Value = (double)value
                });
            }

            return similarTags;
        }
    }
}
