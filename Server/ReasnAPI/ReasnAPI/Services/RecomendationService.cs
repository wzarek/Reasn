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

namespace ReasnAPI.Services
{
    public class RecomendationService(ReasnContext context,EventService eventService, IConfiguration configuration)
    {
        private readonly string? _connectionString = configuration.GetConnectionString("DefaultValue");

        public async Task<List<EventDto>> GetEventsByInterest(List<UserInterestDto> interestsDto, string username)
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
                    .Where(e => e.Tags.Any(t => tagNames.Contains(t.Name)) && !userEventSlugs.Contains(e.Slug))
                    .Select(e => new
                    {
                        Event = e,
                        TotalTagValue = e.Tags.Where(t => tagNames.Contains(t.Name))
                            .Sum(t => values[tagNames.IndexOf(t.Name)])
                    })
                    .OrderByDescending(e => e.TotalTagValue)
                    .Select(e => e.Event)
                    .ToListAsync();


                var eventDtos = events.Select(e => e.ToDto()).ToList();

                if (eventDtos.Count < 10)
                {
                    int additionalEventsNeeded = 10 - eventDtos.Count;
                    var randomEvents = await context.Events
                        .Include(e => e.Tags)
                        .Include(e => e.Parameters)
                        .Where(e => !events.Contains(e)) 
                        .OrderBy(r => r.StartAt) 
                        .Take(additionalEventsNeeded)
                        .ToListAsync();

                    eventDtos.AddRange(randomEvents.Select(e => e.ToDto()));

                }

                return eventDtos;
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
