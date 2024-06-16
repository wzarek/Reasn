using ReasnAPI.Models.DTOs;
using ReasnAPI.Models.Enums;

namespace ReasnAPI.Helpers
{
    public static class EventCreateRequestMapper
    {
        public static EventDto ToDtoFromRequest(this EventCreateRequest eventCreateRequest, int organizerId)
        {
            return new EventDto()
            {
                Name = eventCreateRequest.Name,
                AddressId = eventCreateRequest.AddressId,
                Description = eventCreateRequest.Description,
                StartAt = eventCreateRequest.StartAt,
                EndAt = eventCreateRequest.EndAt,
                Tags = eventCreateRequest.Tags,
                Status = EventStatus.WaitingForApproval,
                Parameters = eventCreateRequest.Parameters,
                OrganizerId = organizerId
            };
        }
    }
}
