using ReasnAPI.Exceptions;
using ReasnAPI.Mappers;
using ReasnAPI.Models.Database;
using ReasnAPI.Models.DTOs;
using System.Linq.Expressions;

namespace ReasnAPI.Services;

public class ParticipantService(ReasnContext context)
{
    public ParticipantDto CreateParticipant(ParticipantDto participantDto)
    {
        ArgumentNullException.ThrowIfNull(participantDto);

        var participantDb = context.Participants.FirstOrDefault(r => r.Event.Id == participantDto.EventId && r.User.Id == participantDto.UserId);

        if (participantDb is not null)
        {
            throw new BadRequestException("Participant already exists");
        }

        var userInDb = context.Users.FirstOrDefault(r => r.Id == participantDto.UserId);

        if (userInDb is null)
        {
            throw new NotFoundException("User not found");
        }

        var eventInDb = context.Events.FirstOrDefault(r => r.Id == participantDto.EventId);

        if (eventInDb is null)
        {
            throw new NotFoundException("Event not found");
        }

        context.Participants.Add(participantDto.ToEntity());
        context.SaveChanges();

        return participantDto;
    }

    public ParticipantDto UpdateParticipant(int participantId, ParticipantDto participantDto)
    {
        ArgumentNullException.ThrowIfNull(participantDto);

        var participant = context.Participants.FirstOrDefault(r => r.Id == participantId);
        if (participant is null)
        {
            throw new NotFoundException("Participant not found");
        }

        participant.Status = participantDto.Status;

        context.Participants.Update(participant);
        context.SaveChanges();

        return participant.ToDto();
    }

    public void DeleteParticipant(int participantId)
    {
        var participant = context.Participants.FirstOrDefault(r => r.Id == participantId);

        if (participant is null)
        {
            throw new NotFoundException("Participant not found");
        }

        context.Participants.Remove(participant);
        context.SaveChanges();
    }

    public ParticipantDto GetParticipantById(int participantId)
    {
        var participant = context.Participants.Find(participantId);

        if (participant is null)
        {
            throw new NotFoundException("Participant not found");
        }

        return participant.ToDto();
    }

    public IEnumerable<ParticipantDto> GetParticipantsByFilter(Expression<Func<Participant, bool>> filter)
    {
        return context.Participants
                        .Where(filter)
                        .ToDtoList()
                        .AsEnumerable();
    }

    public IEnumerable<ParticipantDto> GetAllParticipants()
    {
        return context.Participants
                        .ToDtoList()
                        .AsEnumerable();
    }
}