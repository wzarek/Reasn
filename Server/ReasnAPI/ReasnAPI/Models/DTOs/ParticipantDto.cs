using ReasnAPI.Models.Enums;

namespace ReasnAPI.Models.DTOs
{
    public class ParticipantDto
    {
        public string EventSlug { get; set; } = null!;
        public string Username { get; set; } = null!;
        public ParticipantStatus Status { get; set; }
    }
}