using Moq;
using Moq.EntityFrameworkCore;
using ReasnAPI.Models.Database;
using ReasnAPI.Models.DTOs;
using ReasnAPI.Models.Enums;
using ReasnAPI.Services;

namespace ReasnAPI.Tests.Services
{
    [TestClass]
    public class UserServiceTests
    {
        [TestMethod]
        public void GetUserById_UserExist_UserReturned()
        {
            var mockContext = new Mock<ReasnContext>();

            var address = new Address
            {
                Id = 1,
                City = "City",
                Country = "Country",
                Street = "Street",
                State = "State",
                ZipCode = "ZipCode"
            };

            var user = new User
            {
                Id = 1,
                Name = "John",
                Surname = "Doe",
                Username = "Username",
                Email = "Email",
                AddressId = address.Id,
                Role = UserRole.User
            };

            var fakeAddress = new FakeDbSet<Address> { address };
            var fakeUser = new FakeDbSet<User> { user };

            mockContext.Setup(c => c.Addresses).Returns(fakeAddress);
            mockContext.Setup(c => c.Users).Returns(fakeUser);

            var userService = new UserService(mockContext.Object);
            var result = userService.GetUserById(1);

            Assert.IsNotNull(result);
            Assert.AreEqual("John", result.Name);
            Assert.AreEqual("Doe", result.Surname);
            Assert.AreEqual("Username", result.Username);
            Assert.AreEqual("Email", result.Email);
            Assert.AreEqual(1, result.AddressId);
            Assert.AreEqual(UserRole.User, result.Role);
        }

        [TestMethod]
        public void GetUserById_UserDoesNotExist_NullReturned()
        {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Users).ReturnsDbSet([]);

            var userService = new UserService(mockContext.Object);

