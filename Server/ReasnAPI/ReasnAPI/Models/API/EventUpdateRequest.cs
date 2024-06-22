using ReasnAPI.Models.DTOs;
using ReasnAPI.Models.Enums;

namespace ReasnAPI.Models.API
{
    public class EventUpdateRequest
    {
        public string Name { get; set; } = null!;
        public int AddressId { get; set; }
        public AddressDto AddressDto { get; set; } = null!;
        public int OrganizerId { get; set; }
        public string Description { get; set; } = null!;
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public EventStatus Status { get; set; }
        public List<TagDto>? Tags { get; set; }
        public List<ParameterDto>? Parameters { get; set; }
    }
}
