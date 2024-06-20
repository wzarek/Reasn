using ReasnAPI.Models.DTOs;

namespace ReasnAPI.Models.Controller
{
    public class ParticipantsResponse
    {
        public List<ParticipantDto> Participating { get; set; }
        public List<ParticipantDto> Interested { get; set; }

    }
}
