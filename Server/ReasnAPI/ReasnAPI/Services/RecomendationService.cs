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

            try
            {
                var response = await httpClient.PostAsJsonAsync(flaskApiUrl, interests);

                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException(
                        $"Error fetching tags from Flask API. Status code: {response.StatusCode}");
                }

                var tagNames = await response.Content.ReadFromJsonAsync<List<string>>();

                if (tagNames == null || tagNames.Count == 0)
                {
                    return new List<EventDto>();
                }

                var events = await context.Events
                    .Include(e => e.Tags).Include(e => e.Parameters)
                    .Where(e => e.Tags.Any(t => tagNames.Contains(t.Name)))
                    .ToListAsync();

                return events.ToDtoList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred while fetching events: {ex.Message}");
                throw;
            }
        }
    }
}
