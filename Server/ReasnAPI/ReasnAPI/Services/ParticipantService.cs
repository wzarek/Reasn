using ReasnAPI.Models.Database;
using ReasnAPI.Models.DTOs;
using System.Linq.Expressions;

namespace ReasnAPI.Services
{
    public class ParticipantService(ReasnContext context)
    {
        private readonly ReasnContext _context = context;

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

            var participant = new Participant
            {
                EventId = participantDto.EventId,
                UserId = participantDto.UserId,
                Status = participantDto.Status
            };

            context.Participants.Add(participant);
            context.SaveChanges();

            return MapToParticipantDto(participant);
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

            return MapToParticipantDto(participant);
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

            return MapToParticipantDto(participant);
        }

        public IEnumerable<ParticipantDto?> GetParticipantsByFilter(Expression<Func<Participant, bool>> filter)
        {
            return context.Participants
                           .Where(filter)
                           .Select(participant => MapToParticipantDto(participant))
                           .AsEnumerable();
        }

        public IEnumerable<ParticipantDto?> GetAllParticipants()
        {
            return context.Participants
                           .Select(participant => MapToParticipantDto(participant))
                           .AsEnumerable();
        }

        private static ParticipantDto MapToParticipantDto(Participant participant)
        {
            return new ParticipantDto
            {
                EventId = participant.EventId,
                UserId = participant.UserId,
                Status = participant.Status
            };
        }
    }
}
