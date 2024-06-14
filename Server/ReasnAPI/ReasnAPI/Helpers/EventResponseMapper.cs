using ReasnAPI.Models.Database;
using ReasnAPI.Models.DTOs;

namespace ReasnAPI.Helpers
{
    public static class EventResponseMapper
    {
        public static EventResponse ToResponse(EventDto eventDto, int participating, int interested)
        {
            return new EventResponse
            {
                Name = eventDto.Name,
                AddressId = eventDto.AddressId,
                Description = eventDto.Description,
                OrganizerId = eventDto.OrganizerId,
                StartAt = eventDto.StartAt,
                EndAt = eventDto.EndAt,
                CreatedAt = eventDto.CreatedAt,
                UpdatedAt = eventDto.UpdatedAt,
                Slug = eventDto.Slug,
                Status = eventDto.Status,
                Tags = eventDto.Tags,
                Parameters = eventDto.Parameters,
                ParticipatingParticipantsCount = participating,
                InterestedParticipantsCount = interested
            };
        }
    }
}
