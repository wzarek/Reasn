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
            EventId = participant.EventId,
            UserId = participant.UserId,
            Status = participant.Status
        };
    }

    public static List<ParticipantDto> ToDtoList(this IEnumerable<Participant> participants)
    {
        return participants.Select(ToDto).ToList();
    }

    public static Participant ToEntity(this ParticipantDto participantDto)
    {
        return new Participant
        {
            EventId = participantDto.EventId,
            UserId = participantDto.UserId,
            Status = participantDto.Status
        };
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