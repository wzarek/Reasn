using ReasnAPI.Models.Database;
using ReasnAPI.Models.DTOs;
using ReasnAPI.Services;
using Moq;
using Moq.EntityFrameworkCore;

namespace ReasnAPI.Tests.Services {
    [TestClass]
    public class RoleServiceTests {
        [TestMethod]
        public void GetRoleById_RoleExist_RoleReturned() {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Roles).ReturnsDbSet([
                new() { Id = 1, Name = "Admin" }
            ]);

            var roleService = new RoleService(mockContext.Object);

            var result = roleService.GetRoleById(1);

            Assert.IsNotNull(result);
            Assert.AreEqual("Admin", result.Name);
        }

        [TestMethod]
        public void GetRoleById_RoleDoesNotExist_NullReturned() {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Roles).ReturnsDbSet([]);

            var roleService = new RoleService(mockContext.Object);

            var result = roleService.GetRoleById(1);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetAllRoles_RolesExist_RolesReturned() {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Roles).ReturnsDbSet([
                new() { Id = 1, Name = "Admin" },
                new() { Id = 2, Name = "User" }
            ]);

            var roleService = new RoleService(mockContext.Object);

            var result = roleService.GetAllRoles();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public void GetAllRoles_NoRoles_EmptyListReturned() {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Roles).ReturnsDbSet([]);

            var roleService = new RoleService(mockContext.Object);

            var result = roleService.GetAllRoles();

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void GetRolesByFilter_RolesExist_RolesReturned() {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Roles).ReturnsDbSet([
                new() { Id = 1, Name = "Admin" },
                new() { Id = 2, Name = "User" }
            ]);

            var roleService = new RoleService(mockContext.Object);

            var result = roleService.GetRolesByFilter(r => r.Name == "Admin");

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
        }

        [TestMethod]
        public void GetRolesByFilter_NoRoles_EmptyListReturned() {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Roles).ReturnsDbSet([]);

            var roleService = new RoleService(mockContext.Object);

            var result = roleService.GetRolesByFilter(r => r.Name == "Admin");

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void CreateRole_RoleCreated_RoleReturned() {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Roles).ReturnsDbSet([]);

            var roleService = new RoleService(mockContext.Object);

            var result = roleService.CreateRole(new RoleDto { Name = "Admin" });

            Assert.IsNotNull(result);
            Assert.AreEqual("Admin", result.Name);
        }

        [TestMethod]
        public void CreateRole_RoleDtoIsNull_NullReturned() {
            var mockContext = new Mock<ReasnContext>();

            var roleService = new RoleService(mockContext.Object);

            var result = roleService.CreateRole(null);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void CreateRole_RoleAlreadyExists_NullReturned() {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Roles).ReturnsDbSet([
                new() { Id = 1, Name = "Admin" }
            ]);

            var roleService = new RoleService(mockContext.Object);

            var result = roleService.CreateRole(new RoleDto { Name = "Admin" });

            Assert.IsNull(result);
        }

        [TestMethod]
        public void UpdateRole_RoleUpdated_RoleReturned() {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Roles).ReturnsDbSet([
                new() { Id = 1, Name = "Admin" }
            ]);

            var roleService = new RoleService(mockContext.Object);

            var result = roleService.UpdateRole(1, new RoleDto { Name = "User" });

            Assert.IsNotNull(result);
            Assert.AreEqual("User", result.Name);
        }

        [TestMethod]
        public void UpdateRole_RoleDtoIsNull_NullReturned() {
            var mockContext = new Mock<ReasnContext>();

            var roleService = new RoleService(mockContext.Object);

            var result = roleService.UpdateRole(1, null);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void UpdateRole_RoleDoesNotExist_NullReturned() {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Roles).ReturnsDbSet([]);

            var roleService = new RoleService(mockContext.Object);

            var result = roleService.UpdateRole(1, new RoleDto { Name = "User" });

            Assert.IsNull(result);
        }

        [TestMethod]
        public void DeleteRole_RoleExists_RoleDeleted() {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Roles).ReturnsDbSet([
                new() { Id = 1, Name = "Admin" }
            ]);

            var roleService = new RoleService(mockContext.Object);

            roleService.DeleteRole(1);

            mockContext.Verify(c => c.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public void DeleteRole_RoleDoesNotExist_NothingHappens() {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Roles).ReturnsDbSet([]);

            var roleService = new RoleService(mockContext.Object);

            roleService.DeleteRole(1);

            mockContext.Verify(c => c.SaveChanges(), Times.Never);
        }
    }
}