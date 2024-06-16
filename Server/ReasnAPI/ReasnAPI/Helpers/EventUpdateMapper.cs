using ReasnAPI.Models.DTOs;
using ReasnAPI.Models.Enums;

namespace ReasnAPI.Helpers
{
  
    public static class EventUpdateMapper
    {
        public static EventDto ToDtoFromRequest(this EventUpdateRequest eventCreateRequest)
        {
            return new EventDto()
            {
                Name = eventCreateRequest.Name,
                AddressId = eventCreateRequest.AddressId,
                OrganizerId = eventCreateRequest.OrganizerId,
                Description = eventCreateRequest.Description,
                StartAt = eventCreateRequest.StartAt,
                EndAt = eventCreateRequest.EndAt,
                Tags = eventCreateRequest.Tags,
                Status = eventCreateRequest.Status,
                Parameters = eventCreateRequest.Parameters,
            };
        }
    }

    
}
