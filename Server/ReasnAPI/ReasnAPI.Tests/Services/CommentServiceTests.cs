using Moq;
using Moq.EntityFrameworkCore;
using ReasnAPI.Models.Database;
using ReasnAPI.Models.DTOs;
using ReasnAPI.Models.Enums;
using ReasnAPI.Services;
using ReasnAPI.Services.Exceptions;

namespace ReasnAPI.Tests.Services;

[TestClass]
public class CommentServiceTests
{
    [TestMethod]
    public void GetCommentById_CommentExist_CommentReturned()
    {
        var mockContext = new Mock<ReasnContext>();

        var event1 = new Event
        {
            Id = 1,
            Name = "Event",
            Description = "Description",
        };

        var user = new User
        {
            Id = 1,
            Username = "Username",
            Email = "Email",
            Password = "Password",
        };

        var comment = new Comment
        {
            Id = 1,
            Content = "Content",
            EventId = event1.Id,
            UserId = user.Id
        };

        var fakeComment = new FakeDbSet<Comment> { comment };
        var fakeEvent = new FakeDbSet<Event> { event1 };
        var fakeUser = new FakeDbSet<User> { user };

        mockContext.Setup(c => c.Users).Returns(fakeUser);
        mockContext.Setup(c => c.Events).Returns(fakeEvent);
        mockContext.Setup(c => c.Comments).Returns(fakeComment);

        var commentService = new CommentService(mockContext.Object);

        var result = commentService.GetCommentById(1);

        Assert.IsNotNull(result);
        Assert.AreEqual("Content", result.Content);
        Assert.AreEqual(1, result.EventId);
        Assert.AreEqual(1, result.UserId);
    }

    [TestMethod]
    public void GetCommentById_CommentDoesNotExist_NullReturned()
    {
        var mockContext = new Mock<ReasnContext>();
        mockContext.Setup(c => c.Comments).ReturnsDbSet([]);

        var commentService = new CommentService(mockContext.Object);

        Assert.ThrowsException<NotFoundException>(() => commentService.GetCommentById(1));
    }

    [TestMethod]
    public void GetAllComments_CommentsExist_CommentsReturned()
    {
        var mockContext = new Mock<ReasnContext>();

        var user = new User
        {
            Id = 1,
            Username = "Username",
            Email = "Email",
            Password = "Password",
        };

        var event1 = new Event
        {
            Id = 1,
            Name = "Event",
            Description = "Description",
        };

        var comment1 = new Comment
        {
            Id = 1,
            Content = "Content",
            EventId = event1.Id,
            UserId = user.Id
        };

        var comment2 = new Comment
        {
            Id = 2,
            Content = "Content",
            EventId = event1.Id,
            UserId = user.Id
        };

        mockContext.Setup(c => c.Users).ReturnsDbSet([user]);
        mockContext.Setup(c => c.Events).ReturnsDbSet([event1]);
        mockContext.Setup(c => c.Comments).ReturnsDbSet([comment1, comment2]);

        var commentService = new CommentService(mockContext.Object);

        var result = commentService.GetAllComments();

        Assert.IsNotNull(result);
        Assert.AreEqual(2, result.Count());
    }

    [TestMethod]
    public void GetAllComments_NoComments_EmptyListReturned()
    {
        var mockContext = new Mock<ReasnContext>();
        mockContext.Setup(c => c.Comments).ReturnsDbSet([]);

        var commentService = new CommentService(mockContext.Object);

        var result = commentService.GetAllComments();

        Assert.IsNotNull(result);
        Assert.AreEqual(0, result.Count());
    }

    [TestMethod]
    public void GetCommentsByFilter_CommentsExist_CommentsReturned()
    {
        var mockContext = new Mock<ReasnContext>();

        var user = new User
        {
            Id = 1,
            Username = "Username",
            Email = "Email",
            Password = "Password",
        };

        var event1 = new Event
        {
            Id = 1,
            Name = "Event",
            Description = "Description",
        };

        var comment1 = new Comment
        {
            Id = 1,
            Content = "Content",
            EventId = event1.Id,
            UserId = user.Id
        };

        var comment2 = new Comment
        {
            Id = 2,
            Content = "Content",
            EventId = event1.Id,
            UserId = user.Id
        };

        mockContext.Setup(c => c.Users).ReturnsDbSet([user]);
        mockContext.Setup(c => c.Events).ReturnsDbSet([event1]);
        mockContext.Setup(c => c.Comments).ReturnsDbSet([comment1, comment2]);

        var commentService = new CommentService(mockContext.Object);

        var result = commentService.GetCommentsByFilter(r => r.EventId == 1);

        Assert.IsNotNull(result);
        Assert.AreEqual(2, result.Count());
    }

