using Moq;
using Moq.EntityFrameworkCore;
using ReasnAPI.Exceptions;
using ReasnAPI.Models.Database;
using ReasnAPI.Models.DTOs;
using ReasnAPI.Models.Enums;
using ReasnAPI.Services;

namespace ReasnAPI.Tests.Services;

[TestClass]
public class ParticipantServiceTests
{

    [TestMethod]
    public void GetAllParticipants_ParticipantsExist_ParticipantsReturned()
    {
        var mockContext = new Mock<ReasnContext>();

        var event1 = new Event
        {
            Id = 1,
            Name = "Event",
            Description = "Description"
        };

        var user1 = new User
        {
            Id = 1,
            Username = "User",
            Email = "Email",
            Password = "Password"
        };

        var user2 = new User
        {
            Id = 2,
            Username = "User",
            Email = "Email",
            Password = "Password"
        };

        var participant1 = new Participant
        {
            Id = 1,
            Event = event1,
            User = user1,
            Status = ParticipantStatus.Interested
        };

        var participant2 = new Participant
        {
            Id = 2,
            Event = event1,
            User = user2,
            Status = ParticipantStatus.Participating
        };

        mockContext.Setup(c => c.Events).ReturnsDbSet([event1]);
        mockContext.Setup(c => c.Users).ReturnsDbSet([user1, user2]);
        mockContext.Setup(c => c.Participants).ReturnsDbSet([participant1, participant2]);

        var participantService = new ParticipantService(mockContext.Object);

        var result = participantService.GetAllParticipants();

        Assert.IsNotNull(result);
        Assert.AreEqual(2, result.Count());
    }

    [TestMethod]
    public void GetAllParticipants_NoParticipants_EmptyListReturned()
    {
        var mockContext = new Mock<ReasnContext>();
        mockContext.Setup(c => c.Participants).ReturnsDbSet([]);

        var participantService = new ParticipantService(mockContext.Object);

        var result = participantService.GetAllParticipants();

        Assert.IsNotNull(result);
        Assert.AreEqual(0, result.Count());
    }

    [TestMethod]
    public void GetParticipantsByFilter_ParticipantsExist_ParticipantsReturned()
    {
        var mockContext = new Mock<ReasnContext>();

        var event1 = new Event
        {
            Id = 1,
            Name = "Event",
            Description = "Description"
        };

        var user1 = new User
        {
            Id = 1,
            Username = "User",
            Email = "Email",
            Password = "Password"
        };

        var user2 = new User
        {
            Id = 2,
            Username = "User",
            Email = "Email",
            Password = "Password"
        };

        var participant1 = new Participant
        {
            Id = 1,
            Event = event1,
            EventId = event1.Id,
            User = user1,
            Status = ParticipantStatus.Interested
        };

        var participant2 = new Participant
        {
            Id = 2,
            Event = event1,
            EventId = event1.Id,
            User = user2,
            Status = ParticipantStatus.Participating
        };

        mockContext.Setup(c => c.Events).ReturnsDbSet([event1]);
        mockContext.Setup(c => c.Users).ReturnsDbSet([user1, user2]);
        mockContext.Setup(c => c.Participants).ReturnsDbSet([participant1, participant2]);

        var participantService = new ParticipantService(mockContext.Object);

        var result = participantService.GetParticipantsByFilter(r => r.EventId == 1);

        Assert.IsNotNull(result);
        Assert.AreEqual(2, result.Count());
    }

    [TestMethod]
    public void GetParticipantsByFilter_ParticipantsDoNotExist_EmptyListReturned()
    {
        var mockContext = new Mock<ReasnContext>();
        mockContext.Setup(c => c.Participants).ReturnsDbSet([]);

        var participantService = new ParticipantService(mockContext.Object);

        var result = participantService.GetParticipantsByFilter(r => r.EventId == 1);

        Assert.IsNotNull(result);
        Assert.AreEqual(0, result.Count());
    }

    [TestMethod]
    public void CreateParticipant_ParticipantCreated_ParticipantReturned()
    {
        var mockContext = new Mock<ReasnContext>();

        var event1 = new Event
        {
            Id = 1,
            Name = "Event",
            Description = "Description",
            Slug = "Event-Slug"
        };

        var user = new User
        {
            Id = 1,
            Username = "Username",
            Email = "Email",
            Password = "Password"
        };

        mockContext.Setup(c => c.Events).ReturnsDbSet([event1]);
        mockContext.Setup(c => c.Users).ReturnsDbSet([user]);
        mockContext.Setup(c => c.Participants).ReturnsDbSet([]);

        var participantService = new ParticipantService(mockContext.Object);

        var participantDto = new ParticipantDto
        {
            EventSlug = "Event-Slug",
            Username = "Username",
            Status = ParticipantStatus.Interested
        };

        var result = participantService.CreateUpdateParticipant(participantDto);

        Assert.IsNotNull(result);
        Assert.AreEqual("Event-Slug", result.EventSlug);
        Assert.AreEqual("Username", result.Username);
        Assert.AreEqual(ParticipantStatus.Interested, result.Status);
    }

