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
using ReasnAPI.Exceptions;

namespace ReasnAPI.Tests.Services
{
    [TestClass]
    public class InterestServiceTests
    {
        [TestMethod]
        public void CreateInterest_InterestDoesNotExist_InterestCreated()
        {
            var interestDto = new InterestDto
            {
                Name = "TestInterest"
            };

            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Interests).ReturnsDbSet(new List<Interest>());

            var interestService = new InterestService(mockContext.Object);
           
            var result = interestService.CreateInterest(interestDto);
           
            Assert.AreEqual(interestDto.Name, result.Name);
        }

        [TestMethod]
        public void CreateInterest_InterestExists_InterestNotCreated()
        {
            var interestDto = new InterestDto
            {
                Name = "TestInterest"
            };

            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Interests).ReturnsDbSet(new List<Interest> { new Interest { Name = "TestInterest" } });

            var interestService = new InterestService(mockContext.Object);
            
            Assert.ThrowsException<BadRequestException>(() => interestService.CreateInterest(interestDto));
        }

        [TestMethod]
        public void GetAllInterests_InterestExists_InterestsReturned()
        {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Interests).ReturnsDbSet(new List<Interest> { new Interest { Name = "TestInterest" } });

            var interestService = new InterestService(mockContext.Object);

            var result = interestService.GetAllInterests();

            Assert.AreEqual(1, result.Count());
        }

        [TestMethod]
        public void GetAllInterests_InterestDoesNotExists_NothingReturned()
        {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Interests).ReturnsDbSet(new List<Interest>());

            var interestService = new InterestService(mockContext.Object);

            var result = interestService.GetAllInterests();

            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void GetInterestById_InterestDoesNotExist_NullReturned()
        {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Interests).ReturnsDbSet(new List<Interest>());

            var interestService = new InterestService(mockContext.Object);

            Assert.ThrowsException<NotFoundException>(() => interestService.GetInterestById(1));
        }

        //[TestMethod]
        //public void GetInterestById_InterestExists_InterestReturned()
        //{
        //    var mockContext = new Mock<ReasnContext>();
        //    mockContext.Setup(c => c.Interests).ReturnsDbSet(new List<Interest> { new Interest {Id = 1, Name = "TestInterest"} });

        //    var interestService = new InterestService(mockContext.Object);

        //    var result = interestService.GetInterestById(1);

        //    Assert.AreEqual("TestInterest", result.Name);
        //}

        [TestMethod]
        public void UpdateInterest_InterestExists_InterestUpdated()
        {
            var interestDto = new InterestDto
            {
                Name = "TestInterest1"
            };

            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Interests).ReturnsDbSet(new List<Interest> { new Interest { Id = 1, Name = "TestInterest" } });

            var interestService = new InterestService(mockContext.Object);
            
            var result = interestService.UpdateInterest(1, interestDto);
            
            Assert.AreEqual("TestInterest1", result.Name);
        }

        [TestMethod]
        public void UpdateInterest_InterestDoesNotExist_NullReturned()
        {
            var interestDto = new InterestDto
            {
                Name = "TestInterest"
            };

            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Interests).ReturnsDbSet(new List<Interest>());

            var interestService = new InterestService(mockContext.Object);
            
            Assert.ThrowsException<NotFoundException>(() => interestService.UpdateInterest(1, interestDto));
        }

        [TestMethod]
        public void DeleteInterest_InterestExists_InterestDeleted()
        {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Interests).ReturnsDbSet(new List<Interest>
            {
                new Interest { Id = 1, Name = "TestInterest" }
            });
            mockContext.Setup(c => c.UserInterests).ReturnsDbSet(new List<UserInterest>());

            var interestService = new InterestService(mockContext.Object);

            interestService.DeleteInterest(1);

            mockContext.Verify(c => c.Interests.Remove(It.Is<Interest>(i => i.Id == 1)), Times.Once);
            mockContext.Verify(c => c.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public void DeleteInterest_InterestDoesNotExist_NothingDeleted()
        {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Interests).ReturnsDbSet(new List<Interest>());
            mockContext.Setup(c => c.UserInterests).ReturnsDbSet(new List<UserInterest>());

            var interestService = new InterestService(mockContext.Object);

            Assert.ThrowsException<NotFoundException>(() => interestService.DeleteInterest(1));
        }

        [TestMethod]
        public void DeleteInterest_InterestHasUserInterests_NothingDeleted()
        {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Interests).ReturnsDbSet(new List<Interest>
            {
                new Interest { Id = 1, Name = "TestInterest" }
            });
            mockContext.Setup(c => c.UserInterests).ReturnsDbSet(new List<UserInterest>
            {
                new UserInterest { InterestId = 1, UserId = 1 }
            });

            Assert.ThrowsException<BadRequestException>(() => new InterestService(mockContext.Object).DeleteInterest(1));

            mockContext.Verify(c => c.Interests.Remove(It.IsAny<Interest>()), Times.Never);
            mockContext.Verify(c => c.SaveChanges(), Times.Never);
        }

        [TestMethod]
        public void GetInterestsByFilter_InterestExists_InterestReturned()
        {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Interests).ReturnsDbSet(new List<Interest> { new Interest { Name = "TestInterest" } });

            var interestService = new InterestService(mockContext.Object);

            var result = interestService.GetInterestsByFilter(i => i.Name == "TestInterest").ToList();

            Assert.AreEqual(1, result.Count());
         
        }

        [TestMethod]
        public void GetInterestsByFilter_InterestDoesNotExist_NothingReturned()
        {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Interests).ReturnsDbSet(new List<Interest>());

            var interestService = new InterestService(mockContext.Object);

            var result = interestService.GetInterestsByFilter(i => i.Name == "TestInterest").ToList();

            Assert.AreEqual(0, result.Count());
        }

    }
}
