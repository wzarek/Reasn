using ReasnAPI.Models.Enums;

namespace ReasnAPI.Models.DTOs
{
    public class EventDto
    {
        public string Name { get; set; } = null!;
        public int AddressId { get; set; }
        public string Description { get; set; } = null!;
        public int OrganizerId { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Slug { get; set; } = null!;
        public EventStatus Status { get; set; }
        public List<TagDto>? Tags { get; set; }
        public List<ParameterDto>? Parameters { get; set; }
    }
}