    [TestMethod]
    public void CreateParticipant_ParticipantDtoIsNull_NullReturned()
    {
        var mockContext = new Mock<ReasnContext>();
        mockContext.Setup(c => c.Participants).ReturnsDbSet([]);

        var participantService = new ParticipantService(mockContext.Object);

        Assert.ThrowsException<ArgumentNullException>(() => participantService.CreateUpdateParticipant(null));
    }

    [TestMethod]
    public void UpdateParticipant_ParticipantExists_ParticipantReturned()
    {
        var mockContext = new Mock<ReasnContext>();

        var event1 = new Event
        {
            Id = 1,
            Name = "Event",
            Description = "Description",
            Slug = "Event-Slug"
        };

        var user = new User
        {
            Id = 1,
            Username = "Username",
            Email = "Email",
            Password = "Password"
        };

        var participant = new Participant
        {
            Id = 1,
            Event = event1,
            User = user,
            Status = ParticipantStatus.Interested
        };

        mockContext.Setup(c => c.Events).ReturnsDbSet([event1]);
        mockContext.Setup(c => c.Users).ReturnsDbSet([user]);
        mockContext.Setup(c => c.Participants).ReturnsDbSet([participant]);

        var participantService = new ParticipantService(mockContext.Object);

        var result = participantService.CreateUpdateParticipant(new ParticipantDto
        {
            EventSlug = "Event-Slug",
            Username = "Username",
            Status = ParticipantStatus.Participating
        });

        Assert.IsNotNull(result);
        Assert.AreEqual("Event-Slug", result.EventSlug);
        Assert.AreEqual("Username", result.Username);
        Assert.AreEqual(ParticipantStatus.Participating, result.Status);
    }

    [TestMethod]
    public void UpdateParticipant_ParticipantDoesNotExist_NullReturned()
    {
        var mockContext = new Mock<ReasnContext>();
        mockContext.Setup(c => c.Events).ReturnsDbSet([]);
        mockContext.Setup(c => c.Users).ReturnsDbSet([]);
        mockContext.Setup(c => c.Participants).ReturnsDbSet([]);

        var participantService = new ParticipantService(mockContext.Object);

        Assert.ThrowsException<NotFoundException>(() => participantService.CreateUpdateParticipant(new ParticipantDto
        {
            EventSlug = "Event-Slug",
            Username = "Username",
            Status = ParticipantStatus.Participating
        }));
    }

    [TestMethod]
    public void UpdateParticipant_ParticipantDtoIsNull_NullReturned()
    {
        var mockContext = new Mock<ReasnContext>();
        mockContext.Setup(c => c.Participants).ReturnsDbSet([
            new() { Id = 1, EventId = 1, UserId = 1, Status = ParticipantStatus.Participating }
        ]);

        var participantService = new ParticipantService(mockContext.Object);

        Assert.ThrowsException<ArgumentNullException>(() => participantService.CreateUpdateParticipant(null));
    }

    [TestMethod]
    public void DeleteParticipant_ParticipantExists_ParticipantDeleted()
    {
        var mockContext = new Mock<ReasnContext>();

        var event1 = new Event
        {
            Id = 1,
            Name = "Event",
            Description = "Description",
            Slug = "Event-Slug"
        };

        var user = new User
        {
            Id = 1,
            Username = "Username",
            Email = "Email",
            Password = "Password"
        };

        var participant = new Participant
        {
            Id = 1,
            Event = event1,
            User = user,
            Status = ParticipantStatus.Participating
        };

        mockContext.Setup(c => c.Participants).ReturnsDbSet([participant]);
        mockContext.Setup(c => c.Events).ReturnsDbSet([event1]);
        mockContext.Setup(c => c.Users).ReturnsDbSet([user]);

        var participantService = new ParticipantService(mockContext.Object);

        participantService.DeleteParticipant(user.Id, event1.Slug);

        mockContext.Verify(c => c.SaveChanges(), Times.Once);
    }

    [TestMethod]
    public void DeleteParticipant_ParticipantDoesNotExist_NothingDeleted()
    {
        var mockContext = new Mock<ReasnContext>();
        mockContext.Setup(c => c.Participants).ReturnsDbSet([]);
        mockContext.Setup(c => c.Events).ReturnsDbSet([]);
        mockContext.Setup(c => c.Users).ReturnsDbSet([]);

        var participantService = new ParticipantService(mockContext.Object);

        Assert.ThrowsException<NotFoundException>(() => participantService.DeleteParticipant(1, "Event-Slug"));
        mockContext.Verify(c => c.SaveChanges(), Times.Never);
    }
}