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

            var event1 = new Event {
                Id = 1,
                Name = "Event",
                Description = "Description"
            };

            var user = new User {
                Id = 1,
                Username = "User",
                Email = "Email",
                Password = "Password"
            };

            var status = new Status {
                Id = 1,
                Name = "Status"
            };

            var participant = new Participant {
                Id = 1,
                EventId = event1.Id,
                UserId = user.Id,
                StatusId = status.Id
            };

            mockContext.Setup(c => c.Events).ReturnsDbSet([ event1 ]);
            mockContext.Setup(c => c.Users).ReturnsDbSet([ user ]);
            mockContext.Setup(c => c.Statuses).ReturnsDbSet([ status ]);
            mockContext.Setup(c => c.Participants).ReturnsDbSet([ participant ]);

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
            mockContext.Setup(c => c.Participants).ReturnsDbSet([]);

            var participantService = new ParticipantService(mockContext.Object);

            var result = participantService.GetParticipantById(1);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetAllParticipants_ParticipantsExist_ParticipantsReturned() {
            var mockContext = new Mock<ReasnContext>();

            var event1 = new Event {
                Id = 1,
                Name = "Event",
                Description = "Description"
            };

            var user1 = new User {
                Id = 1,
                Username = "User",
                Email = "Email",
                Password = "Password"
            };

            var user2 = new User {
                Id = 2,
                Username = "User",
                Email = "Email",
                Password = "Password"
            };

            var status = new Status {
                Id = 1,
                Name = "Status"
            };

            var participant1 = new Participant {
                Id = 1,
                EventId = event1.Id,
                UserId = user1.Id,
                StatusId = status.Id
            };

            var participant2 = new Participant {
                Id = 2,
                EventId = event1.Id,
                UserId = user2.Id,
                StatusId = status.Id
            };

            mockContext.Setup(c => c.Events).ReturnsDbSet([ event1 ]);
            mockContext.Setup(c => c.Users).ReturnsDbSet([ user1, user2 ]);
            mockContext.Setup(c => c.Statuses).ReturnsDbSet([ status ]);
            mockContext.Setup(c => c.Participants).ReturnsDbSet([ participant1, participant2 ]);

            var participantService = new ParticipantService(mockContext.Object);

            var result = participantService.GetAllParticipants();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public void GetAllParticipants_NoParticipants_EmptyListReturned() {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Participants).ReturnsDbSet([]);

            var participantService = new ParticipantService(mockContext.Object);

            var result = participantService.GetAllParticipants();

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void GetParticipantsByFilter_ParticipantsExist_ParticipantsReturned() {
            var mockContext = new Mock<ReasnContext>();

            var event1 = new Event {
                Id = 1,
                Name = "Event",
                Description = "Description"
            };

            var user1 = new User {
                Id = 1,
                Username = "User",
                Email = "Email",
                Password = "Password"
            };

            var user2 = new User {
                Id = 2,
                Username = "User",
                Email = "Email",
                Password = "Password"
            };

            var status = new Status {
                Id = 1,
                Name = "Status"
            };

            var participant1 = new Participant {
                Id = 1,
                EventId = event1.Id,
                UserId = user1.Id,
                StatusId = status.Id
            };

            var participant2 = new Participant {
                Id = 2,
                EventId = event1.Id,
                UserId = user2.Id,
                StatusId = status.Id
            };

            mockContext.Setup(c => c.Events).ReturnsDbSet([event1]);
            mockContext.Setup(c => c.Users).ReturnsDbSet([user1, user2]);
            mockContext.Setup(c => c.Statuses).ReturnsDbSet([status]);
            mockContext.Setup(c => c.Participants).ReturnsDbSet([participant1, participant2]);

            var participantService = new ParticipantService(mockContext.Object);

            var result = participantService.GetParticipantsByFilter(r => r.EventId == 1);

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public void GetParticipantsByFilter_ParticipantsDoNotExist_EmptyListReturned() {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Participants).ReturnsDbSet([]);

            var participantService = new ParticipantService(mockContext.Object);

            var result = participantService.GetParticipantsByFilter(r => r.EventId == 1);

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void CreateParticipant_ParticipantCreated_ParticipantReturned() {
            var mockContext = new Mock<ReasnContext>();

            var event1 = new Event {
                Id = 1,
                Name = "Event",
                Description = "Description"
            };

            var user = new User {
                Id = 1,
                Username = "User",
                Email = "Email",
                Password = "Password"
            };

            var status = new Status {
                Id = 1,
                Name = "Status"
            };

            mockContext.Setup(c => c.Events).ReturnsDbSet([ event1 ]);
            mockContext.Setup(c => c.Users).ReturnsDbSet([ user ]);
            mockContext.Setup(c => c.Statuses).ReturnsDbSet([ status ]);
            mockContext.Setup(c => c.Participants).ReturnsDbSet([]);

            var participantService = new ParticipantService(mockContext.Object);

            var participantDto = new ParticipantDto {
                EventId = 1,
                UserId = 1,
                StatusId = 1
            };

            var result = participantService.CreateParticipant(participantDto);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.EventId);
            Assert.AreEqual(1, result.UserId);
            Assert.AreEqual(1, result.StatusId);
        }

        [TestMethod]
        public void CreateParticipant_ParticipantDtoIsNull_NullReturned() {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Participants).ReturnsDbSet([]);

            var participantService = new ParticipantService(mockContext.Object);

            var result = participantService.CreateParticipant(null);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void UpdateParticipant_ParticipantExists_ParticipantReturned() {
            var mockContext = new Mock<ReasnContext>();

            var event1 = new Event {
                Id = 1,
                Name = "Event",
                Description = "Description"
            };

            var user = new User {
                Id = 1,
                Username = "User",
                Email = "Email",
                Password = "Password"
            };

            var status1 = new Status {
                Id = 1,
                Name = "Status"
            };

            var status2 = new Status {
                Id = 2,
                Name = "Status"
            };

            var participant = new Participant {
                Id = 1,
                EventId = event1.Id,
                UserId = user.Id,
                StatusId = status1.Id
            };

            mockContext.Setup(c => c.Events).ReturnsDbSet([ event1 ]);
            mockContext.Setup(c => c.Users).ReturnsDbSet([ user ]);
            mockContext.Setup(c => c.Statuses).ReturnsDbSet([ status1, status2 ]);
            mockContext.Setup(c => c.Participants).ReturnsDbSet([ participant ]);

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
            mockContext.Setup(c => c.Participants).ReturnsDbSet([]);

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
            mockContext.Setup(c => c.Participants).ReturnsDbSet([
                new() { Id = 1, EventId = 1, UserId = 1, StatusId = 1 }
            ]);

            var participantService = new ParticipantService(mockContext.Object);

            var result = participantService.UpdateParticipant(1, null);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void DeleteParticipant_ParticipantExists_ParticipantDeleted() {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Participants).ReturnsDbSet([
                new() { Id = 1, EventId = 1, UserId = 1, StatusId = 1 }
            ]);

            var participantService = new ParticipantService(mockContext.Object);

            participantService.DeleteParticipant(1);

            mockContext.Verify(c => c.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public void DeleteParticipant_ParticipantDoesNotExist_NothingDeleted() {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Participants).ReturnsDbSet([]);

            var participantService = new ParticipantService(mockContext.Object);

            participantService.DeleteParticipant(1);

            mockContext.Verify(c => c.SaveChanges(), Times.Never);
        }
    }
}