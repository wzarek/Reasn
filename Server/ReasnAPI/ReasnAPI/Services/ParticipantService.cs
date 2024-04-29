﻿using ReasnAPI.Models.Database;
using ReasnAPI.Models.DTOs;
using System.Linq.Expressions;

namespace ReasnAPI.Services {
    public class ParticipantService (ReasnContext context) {
        private readonly ReasnContext _context = context;

        public ParticipantDto? CreateParticipant(ParticipantDto? participantDto) {
            if (participantDto is null)
                return null;

            // check if participant already exists (same event and user)
            var participantDb = _context.Participants.FirstOrDefault(r => r.Event.Id == participantDto.EventId && r.User.Id == participantDto.UserId);

            if (participantDb is not null)
                return null;

            var userDb = _context.Users.FirstOrDefault(r => r.Id == participantDto.UserId);
            var eventDb = _context.Events.FirstOrDefault(r => r.Id == participantDto.EventId);
            var statusDb = _context.Statuses.FirstOrDefault(r => r.Id == participantDto.StatusId);

            if (userDb is null || eventDb is null || statusDb is null)
                return null;

            var participant = new Participant() {
                EventId = participantDto.EventId,
                UserId = participantDto.UserId,
                StatusId = participantDto.StatusId
            };

            _context.Participants.Add(participant);
            _context.SaveChanges();

            return MapToParticipantDto(participant);
        }

        public ParticipantDto? UpdateParticipant(int participantId, ParticipantDto? participantDto) {
            if (participantDto is null)
                return null;

            var participant = _context.Participants.FirstOrDefault(r => r.Id == participantId);
            if (participant is null)
                return null;

            var status = _context.Statuses.FirstOrDefault(r => r.Id == participantDto.StatusId);
            if (status is null)
                return null;

            participant.StatusId = participantDto.StatusId;

            _context.SaveChanges();

            return MapToParticipantDto(participant);
        }

        public void DeleteParticipant(int participantId) {
            var participant = _context.Participants.FirstOrDefault(r => r.Id == participantId);

            if (participant is not null) {
                _context.Participants.Remove(participant);
                _context.SaveChanges();
            }
        }

        public ParticipantDto? GetParticipantById(int participantId) {
            return MapToParticipantDto(_context.Participants.FirstOrDefault(r => r.Id == participantId));
        }

        public IEnumerable<ParticipantDto?> GetParticipantsByFilter(Expression<Func<Participant, bool>> filter) {
            return _context.Participants
                           .Where(filter)
                           .Select(participant => MapToParticipantDto(participant))
                           .ToList();
        }

        public IEnumerable<ParticipantDto?> GetAllParticipants() {
            return _context.Participants
                           .Select(participant => MapToParticipantDto(participant))
                           .ToList();
        }

        private static ParticipantDto? MapToParticipantDto(Participant? participant) {
            if (participant is null)
                return null;

            return new ParticipantDto {
                EventId = participant.EventId,
                UserId = participant.UserId,
                StatusId = participant.StatusId
            };
        }
    }
}
