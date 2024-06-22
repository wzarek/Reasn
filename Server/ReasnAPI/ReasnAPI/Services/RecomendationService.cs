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
    public class RecomendationService
    {
        private readonly HttpClient httpClient;
        private readonly ReasnContext context;
        private readonly string flaskApiUrl;
        private readonly string _connectionString;

        public RecomendationService(HttpClient httpClient, ReasnContext context, IConfiguration configuration)
        {
            this.httpClient = httpClient;
            this.context = context;
            _connectionString = configuration.GetConnectionString("DefaultValue");
        }

        public async Task<List<EventDto>> GetEventsByInterest(List<UserInterestDto> interestsDto)
        {
            var interests = interestsDto.Select(i => i.Interest.Name).ToList();
            var interestsLevels = interestsDto.Select(i => i.Level).ToList();

            try
            {
                var similarTags = await GetSimilarTagsFromDb(interests);

                if (similarTags == null || similarTags.Count == 0)
                {
                    return new List<EventDto>();
                }

                var tagNames = similarTags.Select(t => t.Tag_Name).ToList();
                var interestNames = similarTags.Select(t => t.Interest_Name).ToList();
                var values = similarTags.Select(t => t.Value).ToList();

                for (int i = 0; i < values.Count; i++)
                {
                    values[i] *= interestsLevels[interestNames.IndexOf(similarTags[i].Interest_Name)];
                }

                var events = await context.Events
                    .Include(e => e.Tags)
                    .Include(e => e.Parameters)
                    .Where(e => e.Tags.Any(t => tagNames.Contains(t.Name)))
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
                            SELECT DISTINCT tag_name, interest_name, value
                            FROM common.related
                            WHERE interest_name = any(@interests) AND value > 0.3
                        ";

            cmd.CommandText = query;
            cmd.Parameters.AddWithValue("interests", interests.ToArray());

            await using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var tag_name = reader.GetString(reader.GetOrdinal("tag_name"));
                var interest_name = reader.GetString(reader.GetOrdinal("interest_name"));
                var value = reader.GetDecimal(reader.GetOrdinal("value"));

                similarTags.Add(new TagInfo
                {
                    Tag_Name = tag_name,
                    Interest_Name = interest_name,
                    Value = (double)value
                });
            }

            return similarTags;
        }
    }
}
