using Moq;
using Moq.EntityFrameworkCore;
using ReasnAPI.Models.Database;
using ReasnAPI.Services;

namespace ReasnAPI.Tests.Services
{
    [TestClass]
    public class ObjectTypeServiceTests
    {
        [TestMethod]
        public void GetObjectTypeById_ObjectTypeExist_ObjectTypeReturned()
        {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.ObjectTypes).ReturnsDbSet([
                new() { Id = 1, Name = "Type1" }
            ]);

            var objectTypeService = new ObjectTypeService(mockContext.Object);

            var result = objectTypeService.GetObjectTypeById(1);

            Assert.IsNotNull(result);
            Assert.AreEqual("Type1", result.Name);
        }

        [TestMethod]
        public void GetObjectTypeById_ObjectTypeDoesNotExist_NullReturned()
        {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.ObjectTypes).ReturnsDbSet([]);

            var objectTypeService = new ObjectTypeService(mockContext.Object);

            var result = objectTypeService.GetObjectTypeById(1);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetAllObjectTypes_ObjectTypesExist_ObjectTypesReturned()
        {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.ObjectTypes).ReturnsDbSet([
                new() { Id = 1, Name = "Type1" },
                new() { Id = 2, Name = "Type2" }
            ]);

            var objectTypeService = new ObjectTypeService(mockContext.Object);

            var result = objectTypeService.GetAllObjectTypes();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public void GetAllObjectTypes_NoObjectTypes_EmptyListReturned()
        {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.ObjectTypes).ReturnsDbSet([]);

            var objectTypeService = new ObjectTypeService(mockContext.Object);

            var result = objectTypeService.GetAllObjectTypes();

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void GetObjectTypesByFilter_ObjectTypesExist_ObjectTypesReturned()
        {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.ObjectTypes).ReturnsDbSet([
                new() { Id = 1, Name = "Type1" },
                new() { Id = 2, Name = "Type2" }
            ]);

            var objectTypeService = new ObjectTypeService(mockContext.Object);

            var result = objectTypeService.GetObjectTypesByFilter(r => r.Name == "Type1");

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("Type1", result.First().Name);
        }

        [TestMethod]
        public void GetObjectTypesByFilter_NoObjectTypes_EmptyListReturned()
        {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.ObjectTypes).ReturnsDbSet([]);

            var objectTypeService = new ObjectTypeService(mockContext.Object);

            var result = objectTypeService.GetObjectTypesByFilter(r => r.Name == "Type1");

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void CreateObjectType_ObjectTypeCreated_ObjectTypeReturned()
        {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.ObjectTypes).ReturnsDbSet([]);

            var objectTypeService = new ObjectTypeService(mockContext.Object);

            var result = objectTypeService.CreateObjectType(new ObjectType { Name = "Type1" });

            Assert.IsNotNull(result);
            Assert.AreEqual("Type1", result.Name);
        }

        [TestMethod]
        public void CreateObjectType_ObjectTypeIsNull_NullReturned()
        {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.ObjectTypes).ReturnsDbSet([]);

            var objectTypeService = new ObjectTypeService(mockContext.Object);

            var result = objectTypeService.CreateObjectType(null);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void CreateObjectType_ObjectTypeWithGivenNameExists_NullReturned()
        {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.ObjectTypes).ReturnsDbSet([
                new() { Id = 1, Name = "Type1" }
            ]);

            var objectTypeService = new ObjectTypeService(mockContext.Object);

            var result = objectTypeService.CreateObjectType(new ObjectType { Name = "Type1" });

            Assert.IsNull(result);
        }

        [TestMethod]
        public void UpdateObjectType_ObjectTypeUpdated_ObjectTypeReturned()
        {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.ObjectTypes).ReturnsDbSet([
                new() { Id = 1, Name = "Type1" }
            ]);

            var objectTypeService = new ObjectTypeService(mockContext.Object);

            var result = objectTypeService.UpdateObjectType(new ObjectType { Id = 1, Name = "Type2" });

            Assert.IsNotNull(result);
            Assert.AreEqual("Type2", result.Name);
        }

        [TestMethod]
        public void UpdateObjectType_ObjectTypeIsNull_NullReturned()
        {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.ObjectTypes).ReturnsDbSet([]);

            var objectTypeService = new ObjectTypeService(mockContext.Object);

            var result = objectTypeService.UpdateObjectType(null);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void UpdateObjectType_ObjectTypeDoesNotExist_NullReturned()
        {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.ObjectTypes).ReturnsDbSet([]);

            var objectTypeService = new ObjectTypeService(mockContext.Object);

            var result = objectTypeService.UpdateObjectType(new ObjectType { Id = 1, Name = "Type1" });

            Assert.IsNull(result);
        }

        [TestMethod]
        public void DeleteObjectType_ObjectTypeDeleted()
        {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.ObjectTypes).ReturnsDbSet([
                new() { Id = 1, Name = "Type1" }
            ]);

            var objectTypeService = new ObjectTypeService(mockContext.Object);

            objectTypeService.DeleteObjectType(1);

            mockContext.Verify(c => c.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public void DeleteObjectType_ObjectTypeDoesNotExist_NothingHappens()
        {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.ObjectTypes).ReturnsDbSet([]);

            var objectTypeService = new ObjectTypeService(mockContext.Object);

            objectTypeService.DeleteObjectType(1);

            mockContext.Verify(c => c.SaveChanges(), Times.Never);
        }
    }
}