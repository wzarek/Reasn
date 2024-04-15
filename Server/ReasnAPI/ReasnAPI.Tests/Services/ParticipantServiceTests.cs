using ReasnAPI.Models.Database;
using ReasnAPI.Models.DTOs;
using ReasnAPI.Services;
using Moq;
using Moq.EntityFrameworkCore;

namespace ReasnAPI.Tests.Services {
    [TestClass]
    public class ParticipantServiceTests {
        [TestMethod]
        public void GetParticipantById_ParticipantExists_ParticipantReturned() {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Participants).ReturnsDbSet(new List<Participant> {
                new() { Id = 1, EventId = 1, UserId = 1, StatusId = 1 }
            });

            var participantService = new ParticipantService(mockContext.Object);

            var result = participantService.GetParticipantById(1);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.EventId);
            Assert.AreEqual(1, result.UserId);
            Assert.AreEqual(1, result.StatusId);
        }

        [TestMethod]
        public void GetParticipantById_ParticipantDoesNotExist_NullReturned() {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Participants).ReturnsDbSet(new List<Participant>());

            var participantService = new ParticipantService(mockContext.Object);

            var result = participantService.GetParticipantById(1);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetAllParticipants_ParticipantsExist_ParticipantsReturned() {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Participants).ReturnsDbSet(new List<Participant> {
                new() { Id = 1, EventId = 1, UserId = 1, StatusId = 1 },
                new() { Id = 2, EventId = 2, UserId = 2, StatusId = 2 }
            });

            var participantService = new ParticipantService(mockContext.Object);

            var result = participantService.GetAllParticipants();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public void GetAllParticipants_NoParticipants_EmptyListReturned() {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Participants).ReturnsDbSet(new List<Participant>());

            var participantService = new ParticipantService(mockContext.Object);

            var result = participantService.GetAllParticipants();

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void GetParticipantsByFilter_ParticipantsExist_ParticipantsReturned() {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Participants).ReturnsDbSet(new List<Participant> {
                new() { Id = 1, EventId = 1, UserId = 1, StatusId = 1 },
                new() { Id = 2, EventId = 2, UserId = 2, StatusId = 2 }
            });

            var participantService = new ParticipantService(mockContext.Object);

            var result = participantService.GetParticipantsByFilter(r => r.EventId == 1);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
        }

        [TestMethod]
        public void GetParticipantsByFilter_ParticipantsDoNotExist_EmptyListReturned() {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Participants).ReturnsDbSet(new List<Participant>());

            var participantService = new ParticipantService(mockContext.Object);

            var result = participantService.GetParticipantsByFilter(r => r.EventId == 1);

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void CreateParticipant_ParticipantCreated_ParticipantReturned() {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Participants).ReturnsDbSet(new List<Participant>());

            var participantService = new ParticipantService(mockContext.Object);

            var result = participantService.CreateParticipant(new ParticipantDto {
                EventId = 1,
                UserId = 1,
                StatusId = 1
            });

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.EventId);
            Assert.AreEqual(1, result.UserId);
            Assert.AreEqual(1, result.StatusId);
        }

        [TestMethod]
        public void CreateParticipant_ParticipantDtoIsNull_NullReturned() {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Participants).ReturnsDbSet(new List<Participant>());

            var participantService = new ParticipantService(mockContext.Object);

            var result = participantService.CreateParticipant(null);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void UpdateParticipant_ParticipantExists_ParticipantReturned() {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Participants).ReturnsDbSet(new List<Participant> {
                new() { Id = 1, EventId = 1, UserId = 1, StatusId = 1 }
            });

            var participantService = new ParticipantService(mockContext.Object);

            var result = participantService.UpdateParticipant(1, new ParticipantDto {
                EventId = 2,
                UserId = 2,
                StatusId = 2
            });

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.EventId);
            Assert.AreEqual(1, result.UserId);
            Assert.AreEqual(2, result.StatusId);
        }

        [TestMethod]
        public void UpdateParticipant_ParticipantDoesNotExist_NullReturned() {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Participants).ReturnsDbSet(new List<Participant>());

            var participantService = new ParticipantService(mockContext.Object);

            var result = participantService.UpdateParticipant(1, new ParticipantDto {
                EventId = 2,
                UserId = 2,
                StatusId = 2
            });

            Assert.IsNull(result);
        }

        [TestMethod]
        public void UpdateParticipant_ParticipantDtoIsNull_NullReturned() {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Participants).ReturnsDbSet(new List<Participant> {
                new() { Id = 1, EventId = 1, UserId = 1, StatusId = 1 }
            });

            var participantService = new ParticipantService(mockContext.Object);

            var result = participantService.UpdateParticipant(1, null);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void DeleteParticipant_ParticipantExists_ParticipantDeleted() {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Participants).ReturnsDbSet(new List<Participant> {
                new() { Id = 1, EventId = 1, UserId = 1, StatusId = 1 }
            });

            var participantService = new ParticipantService(mockContext.Object);

            participantService.DeleteParticipant(1);

            mockContext.Verify(c => c.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public void DeleteParticipant_ParticipantDoesNotExist_NothingDeleted() {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Participants).ReturnsDbSet(new List<Participant>());

            var participantService = new ParticipantService(mockContext.Object);

            participantService.DeleteParticipant(1);

            mockContext.Verify(c => c.SaveChanges(), Times.Never);
        }
    }
}