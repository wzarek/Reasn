﻿using Moq;
using Moq.EntityFrameworkCore;
using ReasnAPI.Exceptions;
using ReasnAPI.Models.Database;
using ReasnAPI.Models.DTOs;
using ReasnAPI.Models.Enums;
using ReasnAPI.Services;
using System.Runtime.Versioning;

namespace ReasnAPI.Tests.Services;

[TestClass]
public class CommentServiceTests
{
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
            Slug = "Slug"
        };

        var comment1 = new Comment
        {
            Id = 1,
            Content = "Content",
            Event = event1,
            User = user
        };

        var comment2 = new Comment
        {
            Id = 2,
            Content = "Content",
            Event = event1,
            User = user
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
            Slug = "Slug"
        };

        var comment1 = new Comment
        {
            Id = 1,
            Content = "Content",
            Event = event1,
            EventId = 1,
            User = user
        };

        var comment2 = new Comment
        {
            Id = 2,
            Content = "Content",
            Event = event1,
            EventId = 1,
            User = user
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
            Slug = "Slug",
        };

        mockContext.Setup(c => c.Users).ReturnsDbSet([user]);
        mockContext.Setup(c => c.Events).ReturnsDbSet([event1]);
        mockContext.Setup(c => c.Comments).ReturnsDbSet([]);

        var commentService = new CommentService(mockContext.Object);

        var commentDto = new CommentDto
        {
            Content = "Content",
            Username = user.Username,
            EventSlug = event1.Slug,
        };

        var result = commentService.CreateComment(commentDto, event1.Id, user.Id);

        Assert.IsNotNull(result);
        Assert.AreEqual("Content", result.Content);
        Assert.AreEqual("Slug", result.EventSlug);
        Assert.AreEqual("Username", result.Username);
    }

    [TestMethod]
    public void CreateComment_CommentDtoIsNull_NullReturned()
    {
        var mockContext = new Mock<ReasnContext>();
        mockContext.Setup(c => c.Comments).ReturnsDbSet([]);

        var commentService = new CommentService(mockContext.Object);

        Assert.ThrowsException<ArgumentNullException>(() => commentService.CreateComment(null, 1, 1));
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

        commentService.DeleteComment(1);

        mockContext.Verify(c => c.SaveChanges(), Times.Once);
    }

    [TestMethod]
    public void DeleteComment_CommentDoesNotExist_NothingHappens()
    {
        var mockContext = new Mock<ReasnContext>();
        mockContext.Setup(c => c.Comments).ReturnsDbSet([]);

        var commentService = new CommentService(mockContext.Object);

        Assert.ThrowsException<NotFoundException>(() => commentService.DeleteComment(1));
    }
}