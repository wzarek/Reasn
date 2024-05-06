using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ReasnAPI.Models.Database;
using ReasnAPI.Models.DTOs;
using ReasnAPI.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Linq.Expressions;
using Moq;
using Moq.EntityFrameworkCore;

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
            
            var result = tagService.CreateTag(tagDto);
            
            Assert.IsNull(result);
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
        public void GetTagById_TagExists_TagReturned()
        {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Tags).ReturnsDbSet(new List<Tag> { new Tag { Id = 1, Name = "TestTag" } });

            var tagService = new TagService(mockContext.Object);
            
            var result = tagService.GetTagById(1);
            
            Assert.AreEqual("TestTag", result.Name);
        }

        [TestMethod]
        public void GetTagById_TagDoesNotExist_NullReturned()
        {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Tags).ReturnsDbSet(new List<Tag>());

            var tagService = new TagService(mockContext.Object);
            
            var result = tagService.GetTagById(1);
            
            Assert.IsNull(result);
        }

        [TestMethod]
        public void UpdateTag_TagExists_TagUpdated()
        {
            var tagDto = new TagDto
            {
                Name = "TestTag1"
            };

            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Tags).ReturnsDbSet(new List<Tag> { new Tag { Id = 1, Name = "TestTag" } });
            mockContext.Setup(c => c.EventTags).ReturnsDbSet(new List<EventTag>());
            var tagService = new TagService(mockContext.Object);
            
            var result = tagService.UpdateTag(1, tagDto);
            
            Assert.AreEqual("TestTag1", result.Name);
        }

        [TestMethod]
        public void UpdateTag_TagDoesNotExist_NullReturned()
        {
            var tagDto = new TagDto
            {
                Name = "TestTag"
            };

            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Tags).ReturnsDbSet(new List<Tag>());
            mockContext.Setup(c => c.EventTags).ReturnsDbSet(new List<EventTag>());
            var tagService = new TagService(mockContext.Object);
            
            var result = tagService.UpdateTag(1, tagDto);
            
            Assert.IsNull(result);
        }

        [TestMethod]
        public void DeleteTag_TagExists_TagDeleted()
        {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Tags).ReturnsDbSet(new List<Tag> { new Tag { Id = 1, Name = "TestTag" } });
            mockContext.Setup(c => c.EventTags).ReturnsDbSet(new List<EventTag>());
            var tagService = new TagService(mockContext.Object);
            
            tagService.DeleteTag(1);
            
            mockContext.Verify(c => c.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public void DeleteTag_TagDoesNotExist_NothingHappens()
        {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Tags).ReturnsDbSet(new List<Tag>());
            mockContext.Setup(c => c.EventTags).ReturnsDbSet(new List<EventTag>());
            var tagService = new TagService(mockContext.Object);
            
            tagService.DeleteTag(1);
            
            mockContext.Verify(c => c.SaveChanges(), Times.Never);
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
