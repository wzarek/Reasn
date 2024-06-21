using ReasnAPI.Models.DTOs;

namespace ReasnAPI.Models.API
{
    public class ParticipantsResponse
    {
        public List<ParticipantDto> Participating { get; set; }
        public List<ParticipantDto> Interested { get; set; }

    }
}
