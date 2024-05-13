<<<<<<< HEAD
using ReasnAPI.Models.Enums;

namespace ReasnAPI.Models.DTOs
{
    public class ParticipantDto
=======
using System;

namespace ReasnAPI.Models.DTOs
{
    public class ParticipantDto
>>>>>>> f457d1a (ci: add pre-commit hook to run fmt and lint)
    {
        public int EventId { get; set; }
        public int UserId { get; set; }
        public ParticipantStatus Status { get; set; }
    }
}