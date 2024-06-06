using ReasnAPI.Validators.Authentication;
using ReasnAPI.Models.Authentication;
using ReasnAPI.Models.DTOs;

namespace ReasnAPI.Tests.UnitTests.Validators.Authentication;

[TestClass]
public class RegisterRequestValidatorTests
{
    private RegisterRequestValidator _validator = null!;

    [TestInitialize]
    public void Setup()
    {
        _validator = new RegisterRequestValidator();
    }

    [TestMethod]
    public void Validate_WhenValidRequest_ShouldReturnTrue()
    {
        var request = new RegisterRequest
        {
            Name = "Jon",
            Surname = "Snow",
            Email = "jon.snow@castleblack.com",
            Username = "jonSnow",
            Password = "S3cureP@ssword!",
            Phone = "+123 456789",
            Address = new AddressDto
            {
                Street = "The Wall",
                City = "Castle Black",
                Country = "Westeros",
                State = "The North"
            },
            Role = "User"
        };
        var result = _validator.Validate(request);

        Assert.IsTrue(result.IsValid);
    }

    [TestMethod]
    public void Validate_WhenEmptyName_ShouldReturnFalse()
    {
        var request = new RegisterRequest
        {
            Name = ""
        };

        var result = _validator.Validate(request);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Exists(
            e => e.ErrorMessage == "'Name' must not be empty."
        ));
    }

    [TestMethod]
    public void Validate_WhenNameTooLong_ShouldReturnFalse()
    {
        var request = new RegisterRequest
        {
            Name = new string('a', 65)
        };

        var result = _validator.Validate(request);

        Assert.IsFalse(result.IsValid);
        Console.WriteLine(result.Errors[0]);
        Assert.IsTrue(result.Errors.Exists(
            e => e.ErrorMessage ==
                 "The length of 'Name' must be 64 characters or fewer. You entered 65 characters."
        ));
    }

    [TestMethod]
    public void Validate_WhenInvalidName_ShouldReturnFalse()
    {
        var request = new RegisterRequest
        {
            Name = "123"
        };

        var result = _validator.Validate(request);

        Assert.IsFalse(result.IsValid);
        Console.WriteLine(result.Errors[0]);
        Assert.IsTrue(result.Errors.Exists(
            e => e.ErrorMessage == "'Name' is not in the correct format."
        ));
    }

    [TestMethod]
    public void Validate_WhenEmptySurname_ShouldReturnFalse()
    {
        var request = new RegisterRequest
        {
            Surname = ""
        };

        var result = _validator.Validate(request);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Exists(
            e => e.ErrorMessage == "'Surname' must not be empty."
        ));
    }

    [TestMethod]
    public void Validate_WhenSurnameTooLong_ShouldReturnFalse()
    {
        var request = new RegisterRequest
        {
            Surname = new string('a', 65)
        };

        var result = _validator.Validate(request);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Exists(
            e => e.ErrorMessage ==
                 "The length of 'Surname' must be 64 characters or fewer. You entered 65 characters."
        ));
    }

    [TestMethod]
    public void Validate_WhenInvalidSurname_ShouldReturnFalse()
    {
        var request = new RegisterRequest
        {
            Surname = "123"
        };

        var result = _validator.Validate(request);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Exists(
            e => e.ErrorMessage == "'Surname' is not in the correct format."
        ));
    }

    [TestMethod]
    public void Validate_WhenEmptyUsername_ShouldReturnFalse()
    {
        var request = new RegisterRequest
        {
            Username = ""
        };

        var result = _validator.Validate(request);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Exists(
            e => e.ErrorMessage == "'Username' must not be empty."
        ));
    }

    [TestMethod]
    public void Validate_WhenUsernameTooLong_ShouldReturnFalse()
    {
        var request = new RegisterRequest
        {
            Username = new string('a', 65)
        };

        var result = _validator.Validate(request);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Exists(
            e => e.ErrorMessage ==
                 "The length of 'Username' must be 64 characters or fewer. You entered 65 characters."
        ));
    }

    [TestMethod]
    public void Validate_WhenInvalidUsername_ShouldReturnFalse()
    {
        var request = new RegisterRequest
        {
            Username = "user name"
        };

        var result = _validator.Validate(request);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Exists(
            e => e.ErrorMessage == "'Username' is not in the correct format."
        ));
    }

    [TestMethod]
    public void Validate_WhenEmptyEmail_ShouldReturnFalse()
    {
        var request = new RegisterRequest
        {
            Email = ""
        };

        var result = _validator.Validate(request);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Exists(
            e => e.ErrorMessage == "'Email' must not be empty."
        ));
    }

    [TestMethod]
    public void Validate_WhenEmailTooLong_ShouldReturnFalse()
    {
        var request = new RegisterRequest
        {
            Email = new string('a', 256)
        };

        var result = _validator.Validate(request);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Exists(
            e => e.ErrorMessage ==
                 "The length of 'Email' must be 255 characters or fewer. You entered 256 characters."
        ));
    }

    [TestMethod]
    public void Validate_WhenInvalidEmail_ShouldReturnFalse()
    {
        var request = new RegisterRequest
        {
            Email = "invalid email"
        };

        var result = _validator.Validate(request);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Exists(
            e => e.ErrorMessage == "'Email' is not a valid email address."
        ));
    }

    [TestMethod]
    public void Validate_WhenEmptyPassword_ShouldReturnFalse()
    {
        var request = new RegisterRequest
        {
            Password = ""
        };

        var result = _validator.Validate(request);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Exists(
            e => e.ErrorMessage == "'Password' must not be empty."
        ));
    }

    [TestMethod]
    public void Validate_WhenInvalidPassword_ShouldReturnFalse()
    {
        var request = new RegisterRequest
        {
            Password = "password"
        };

        var result = _validator.Validate(request);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Exists(
            e => e.ErrorMessage ==
                 "Password must contain at least one uppercase letter, " +
                 "one lowercase letter, one number, and be at least 6 characters long."
        ));
    }

    [TestMethod]
    public void Validate_WhenInvalidPhone_ShouldReturnFalse()
    {
        var request = new RegisterRequest
        {
            Phone = "invalid phone"
        };

        var result = _validator.Validate(request);

        Assert.IsFalse(result.IsValid);
        Console.WriteLine(result.Errors[5]);
        Assert.IsTrue(result.Errors.Exists(
            e => e.ErrorMessage == "'Phone' is not in the correct format."
        ));
    }

    [TestMethod]
    public void Validate_WhenEmptyAddress_ShouldReturnFalse()
    {
        var request = new RegisterRequest
        {
            Address = null!
        };

        var result = _validator.Validate(request);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Exists(
            e => e.ErrorMessage == "'Address' must not be empty."
        ));
    }

    [TestMethod]
    public void Validate_WhenEmptyRole_ShouldReturnFalse()
    {
        var request = new RegisterRequest
        {
            Role = ""
        };

        var result = _validator.Validate(request);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Exists(
            e => e.ErrorMessage == "'Role' must not be empty."
        ));
    }

    [TestMethod]
    public void Validate_WhenInvalidRole_ShouldReturnFalse()
    {
        var request = new RegisterRequest
        {
            Role = "invalid role"
        };

        var result = _validator.Validate(request);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Exists(
            e => e.ErrorMessage == "Role must be either 'User' or 'Organizer'."
        ));
    }
}