    [TestMethod]
    public void GetCommentsByFilter_NoComments_EmptyListReturned()
    {
        var mockContext = new Mock<ReasnContext>();
        mockContext.Setup(c => c.Comments).ReturnsDbSet([]);

        var commentService = new CommentService(mockContext.Object);

        var result = commentService.GetCommentsByFilter(r => r.EventId == 1);

        Assert.IsNotNull(result);
        Assert.AreEqual(0, result.Count());
    }

    [TestMethod]
    public void CreateComment_CommentCreated_CommentReturned()
    {
        var mockContext = new Mock<ReasnContext>();

        var user = new User
        {
            Id = 1,
            Username = "Username",
            Email = "Email",
            Password = "Password",
        };

        var event1 = new Event
        {
            Id = 1,
            Name = "Event",
            Description = "Description",
        };

        mockContext.Setup(c => c.Users).ReturnsDbSet([user]);
        mockContext.Setup(c => c.Events).ReturnsDbSet([event1]);
        mockContext.Setup(c => c.Comments).ReturnsDbSet([]);

        var commentService = new CommentService(mockContext.Object);

        var commentDto = new CommentDto
        {
            Content = "Content",
            UserId = 1,
            EventId = 1
        };

        var result = commentService.CreateComment(commentDto);

        Assert.IsNotNull(result);
        Assert.AreEqual("Content", result.Content);
        Assert.AreEqual(1, result.EventId);
        Assert.AreEqual(1, result.UserId);
    }

    [TestMethod]
    public void CreateComment_CommentDtoIsNull_NullReturned()
    {
        var mockContext = new Mock<ReasnContext>();
        mockContext.Setup(c => c.Comments).ReturnsDbSet([]);

        var commentService = new CommentService(mockContext.Object);

        Assert.ThrowsException<ArgumentNullException>(() => commentService.CreateComment(null));
    }

    [TestMethod]
    public void UpdateComment_CommentUpdated_CommentReturned()
    {
        var mockContext = new Mock<ReasnContext>();

        var user = new User
        {
            Id = 1,
            Username = "Username",
            Email = "Email",
            Password = "Password",
        };

        var event1 = new Event
        {
            Id = 1,
            Name = "Event",
            Description = "Description",
        };

        var comment = new Comment
        {
            Id = 1,
            Content = "Content",
            EventId = event1.Id,
            UserId = user.Id
        };

        mockContext.Setup(c => c.Users).ReturnsDbSet([user]);
        mockContext.Setup(c => c.Events).ReturnsDbSet([event1]);
        mockContext.Setup(c => c.Comments).ReturnsDbSet([comment]);

        var commentService = new CommentService(mockContext.Object);

        var result = commentService.UpdateComment(1, new CommentDto
        {
            Content = "UpdatedContent",
            EventId = 2,
            UserId = 1
        });

        Assert.IsNotNull(result);
        Assert.AreEqual("UpdatedContent", result.Content);
        Assert.AreEqual(1, result.EventId);
        Assert.AreEqual(1, result.UserId);
    }

    [TestMethod]
    public void UpdateComment_CommentDoesNotExist_NullReturned()
    {
        var mockContext = new Mock<ReasnContext>();
        mockContext.Setup(c => c.Comments).ReturnsDbSet([]);

        var commentService = new CommentService(mockContext.Object);

        Assert.ThrowsException<NotFoundException>(() => commentService.UpdateComment(1, new CommentDto
        {
            Content = "UpdatedContent",
            EventId = 2,
            UserId = 1
        }));
    }

    [TestMethod]
    public void UpdateComment_CommentDtoIsNull_NullReturned()
    {
        var mockContext = new Mock<ReasnContext>();
        mockContext.Setup(c => c.Comments).ReturnsDbSet([]);

        var commentService = new CommentService(mockContext.Object);

        Assert.ThrowsException<ArgumentNullException>(() => commentService.UpdateComment(1, null));
    }

    [TestMethod]
    public void DeleteComment_CommentExists_CommentDeleted()
    {
        var mockContext = new Mock<ReasnContext>();

        var user = new User
        {
            Id = 1,
            Username = "Username",
            Email = "Email",
            Password = "Password"
        };

        var comment = new Comment
        {
            Id = 1,
            Content = "Content",
            EventId = 1,
            UserId = user.Id
        };

        mockContext.Setup(c => c.Users).ReturnsDbSet([user]);
        mockContext.Setup(c => c.Comments).ReturnsDbSet([comment]);

        var commentService = new CommentService(mockContext.Object);

        commentService.DeleteComment(1, 1);

        mockContext.Verify(c => c.SaveChanges(), Times.Once);
    }

    [TestMethod]
    public void DeleteComment_CommentDoesNotExist_NothingHappens()
    {
        var mockContext = new Mock<ReasnContext>();
        mockContext.Setup(c => c.Comments).ReturnsDbSet([]);

        var commentService = new CommentService(mockContext.Object);

        Assert.ThrowsException<NotFoundException>(() => commentService.DeleteComment(1, 1));
    }
}