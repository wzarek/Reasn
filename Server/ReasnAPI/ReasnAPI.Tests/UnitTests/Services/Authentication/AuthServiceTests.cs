using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Moq;
using Moq.EntityFrameworkCore;
using ReasnAPI.Models.Authentication;
using ReasnAPI.Models.Database;
using ReasnAPI.Models.DTOs;
using ReasnAPI.Services.Authentication;
using ReasnAPI.Services.Exceptions;

namespace ReasnAPI.Tests.UnitTests.Services.Authentication;

[TestClass]
public class AuthServiceTests
{
    private Mock<ReasnContext> _mockContext = null!;
    private PasswordHasher<User> _hasher = null!;
    private AuthService _service = null!;

    [TestInitialize]
    public void Setup()
    {
        _mockContext = new Mock<ReasnContext>();
        _hasher = new PasswordHasher<User>();
        _service = new AuthService(_mockContext.Object);

        var user = new User
        {
            Email = "jon.snow@castleblack.com",
            Username = "jsnow",
            Password = _hasher.HashPassword(null!, "password"),
            Phone = "+123 456789"
        };
        _mockContext.Setup(c => c.Users)
            .ReturnsDbSet(new List<User> { user });
    }

    [TestMethod]
    public void Login_WhenUserExistsAndPasswordIsCorrect_ShouldReturnUser()
    {
        var request = new LoginRequest
        {
            Email = "jon.snow@castleblack.com",
            Password = "password"
        };

        var result = _service.Login(request);

        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result, typeof(User));
    }

    [TestMethod]
    public void Login_WhenUserDoesNotExist_ShouldThrowNotFoundException()
    {
        var request = new LoginRequest
        {
            Email = "jon.notsnow@castleblack.com"
        };

        Assert.ThrowsException<NotFoundException>(() => _service.Login(request));
    }

    [TestMethod]
    public void Login_WhenPasswordIsIncorrect_ShouldThrowVerificationException()
    {
        var request = new LoginRequest
        {
            Email = "jon.snow@castleblack.com",
            Password = "wrong-password"
        };

        Assert.ThrowsException<VerificationException>(() => _service.Login(request));
    }

    [TestMethod]
    public void Register_WhenUserWithEmailAlreadyExists_ShouldThrowBadRequestException()
    {
        var request = new RegisterRequest
        {
            Email = "jon.snow@castleblack.com"
        };

        Assert.ThrowsException<BadRequestException>(() => _service.Register(request));
    }

    [TestMethod]
    public void Register_WhenUserWithUsernameAlreadyExists_ShouldThrowBadRequestException()
    {
        var request = new RegisterRequest
        {
            Email = "jon.stark@castleblack.com",
            Username = "jsnow"
        };

        Assert.ThrowsException<BadRequestException>(() => _service.Register(request));
    }

    [TestMethod]
    public void Register_WhenUserWithPhoneAlreadyExists_ShouldThrowBadRequestException()
    {
        var request = new RegisterRequest
        {
            Email = "jon.stark@castleblack.com",
            Username = "jstark",
            Phone = "+123 456789"
        };

        Assert.ThrowsException<BadRequestException>(() => _service.Register(request));
    }

    [TestMethod]
    public void Register_WhenUserDoesNotExist_ShouldReturnRegisteredUser()
    {
        var request = new RegisterRequest
        {
            Name = "Jon",
            Surname = "Stark",
            Email = "jon.stark@castleblack.com",
            Username = "jstark",
            Password = "S3cureP@ssword!",
            Phone = "+123 456781",
            Address = new AddressDto
            {
                Street = "The Wall",
                City = "Castle Black",
                Country = "Westeros",
                State = "The North"
            },
            Role = "User"
        };

        var result = _service.Register(request);

        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result, typeof(User));
    }
}