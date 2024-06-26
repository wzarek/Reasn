﻿using ReasnAPI.Models.Database;
using ReasnAPI.Models.DTOs;
using ReasnAPI.Services;
using Moq;
using Moq.EntityFrameworkCore;
using ReasnAPI.Exceptions;

namespace ReasnAPI.Tests.Services
{
    [TestClass]
    public class TagServiceTests
    {
        [TestMethod]
        public void CreateTag_TagDoesNotExist_TagCreated()
        {
            var tagDto = new TagDto
            {
                Name = "TestTag"
            };

            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Tags).ReturnsDbSet(new List<Tag>());

            var tagService = new TagService(mockContext.Object);

            var result = tagService.CreateTag(tagDto);

            Assert.AreEqual(tagDto.Name, result.Name);
        }

        [TestMethod]
        public void CreateTag_TagExists_TagNotCreated()
        {
            var tagDto = new TagDto
            {
                Name = "TestTag"
            };

            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Tags).ReturnsDbSet(new List<Tag> { new Tag { Name = "TestTag" } });

            var tagService = new TagService(mockContext.Object);

            Assert.ThrowsException<BadRequestException>(() => tagService.CreateTag(tagDto));
        }

        [TestMethod]
        public void GetAllTags_TagExists_TagsReturned()
        {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Tags).ReturnsDbSet(new List<Tag> { new Tag { Name = "TestTag" } });

            var tagService = new TagService(mockContext.Object);

            var result = tagService.GetAllTags().ToList();

            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void GetAllTags_TagNotExists_EmptyListReturned()
        {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Tags).ReturnsDbSet(new List<Tag>());

            var tagService = new TagService(mockContext.Object);

            var result = tagService.GetAllTags().ToList();

            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void GetTagById_TagDoesNotExist_NullReturned()
        {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Tags).ReturnsDbSet(new List<Tag>());

            var tagService = new TagService(mockContext.Object);

            Assert.ThrowsException<NotFoundException>(() => tagService.GetTagById(1));
        }

        [TestMethod]
        public void DeleteTag_TagExists_TagDeleted()
        {
            // Arrange
            var tags = new List<Tag> { new Tag { Id = 1, Name = "TestTag" } };
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Tags).ReturnsDbSet(tags);
            mockContext.Setup(c => c.Events).ReturnsDbSet(new List<Event>()); // No events associated with tags

            var tagService = new TagService(mockContext.Object);

            // Act
            tagService.DeleteTag(1);

            // Assert
            mockContext.Verify(c => c.SaveChanges(), Times.Once); // Ensure SaveChanges was called
        }

        [TestMethod]
        public void DeleteTag_TagDoesNotExist_NothingHappens()
        {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Tags).ReturnsDbSet(new List<Tag>()); // No tags in context
            mockContext.Setup(c => c.Events).ReturnsDbSet(new List<Event>()); // No events associated with tags

            var tagService = new TagService(mockContext.Object);

            Assert.ThrowsException<NotFoundException>(() => tagService.DeleteTag(1));
            mockContext.Verify(c => c.SaveChanges(), Times.Never); // Ensure SaveChanges was never called
        }

        [TestMethod]
        public void DeleteTag_TagInUse_NothingHappens()
        {
            var tag = new Tag { Id = 1, Name = "TestTag" };
            var tags = new List<Tag> { tag };
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Tags).ReturnsDbSet(tags);
            mockContext.Setup(c => c.Events).ReturnsDbSet(new List<Event>
    {
        new Event { Tags = new List<Tag> { tag } }
    }); // Tag is associated with an event

            var tagService = new TagService(mockContext.Object);

            Assert.ThrowsException<BadRequestException>(() => tagService.DeleteTag(1));
            Assert.AreEqual(1, tags.Count); // Ensure the tag was not removed
            mockContext.Verify(c => c.SaveChanges(), Times.Never); // Ensure SaveChanges was never called
        }

        [TestMethod]
        public void GetTagsByFilter_TagExist_TagsReturned()
        {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Tags).ReturnsDbSet(new List<Tag> { new Tag { Id = 1, Name = "TestTag" } });

            var tagService = new TagService(mockContext.Object);

            var result = tagService.GetTagsByFilter(t => t.Name == "TestTag").ToList();

            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void GetTagsByFilter_TagsNotExist_TagsNotReturned()
        {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Tags).ReturnsDbSet(new List<Tag> { new Tag { Id = 1, Name = "TestTag" } });

            var tagService = new TagService(mockContext.Object);

            var result = tagService.GetTagsByFilter(t => t.Name == "TestTag1").ToList();

            Assert.AreEqual(0, result.Count);
        }

    }
}
