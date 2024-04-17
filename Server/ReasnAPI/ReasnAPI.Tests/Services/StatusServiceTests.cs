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
    public class StatusServiceTests
    {
        [TestMethod]
        public void CreateStatus_StatusDoesNotExist_StatusCreated()
        {
            var statusDto = new StatusDto
            {
                Name = "TestStatus"
            };

            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Statuses).ReturnsDbSet(new List<Status>());

            var statusService = new StatusService(mockContext.Object);
           
            var result = statusService.CreateStatus(statusDto);
           
            Assert.AreEqual(statusDto.Name, result.Name);
        }

        [TestMethod]
        public void CreateStatus_StatusExists_StatusNotCreated()
        {
            var statusDto = new StatusDto
            {
                Name = "TestStatus"
            };

            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Statuses).ReturnsDbSet(new List<Status> { new Status { Name = "TestStatus" } });

            var statusService = new StatusService(mockContext.Object);
            
            var result = statusService.CreateStatus(statusDto);
            
            Assert.IsNull(result);
        }

        [TestMethod]
        public void UpdateStatus_StatusExists_StatusUpdated()
        {
            var statusDto = new StatusDto
            {
                Name = "TestStatus"
            };

            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Statuses).ReturnsDbSet(new List<Status> { new Status {Id = 1, Name = "TestStatus" } });

            var statusService = new StatusService(mockContext.Object);
            
            var result = statusService.UpdateStatus(1, statusDto);
            
            Assert.AreEqual(statusDto.Name, result.Name);
        }

        [TestMethod]
        public void UpdateStatus_StatusDoesNotExist_StatusNotUpdated()
        {
            var statusDto = new StatusDto
            {
                Name = "TestStatus"
            };

            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Statuses).ReturnsDbSet(new List<Status>());

            var statusService = new StatusService(mockContext.Object);
            
            var result = statusService.UpdateStatus(1, statusDto);
            
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetStatusById_StatusDoesNotExist_NullReturned()
        {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Statuses).ReturnsDbSet(new List<Status>());

            var statusService = new StatusService(mockContext.Object);
            
            var result = statusService.GetStatusById(1);
            
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetStatusById_StatusExists_StatusReturned()
        {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Statuses).ReturnsDbSet(new List<Status> { new Status { Id = 1, Name = "TestStatus" } });

            var statusService = new StatusService(mockContext.Object);
            
            var result = statusService.GetStatusById(1);
            
            Assert.AreEqual("TestStatus", result.Name);
        }

        [TestMethod]
        public void DeleteStatus_StatusDoesNotExist_NothingDeleted()
        {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Statuses).ReturnsDbSet(new List<Status>());

            var statusService = new StatusService(mockContext.Object);
            
            statusService.DeleteStatus(1);
            
            mockContext.Verify(c => c.SaveChanges(), Times.Never());
        }

        [TestMethod]
        public void DeleteStatus_StatusExists_StatusDeleted()
        {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Statuses).ReturnsDbSet(new List<Status> { new Status { Id = 1, Name = "TestStatus" } });

            var statusService = new StatusService(mockContext.Object);
            
            statusService.DeleteStatus(1);
            
            mockContext.Verify(c => c.SaveChanges(), Times.Once());
        }

        [TestMethod]
        public void GetStatusByFilter_StatusExists_StatusReturned()
        {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Statuses).ReturnsDbSet(new List<Status> { new Status { Id = 1, Name = "TestStatus" } });

            var statusService = new StatusService(mockContext.Object);
            
            var result = statusService.GetStatusesByFilter(s => s.Id == 1).ToList();
            
            Assert.AreEqual(1, result.Count());
        }

        [TestMethod]
        public void GetStatusByFilter_StatusDoesNotExist_NothingReturned()
        {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Statuses).ReturnsDbSet(new List<Status>());

            var statusService = new StatusService(mockContext.Object);
            
            var result = statusService.GetStatusesByFilter(s => s.Id == 1).ToList();
            
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void GetAllStatuses_StatusExists_StatusReturned()
        {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Statuses).ReturnsDbSet(new List<Status> { new Status { Id = 1, Name = "TestStatus" } });

            var statusService = new StatusService(mockContext.Object);
            
            var result = statusService.GetAllStatuses().ToList();
            
            Assert.AreEqual(1, result.Count());
        }

        [TestMethod]
        public void GetAllStatuses_StatusDoesNotExist_NothingReturned()
        {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Statuses).ReturnsDbSet(new List<Status>());

            var statusService = new StatusService(mockContext.Object);
            
            var result = statusService.GetAllStatuses().ToList();
            
            Assert.AreEqual(0, result.Count());
        }

    }
}
