using ReasnAPI.Models.API;
using ReasnAPI.Models.Database;
using ReasnAPI.Models.DTOs;
using ReasnAPI.Models.Enums;

namespace ReasnAPI.Mappers
{
    public static class EventMapper
    {
        public static EventDto ToDto(this Event eventToMap)
        {
            var tags = eventToMap.Tags.ToDtoList();

            var parameters = eventToMap.Parameters.ToDtoList();

            return new EventDto
            {
                Name = eventToMap.Name,
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
                Description = eventDto.Description,
                OrganizerId = eventDto.OrganizerId,
                StartAt = eventDto.StartAt,
                EndAt = eventDto.EndAt,
                Status = eventDto.Status
            };
        }

        public static EventResponse ToResponse(this EventDto eventDto, Participants participants, string username, string image, AddressDto addressDto, int addressId)
        {
            var organizer = new Organizer(username, image);
            
            return new EventResponse()
            {
                Name = eventDto.Name,
                AddressId = addressId,
                Description = eventDto.Description,
                Organizer = organizer,
                StartAt = eventDto.StartAt,
                EndAt = eventDto.EndAt,
                CreatedAt = eventDto.CreatedAt,
                UpdatedAt = eventDto.UpdatedAt,
                Slug = eventDto.Slug,
                Status = eventDto.Status,
                Tags = eventDto.Tags,
                Parameters = eventDto.Parameters,
                AddressDto = addressDto,
                Participants = participants
            };
        }

        public static EventDto ToDto(this EventUpdateRequest eventCreateRequest)
        {
            return new EventDto()
            {
                Name = eventCreateRequest.Name,
                OrganizerId = eventCreateRequest.OrganizerId,
                Description = eventCreateRequest.Description,
                StartAt = eventCreateRequest.StartAt,
                EndAt = eventCreateRequest.EndAt,
                Tags = eventCreateRequest.Tags,
                Status = eventCreateRequest.Status,
                Parameters = eventCreateRequest.Parameters,
            };
        }
        public static EventDto ToDto(this EventCreateRequest eventCreateRequest, int organizerId)
        {
            return new EventDto()
            {
                Name = eventCreateRequest.Name,
                Description = eventCreateRequest.Description,
                StartAt = eventCreateRequest.StartAt,
                EndAt = eventCreateRequest.EndAt,
                Tags = eventCreateRequest.Tags,
                Status = EventStatus.PendingApproval,
                Parameters = eventCreateRequest.Parameters,
                OrganizerId = organizerId
            };
        }

    }
}
