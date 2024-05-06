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
    public class ParameterServiceTests
    {
        [TestMethod]
        public void CreateParameter_ParameterDoesNotExist_ParameterCreated()
        {
            var parameterDto = new ParameterDto
            {
                Key = "TestKey",
                Value = "TestValue"
            };

            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Parameters).ReturnsDbSet(new List<Parameter>());

            var parameterService = new ParameterService(mockContext.Object);
           
            var result = parameterService.CreateParameter(parameterDto);
           
            Assert.AreEqual(parameterDto.Value, result.Value);
        }

        [TestMethod]
        public void CreateParameter_ParameterExists_ParameterNotCreated()
        {
            var parameterDto = new ParameterDto
            {
                Key = "TestKey",
                Value = "TestValue"
            };

            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Parameters).ReturnsDbSet(new List<Parameter> { new Parameter { Key = "TestKey", Value = "TestValue" } });

            var parameterService = new ParameterService(mockContext.Object);
            
            var result = parameterService.CreateParameter(parameterDto);
            
            Assert.IsNull(result);
        }

        [TestMethod]
        public void UpdateParameter_ParameterExists_ParameterUpdated()
        {
            var parameterDto = new ParameterDto
            {
                Key = "TestKey",
                Value = "TestValue"
            };

            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Parameters).ReturnsDbSet(new List<Parameter> { new Parameter { Id = 1, Key = "TestKey", Value = "TestValue" } });
            mockContext.Setup(c => c.EventParameters).ReturnsDbSet(new List<EventParameter>());

            var parameterService = new ParameterService(mockContext.Object);
            
            var result = parameterService.UpdateParameter(1, parameterDto);
            
            Assert.AreEqual(parameterDto.Value, result.Value);
        }

        [TestMethod]
        public void UpdateParameter_ParameterDoesNotExist_NullReturned()
        {
            var parameterDto = new ParameterDto
            {
                Key = "TestKey",
                Value = "TestValue"
            };

            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Parameters).ReturnsDbSet(new List<Parameter>());
            mockContext.Setup(c => c.EventParameters).ReturnsDbSet(new List<EventParameter>());
            var parameterService = new ParameterService(mockContext.Object);
            
            var result = parameterService.UpdateParameter(1, parameterDto);
            
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetParameterById_ParameterDoesNotExist_NullReturned()
        {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Parameters).ReturnsDbSet(new List<Parameter>());

            var parameterService = new ParameterService(mockContext.Object);
            
            var result = parameterService.GetParameterById(1);
            
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetParameterById_ParameterExists_ParameterReturned()
        {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Parameters).ReturnsDbSet(new List<Parameter> { new Parameter { Id = 1, Key = "TestKey", Value = "TestValue" } });

            var parameterService = new ParameterService(mockContext.Object);
            
            var result = parameterService.GetParameterById(1);
            
            Assert.AreEqual("TestValue", result.Value);
        }

        [TestMethod]
        public void DeleteParameter_ParameterExists_ParameterDeleted()
        {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Parameters).ReturnsDbSet(new List<Parameter> { new Parameter { Id = 1, Key = "TestKey", Value = "TestValue" } });
            mockContext.Setup(c => c.EventParameters).ReturnsDbSet(new List<EventParameter>());
            var parameterService = new ParameterService(mockContext.Object);
            
            parameterService.DeleteParameter(1);
            
            mockContext.Verify(c => c.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public void DeleteParameter_ParameterDoesNotExist_NothingDeleted()
        {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Parameters).ReturnsDbSet(new List<Parameter>());
            mockContext.Setup(c => c.EventParameters).ReturnsDbSet(new List<EventParameter>());
            var parameterService = new ParameterService(mockContext.Object);
            
            parameterService.DeleteParameter(1);
            
            mockContext.Verify(c => c.SaveChanges(), Times.Never);
        }

        [TestMethod]
        public void GetParameterByFilter_ParameterExists_ParameterReturned()
        {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Parameters).ReturnsDbSet(new List<Parameter> { new Parameter { Id = 1, Key = "TestKey", Value = "TestValue" } });

            var parameterService = new ParameterService(mockContext.Object);
            
            var result = parameterService.GetParametersByFilter(p => p.Key == "TestKey").ToList();
            
            Assert.AreEqual(1, result.Count());
        }

        [TestMethod]
        public void GetParameterByFilter_ParameterDoesNotExist_NothingReturned()
        {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Parameters).ReturnsDbSet(new List<Parameter>());

            var parameterService = new ParameterService(mockContext.Object);
            
            var result = parameterService.GetParametersByFilter(p => p.Key == "TestKey").ToList();
            
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void GetAllParameters_ParameterExists_ParameterReturned()
        {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Parameters).ReturnsDbSet(new List<Parameter> { new Parameter { Id = 1, Key = "TestKey", Value = "TestValue" } });

            var parameterService = new ParameterService(mockContext.Object);
            
            var result = parameterService.GetAllParameters().ToList();
            
            Assert.AreEqual(1, result.Count());
        }

        [TestMethod]
        public void GetAllParameters_ParameterDoesNotExist_NothingReturned()
        {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Parameters).ReturnsDbSet(new List<Parameter>());

            var parameterService = new ParameterService(mockContext.Object);
            
            var result = parameterService.GetAllParameters().ToList();
            
            Assert.AreEqual(0, result.Count());
        }
        
    }
}
