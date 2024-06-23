using ReasnAPI.Models.DTOs;
using ReasnAPI.Models.Enums;

namespace ReasnAPI.Models.API
{
    public class EventCreateRequest
    {
        public string Name { get; set; } = null!;
        public AddressDto AddressDto { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public List<TagDto>? Tags { get; set; }
        public List<ParameterDto>? Parameters { get; set; }
    }
}
