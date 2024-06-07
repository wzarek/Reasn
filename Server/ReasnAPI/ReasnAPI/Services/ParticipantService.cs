using ReasnAPI.Mappers;
using ReasnAPI.Models.Database;
using ReasnAPI.Models.DTOs;
using System.Linq.Expressions;

namespace ReasnAPI.Services
{
    public class ParticipantService(ReasnContext context)
    {
        public ParticipantDto? CreateParticipant(ParticipantDto? participantDto)
        {
            if (participantDto is null)
            {
                return null;
            }

            var participantDb = context.Participants.FirstOrDefault(r => r.Event.Id == participantDto.EventId && r.User.Id == participantDto.UserId);

            if (participantDb is not null)
            {
                return null;
            }

            var userInDb = context.Users.FirstOrDefault(r => r.Id == participantDto.UserId);
            var eventInDb = context.Events.FirstOrDefault(r => r.Id == participantDto.EventId);

            if (userInDb is null || eventInDb is null)
            {
                return null;
            }

            context.Participants.Add(participantDto.FromDto());
            context.SaveChanges();

            return participantDto;
        }

        public ParticipantDto? UpdateParticipant(int participantId, ParticipantDto? participantDto)
        {
            if (participantDto is null)
            {
                return null;
            }

            var participant = context.Participants.FirstOrDefault(r => r.Id == participantId);
            if (participant is null)
            {
                return null;
            }

            participant.Status = participantDto.Status;

            context.Participants.Update(participant);
            context.SaveChanges();

            return participant.ToDto();
        }

        public bool DeleteParticipant(int participantId)
        {
            var participant = context.Participants.FirstOrDefault(r => r.Id == participantId);

            if (participant is null)
            {
                return false;
            }

            context.Participants.Remove(participant);
            context.SaveChanges();

            return true;
        }

        public ParticipantDto? GetParticipantById(int participantId)
        {
            var participant = context.Participants.Find(participantId);

            if (participant is null)
            {
                return null;
            }

            return participant.ToDto();
        }

        public IEnumerable<ParticipantDto?> GetParticipantsByFilter(Expression<Func<Participant, bool>> filter)
        {
            return context.Participants
                           .Where(filter)
                            .ToDtoList()
                           .AsEnumerable();
        }

        public IEnumerable<ParticipantDto?> GetAllParticipants()
        {
            return context.Participants
                           .ToDtoList()
                           .AsEnumerable();
        }
    }
}