            var result = userService.GetUserById(1);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetAllUsers_UsersExist_UsersReturned()
        {
            var mockContext = new Mock<ReasnContext>();

            var user1 = new User
            {
                Id = 1,
                Name = "John",
                Surname = "Doe",
                Username = "Username",
                Email = "Email",
                Role = UserRole.User
            };

            var user2 = new User
            {
                Id = 2,
                Name = "Jane",
                Surname = "Doe",
                Username = "Username",
                Email = "Email",
                Role = UserRole.Admin
            };

            mockContext.Setup(c => c.Users).ReturnsDbSet([user1, user2]);

            var userService = new UserService(mockContext.Object);
            var result = userService.GetAllUsers();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public void GetAllUsers_NoUsers_EmptyListReturned()
        {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Users).ReturnsDbSet([]);

            var userService = new UserService(mockContext.Object);

            var result = userService.GetAllUsers();

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void GetUsersByFilter_UsersExist_UsersReturned()
        {
            var mockContext = new Mock<ReasnContext>();

            var user1 = new User
            {
                Id = 1,
                Name = "John",
                Surname = "Doe",
                Username = "Username",
                Email = "Email",
                Role = UserRole.User
            };

            var user2 = new User
            {
                Id = 2,
                Name = "Jane",
                Surname = "Doe",
                Username = "Username",
                Email = "Email",
                Role = UserRole.Admin
            };

            mockContext.Setup(c => c.Users).ReturnsDbSet([user1, user2]);

            var userService = new UserService(mockContext.Object);

            var result = userService.GetUsersByFilter(u => u.Name == "John");

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
        }

        [TestMethod]
        public void GetUsersByFilter_NoUsers_EmptyListReturned()
        {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Users).ReturnsDbSet([]);

            var userService = new UserService(mockContext.Object);

            var result = userService.GetUsersByFilter(u => u.Name == "John");

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void CreateUser_UserCreated_UserReturned()
        {
            var mockContext = new Mock<ReasnContext>();

            var address = new Address
            {
                Id = 1,
                City = "City",
                Country = "Country",
                Street = "Street",
                State = "State",
                ZipCode = "ZipCode"
            };

            mockContext.Setup(c => c.Addresses).ReturnsDbSet([address]);
            mockContext.Setup(c => c.Users).ReturnsDbSet([]);
            mockContext.Setup(c => c.Interests).ReturnsDbSet([]);
            mockContext.Setup(c => c.UserInterests).ReturnsDbSet([]);

            var userService = new UserService(mockContext.Object);

            var userDto = new UserDto
            {
                Name = "John",
                Surname = "Doe",
                Username = "Username",
                Email = "Email",
                Phone = "Phone",
                Role = UserRole.User,
                AddressId = address.Id,
                Interests =
                [
                    new UserInterestDto
                    {
                        Interest = new InterestDto
                        {
                            Name = "Interest"
                        },
                        Level = 1
                    }
                ]
            };

            var result = userService.CreateUser(userDto);

            Assert.IsNotNull(result);
            Assert.AreEqual("John", result.Name);
            Assert.AreEqual("Doe", result.Surname);
            Assert.AreEqual("Username", result.Username);
            Assert.AreEqual("Email", result.Email);
            Assert.AreEqual("Phone", result.Phone);
            Assert.AreEqual(UserRole.User, result.Role);
            Assert.IsNotNull(result.Interests);
            Assert.AreEqual("Interest", result.Interests[0].Interest.Name);
            Assert.AreEqual(1, result.Interests[0].Level);
            Assert.AreEqual(1, result.AddressId);
        }

        [TestMethod]
        public void CreateUser_UserAlreadyExists_NullReturned()
        {
            var mockContext = new Mock<ReasnContext>();

            var user = new User
            {
                Id = 1,
                Name = "John",
                Surname = "Doe",
                Username = "Username",
                Email = "Email"
            };

            mockContext.Setup(c => c.Users).ReturnsDbSet([user]);

            var userService = new UserService(mockContext.Object);

            var userDto = new UserDto
            {
                Name = "John",
                Surname = "Doe",
                Username = "Username",
                Email = "Email",
                Phone = "Phone",
                Role = UserRole.Admin,
                AddressId = 1
            };

            var result = userService.CreateUser(userDto);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void CreateUser_UserDtoIsNull_NullReturned()
        {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Users).ReturnsDbSet([]);

            var userService = new UserService(mockContext.Object);

            var result = userService.CreateUser(null);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void UpdateUser_UserUpdated_UserReturned()
        {
            var mockContext = new Mock<ReasnContext>();

            var address = new Address
            {
                Id = 1,
                City = "City",
                Country = "Country",
                Street = "Street",
                State = "State",
                ZipCode = "ZipCode"
            };

            var interest = new Interest
            {
                Id = 1,
                Name = "Interest"
            };

            var user = new User
            {
                Id = 1,
                Name = "John",
                Surname = "Doe",
                Username = "Username",
                Email = "Email",
                AddressId = address.Id,
                Role = UserRole.User
            };

            var userInterest = new UserInterest
            {
                UserId = 1,
                InterestId = 1,
                Level = 1
            };

            mockContext.Setup(c => c.Addresses).ReturnsDbSet([address]);
            mockContext.Setup(c => c.Interests).ReturnsDbSet([interest]);
            mockContext.Setup(c => c.UserInterests).ReturnsDbSet([userInterest]);
            mockContext.Setup(c => c.Users).ReturnsDbSet([user]);

            var userService = new UserService(mockContext.Object);

            var userDto = new UserDto
            {
                Name = "Jane",
                Surname = "Doe",
                Username = "Username",
                Email = "Email",
                Phone = "Phone",
                AddressId = address.Id,
                Role = UserRole.User,
                Interests =
                [
                    new UserInterestDto
                    {
                        Interest = new InterestDto
                        {
                            Name = "Interest"
                        },
                        Level = 2
                    }
                ]
            };

            var result = userService.UpdateUser(1, userDto);

            Assert.IsNotNull(result);
            Assert.AreEqual("Jane", result.Name);
            Assert.AreEqual("Doe", result.Surname);
            Assert.AreEqual("Username", result.Username);
            Assert.AreEqual("Email", result.Email);
            Assert.AreEqual("Phone", result.Phone);
            Assert.AreEqual(UserRole.User, result.Role);
            Assert.AreEqual(1, result.AddressId);

            // TODO: fix interest update
            //Assert.IsNotNull(result.Interests);
            //Assert.AreEqual("Interest", result.Interests[0].Interest.Name);
            //Assert.AreEqual(2, result.Interests[1].Level);
        }

        [TestMethod]
        public void UpdateUser_UserDtoIsNull_NullReturned()
        {
            var mockContext = new Mock<ReasnContext>();

            var user = new User
            {
                Id = 1,
                Name = "John",
                Surname = "Doe",
                Username = "Username",
                Email = "Email"
            };

            mockContext.Setup(c => c.Users).ReturnsDbSet([user]);

            var userService = new UserService(mockContext.Object);

            var result = userService.UpdateUser(1, null);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void UpdateUser_UserDoesNotExist_NullReturned()
        {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Users).ReturnsDbSet([]);

            var userService = new UserService(mockContext.Object);

            var userDto = new UserDto
            {
                Name = "Jane",
                Surname = "Doe",
                Username = "Username",
                Email = "Email",
                Phone = "Phone",
                Role = UserRole.User,
                AddressId = 1
            };

            var result = userService.UpdateUser(1, userDto);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void DeleteUser_UserExists_UserDeleted()
        {
            var mockContext = new Mock<ReasnContext>();

            var user = new User
            {
                Id = 1,
                Name = "John",
                Surname = "Doe",
                Username = "Username",
                Email = "Email"
            };

            mockContext.Setup(c => c.Users).ReturnsDbSet([user]);
            mockContext.Setup(c => c.UserInterests).ReturnsDbSet([]);
            mockContext.Setup(c => c.Interests).ReturnsDbSet([]);
            mockContext.Setup(c => c.Addresses).ReturnsDbSet([]);
            mockContext.Setup(c => c.Comments).ReturnsDbSet([]);
            mockContext.Setup(c => c.Events).ReturnsDbSet([]);


            var userService = new UserService(mockContext.Object);

            userService.DeleteUser(1);

            mockContext.Verify(c => c.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public void DeleteUser_UserDoesNotExist_NothingHappens()
        {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Users).ReturnsDbSet([]);

            var userService = new UserService(mockContext.Object);

            userService.DeleteUser(1);

            mockContext.Verify(c => c.SaveChanges(), Times.Never);
        }
    }
}