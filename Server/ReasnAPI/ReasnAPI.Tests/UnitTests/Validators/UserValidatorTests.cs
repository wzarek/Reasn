using ReasnAPI.Models.DTOs;
using ReasnAPI.Models.Enums;
using ReasnAPI.Validators;

namespace ReasnAPI.Tests.UnitTests.Validators;

[TestClass]
public class UserValidatorTests
{
    private UserValidator _validator = null!;

    [TestInitialize]
    public void Setup()
    {
        _validator = new UserValidator();
    }

    [TestMethod]
    public void Validate_WhenValidRequest_ShouldReturnTrue()
    {
        var user = new UserDto
        {
            Name = "Jon",
            Surname = "Snow",
            Email = "jon.snow@castleblack.com",
            Username = "jonSnow",
            Phone = "+123 456789",
            Role = UserRole.User
        };

        var result = _validator.Validate(user);

        Assert.IsTrue(result.IsValid);
    }

    [TestMethod]
    public void Validate_WhenEmptyName_ShouldReturnFalse()
    {
        var user = new UserDto
        {
            Name = ""
        };

        var result = _validator.Validate(user);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Exists(
            e => e.ErrorMessage == "'Name' must not be empty."
        ));
    }

    [TestMethod]
    public void Validate_WhenNameTooLong_ShouldReturnFalse()
    {
        var user = new UserDto
        {
            Name = new string('a', 65)
        };

        var result = _validator.Validate(user);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Exists(
            e => e.ErrorMessage ==
                 "The length of 'Name' must be 64 characters or fewer. You entered 65 characters."
        ));
    }

    [TestMethod]
    public void Validate_WhenInvalidName_ShouldReturnFalse()
    {
        var user = new UserDto
        {
            Name = "123"
        };

        var result = _validator.Validate(user);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Exists(
            e => e.ErrorMessage == "'Name' is not in the correct format."
        ));
    }

    [TestMethod]
    public void Validate_WhenEmptySurname_ShouldReturnFalse()
    {
        var user = new UserDto
        {
            Surname = ""
        };

        var result = _validator.Validate(user);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Exists(
            e => e.ErrorMessage == "'Surname' must not be empty."
        ));
    }

    [TestMethod]
    public void Validate_WhenSurnameTooLong_ShouldReturnFalse()
    {
        var user = new UserDto
        {
            Surname = new string('a', 65)
        };

        var result = _validator.Validate(user);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Exists(
            e => e.ErrorMessage ==
                 "The length of 'Surname' must be 64 characters or fewer. You entered 65 characters."
        ));
    }

    [TestMethod]
    public void Validate_WhenInvalidSurname_ShouldReturnFalse()
    {
        var user = new UserDto
        {
            Surname = "123"
        };

        var result = _validator.Validate(user);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Exists(
            e => e.ErrorMessage == "'Surname' is not in the correct format."
        ));
    }

    [TestMethod]
    public void Validate_WhenEmptyEmail_ShouldReturnFalse()
    {
        var user = new UserDto
        {
            Email = ""
        };

        var result = _validator.Validate(user);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Exists(
            e => e.ErrorMessage == "'Email' must not be empty."
        ));
    }

    [TestMethod]
    public void Validate_WhenEmailTooLong_ShouldReturnFalse()
    {
        var user = new UserDto
        {
            Email = new string('a', 256)
        };

        var result = _validator.Validate(user);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Exists(
            e => e.ErrorMessage ==
                 "The length of 'Email' must be 255 characters or fewer. You entered 256 characters."
        ));
    }

    [TestMethod]
    public void Validate_WhenInvalidEmail_ShouldReturnFalse()
    {
        var user = new UserDto
        {
            Email = "jon.snow"
        };

        var result = _validator.Validate(user);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Exists(
            e => e.ErrorMessage == "'Email' is not a valid email address."
        ));
    }

    [TestMethod]
    public void Validate_WhenEmptyUsername_ShouldReturnFalse()
    {
        var user = new UserDto
        {
            Username = ""
        };

        var result = _validator.Validate(user);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Exists(
            e => e.ErrorMessage == "'Username' must not be empty."
        ));
    }

    [TestMethod]
    public void Validate_WhenUsernameTooLong_ShouldReturnFalse()
    {
        var user = new UserDto
        {
            Username = new string('a', 65)
        };

        var result = _validator.Validate(user);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Exists(
            e => e.ErrorMessage ==
                 "The length of 'Username' must be 64 characters or fewer. You entered 65 characters."
        ));
    }

    [TestMethod]
    public void Validate_WhenInvalidUsername_ShouldReturnFalse()
    {
        var user = new UserDto
        {
            Username = "user name"
        };

        var result = _validator.Validate(user);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Exists(
            e => e.ErrorMessage == "'Username' is not in the correct format."
        ));
    }

    [TestMethod]
    public void Validate_WhenInvalidPhone_ShouldReturnFalse()
    {
        var user = new UserDto
        {
            Phone = "123"
        };

        var result = _validator.Validate(user);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Exists(
            e => e.ErrorMessage == "'Phone' is not in the correct format."
        ));
    }
}