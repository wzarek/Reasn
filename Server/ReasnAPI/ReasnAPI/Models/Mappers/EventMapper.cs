﻿using ReasnAPI.Models.Database;
using ReasnAPI.Models.DTOs;

namespace ReasnAPI.Models.Mappers
{
    public static class EventMapper
    {
        public static EventDto ToDto(this Event eventToMap)
        {
            var tags = eventToMap.Tags
                .Select(t => new TagDto { Name = t.Name })
                .ToList();

            var parameters = eventToMap.Parameters
                .Select(p => new ParameterDto { Key = p.Key, Value = p.Value })
                .ToList();

            return new EventDto
            {
                Name = eventToMap.Name,
                AddressId = eventToMap.AddressId,
                Description = eventToMap.Description,
                OrganizerId = eventToMap.OrganizerId,
                StartAt = eventToMap.StartAt,
                EndAt = eventToMap.EndAt,
                CreatedAt = eventToMap.CreatedAt,
                UpdatedAt = eventToMap.UpdatedAt,
                Slug = eventToMap.Slug,
                Status = eventToMap.Status,
                Tags = tags,
                Parameters = parameters
            };
        }

        public static List<EventDto> ToDtoList(this IEnumerable<Event> @event)
        {
            return @event.Select(ToDto).ToList();
        }
        public static Event ToEntity(this EventDto eventDto)
        {
            return new Event
            {
                Name = eventDto.Name,
                AddressId = eventDto.AddressId,
                Description = eventDto.Description,
                OrganizerId = eventDto.OrganizerId,
                StartAt = eventDto.StartAt,
                EndAt = eventDto.EndAt,
                Status = eventDto.Status
            };
        }

    }
}
