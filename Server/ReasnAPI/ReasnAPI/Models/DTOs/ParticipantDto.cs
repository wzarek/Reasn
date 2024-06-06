using ReasnAPI.Models.Enums;

namespace ReasnAPI.Models.DTOs
{
    public class ParticipantDto
    {
        public int EventId { get; set; }
        public int UserId { get; set; }
        public ParticipantStatus Status { get; set; }
    }
}