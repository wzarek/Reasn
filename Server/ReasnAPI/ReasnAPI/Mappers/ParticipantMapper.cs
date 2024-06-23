using ReasnAPI.Models.API;
using ReasnAPI.Models.Database;
using ReasnAPI.Models.DTOs;

namespace ReasnAPI.Mappers;
public static class ParticipantMapper
{
    public static ParticipantDto ToDto(this Participant participant)
    {
        return new ParticipantDto
        {
            EventSlug = participant.Event.Slug,
            Username = participant.User.Username,
            Status = participant.Status
        };
    }

    public static List<ParticipantDto> ToDtoList(this IEnumerable<Participant> participants)
    {
        return participants.Select(ToDto).ToList();
    }

    public static ParticipantsResponse ToResponse(this List<ParticipantDto> participating,
        List<ParticipantDto> interested)
    {
        return new ParticipantsResponse
        {
            Participating = participating,
            Interested = interested
        };
    }

}