using ReasnAPI.Models.DTOs;
using ReasnAPI.Models.Enums;

namespace ReasnAPI.Models.API
{
    public class EventResponse
    {
        public string Name { get; set; } = null!;
        public int AddressId { get; set; }
        public string Description { get; set; } = null!;
        public string OrganizerUsername { get; set; }
        public string OrganizerPhoto { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Slug { get; set; } = null!;
        public EventStatus Status { get; set; }
        public List<TagDto>? Tags { get; set; }
        public List<ParameterDto>? Parameters { get; set; }
        public int ParticipatingParticipantsCount { get; set; }
        public int InterestedParticipantsCount { get; set; }
    }

}
