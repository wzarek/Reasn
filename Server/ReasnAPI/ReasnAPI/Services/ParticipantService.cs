using Microsoft.EntityFrameworkCore;
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

        var eventDb = context.Events.FirstOrDefault(e => e.Slug == participantDto.EventSlug);

        if (eventDb is null)
        {
            throw new NotFoundException("Event not found");
        }

        var userDb = context.Users.FirstOrDefault(u => u.Username == participantDto.Username);

        if (userDb is null)
        {
            throw new NotFoundException("User not found");
        }

        var participantDb = context.Participants.FirstOrDefault(r => r.Event.Id == eventDb.Id && r.User.Id == userDb.Id);

        if (participantDb is not null)
        {
            throw new BadRequestException("Participant already exists");
        }

        context.Participants.Add(new Participant { EventId = eventDb.Id, UserId = userDb.Id, Status = participantDto.Status });
        context.SaveChanges();

        return participantDto;
    }

    public ParticipantDto UpdateParticipant(ParticipantDto participantDto)
    {
        ArgumentNullException.ThrowIfNull(participantDto);

        var eventDb = context.Events.FirstOrDefault(e => e.Slug == participantDto.EventSlug);

        if (eventDb is null)
        {
            throw new NotFoundException("Event not found");
        }

        var userDb = context.Users.FirstOrDefault(u => u.Username == participantDto.Username);

        if (userDb is null)
        {
            throw new NotFoundException("User not found");
        }

        var participant = context.Participants.FirstOrDefault(r => r.Event.Id == eventDb.Id && r.User.Id == userDb.Id);

        if (participant is null)
        {
            throw new NotFoundException("Participant not found");
        }

        participant.Status = participantDto.Status;

        context.Participants.Update(participant);
        context.SaveChanges();

        return participantDto;
    }

    public void DeleteParticipant(int userId, string eventSlug)
    {
        var eventDb = context.Events.FirstOrDefault(e => e.Slug == eventSlug);

        if (eventDb is null)
        {
            throw new NotFoundException("Event not found");
        }

        var participant = context.Participants.FirstOrDefault(r => r.Event.Id == eventDb.Id && r.User.Id == userId);

        if (participant is null)
        {
            throw new NotFoundException("Participant not found");
        }

        context.Participants.Remove(participant);
        context.SaveChanges();
    }

    public IEnumerable<ParticipantDto> GetParticipantsByFilter(Expression<Func<Participant, bool>> filter)
    {
        return context.Participants
                        .Where(filter)
                        .Include(p => p.Event)
                        .Include(p => p.User)
                        .ToDtoList()
                        .AsEnumerable();
    }

    public IEnumerable<ParticipantDto> GetAllParticipants()
    {
        return context.Participants
                        .Include(p => p.Event)
                        .Include(p => p.User)
                        .ToDtoList()
                        .AsEnumerable();
    }
}