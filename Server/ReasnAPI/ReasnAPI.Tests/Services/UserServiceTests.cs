using Moq;
using Moq.EntityFrameworkCore;
using ReasnAPI.Exceptions;
using ReasnAPI.Models.Database;
using ReasnAPI.Models.DTOs;
using ReasnAPI.Models.Enums;
using ReasnAPI.Services;

namespace ReasnAPI.Tests.Services;

[TestClass]
public class UserServiceTests
{
    [TestMethod]
    public void GetUserById_UserDoesNotExist_NullReturned()
    {
        var mockContext = new Mock<ReasnContext>();
        mockContext.Setup(c => c.Users).ReturnsDbSet([]);

        var userService = new UserService(mockContext.Object);

        Assert.ThrowsException<NotFoundException>(() => userService.GetUserById(1));
    }

    [TestMethod]
    public void GetUserById_UserExists_UserReturned()
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
            Address = address,
        };

        mockContext.Setup(c => c.Users).ReturnsDbSet([user]);
        mockContext.Setup(c => c.Addresses).ReturnsDbSet([address]);

        var userService = new UserService(mockContext.Object);

        var result = userService.GetUserById(1);

        Assert.IsNotNull(result);
        Assert.AreEqual("John", result.Name);
        Assert.AreEqual("Doe", result.Surname);
        Assert.AreEqual("Username", result.Username);
        Assert.AreEqual("Email", result.Email);
    }

    [TestMethod]
    public void GetUserByUsername_UserDoesNotExist_NullReturned()
    {
        var mockContext = new Mock<ReasnContext>();
        mockContext.Setup(c => c.Users).ReturnsDbSet([]);

        var userService = new UserService(mockContext.Object);

        Assert.ThrowsException<NotFoundException>(() => userService.GetUserByUsername("username"));
    }

    [TestMethod]
    public void GetUserByUsername_UserExists_UserReturned()
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
            Address = address,
        };

        mockContext.Setup(c => c.Users).ReturnsDbSet([user]);

        var userService = new UserService(mockContext.Object);

        var result = userService.GetUserByUsername("Username");

        Assert.IsNotNull(result);
        Assert.AreEqual("John", result.Name);
        Assert.AreEqual("Doe", result.Surname);
        Assert.AreEqual("Username", result.Username);
        Assert.AreEqual("Email", result.Email);
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

        mockContext.Setup(c => c.Addresses).ReturnsDbSet([address]);
        mockContext.Setup(c => c.Users).ReturnsDbSet([user]);
        mockContext.Setup(c => c.UserInterests).ReturnsDbSet([]);

        var userService = new UserService(mockContext.Object);

        var userDto = new UserDto
        {
            Name = "Jane",
            Surname = "Doe",
            Username = "username",
            Email = "Email",
            Phone = "Phone",
            AddressId = address.Id,
            Role = UserRole.User
        };

        var result = userService.UpdateUser("Username", userDto);

        Assert.IsNotNull(result);
        Assert.AreEqual("Jane", result.Name);
        Assert.AreEqual("Doe", result.Surname);
        Assert.AreEqual("username", result.Username);
        Assert.AreEqual("Email", result.Email);
        Assert.AreEqual("Phone", result.Phone);
        Assert.AreEqual(UserRole.User, result.Role);
        Assert.AreEqual(1, result.AddressId);
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

        Assert.ThrowsException<ArgumentNullException>(() => userService.UpdateUser("Username", null));
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

        Assert.ThrowsException<NotFoundException>(() => userService.UpdateUser("Username", userDto));
    }
}