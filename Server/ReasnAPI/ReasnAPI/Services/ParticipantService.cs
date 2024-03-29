using ReasnAPI.Models.Database;
using ReasnAPI.Models.DTOs;
using System.Linq.Expressions;

namespace ReasnAPI.Services {
    public class ParticipantService (ReasnContext context) {
        private readonly ReasnContext _context = context;

        // TODO: add method - get all participants in given event

        public ParticipantDto CreateParticipant(ParticipantDto participantDto) {
            var participant = new Participant() {
                EventId = participantDto.EventId,
                UserId = participantDto.UserId,
                StatusId = participantDto.StatusId
            };

            _context.Participants.Add(participant);
            _context.SaveChanges();

            return MapToParticipantDto(participant);
        }

        public ParticipantDto UpdateParticipant(int participantId, ParticipantDto participantDto) {
            var participant = _context.Participants.FirstOrDefault(r => r.Id == participantId);

            if (participant == null) {
                return null;
            }

            participant.StatusId = participantDto.StatusId;

            _context.SaveChanges();

            return MapToParticipantDto(participant);
        }

        public void DeleteParticipant(int participantId) {
            var participant = _context.Participants.FirstOrDefault(r => r.Id == participantId);

            if (participant != null) {
                _context.Participants.Remove(participant);
                _context.SaveChanges();
            }
        }

        public ParticipantDto GetParticipantById(int participantId) {
            return MapToParticipantDto(_context.Participants.FirstOrDefault(r => r.Id == participantId));
        }

        public List<ParticipantDto> GetParticipantsByFilter(Expression<Func<Participant, bool>> filter) {
            return _context.Participants.Where(filter).Select(participant => MapToParticipantDto(participant)).ToList();
        }

        public List<ParticipantDto> GetParticipants() {
            return _context.Participants.Select(participant => MapToParticipantDto(participant)).ToList();
        }

        private static ParticipantDto MapToParticipantDto(Participant participant) {
            return new ParticipantDto {
                EventId = participant.EventId,
                UserId = participant.UserId,
                StatusId = participant.StatusId
            };
        }
    }
}
