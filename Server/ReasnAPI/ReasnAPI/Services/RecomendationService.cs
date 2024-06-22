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

namespace ReasnAPI.Services
{
    public class RecomendationService
    {
        private readonly HttpClient httpClient;
        private readonly ReasnContext context;
        private readonly string flaskApiUrl;

        public RecomendationService(HttpClient httpClient, ReasnContext context, IConfiguration configuration)
        {
            this.httpClient = httpClient;
            this.context = context;
            this.flaskApiUrl = $"{configuration.GetValue<string>("FlaskApi:BaseUrl")}/similar-tags";
        }

        public async Task<List<EventDto>> GetEventsByInterest(List<UserInterestDto> interestsDto)
        {
            var interests = interestsDto.Select(i => i.Interest.Name).ToList();
            var interestsLevels = interestsDto.Select(i => i.Level).ToList();

            try
            {
                var response = await httpClient.PostAsJsonAsync(flaskApiUrl, interests);

                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException(
                        $"Error fetching tags from Flask API. Status code: {response.StatusCode}");
                }

                var tagInfoList = await response.Content.ReadFromJsonAsync<List<TagInfo>>();

                if (tagInfoList == null || tagInfoList.Count == 0)
                {
                    return new List<EventDto>();
                }

                var tagNames = tagInfoList.Select(t => t.Tag_Name).ToList();
                var interestNames = tagInfoList.Select(t => t.Interest_Name).ToList();
                var values = tagInfoList.Select(t => t.Value).ToList();

                for (int i = 0; i < values.Count; i++)
                {
                    values[i] *= interestsLevels[interestNames.IndexOf(tagInfoList[i].Interest_Name)];
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
                    .ToListAsync(); ;

                var eventDtos = events.Select(e => e.ToDto()).ToList();

                return eventDtos;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred while fetching events: {ex.Message}");
                throw;
            }
        }
    }
}
