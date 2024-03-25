using System;

namespace ReasnAPI.Models.DTOs {
    public class ParticipantDto {
        public int EventId { get; set; }
        public int UserId { get; set; }
        public int StatusId { get; set; }
    }
}