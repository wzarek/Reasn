using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReasnAPI.Models.Authentication;
using ReasnAPI.Validators.Authentication;

namespace ReasnAPI.Tests.UnitTests.Validators.Authentication;

[TestClass]
public class LoginRequestValidatorTests
{
    private LoginRequestValidator _validator = null!;

    [TestInitialize]
    public void Setup()
    {
        _validator = new LoginRequestValidator();
    }

    [TestMethod]
    public void Validate_WhenValidRequest_ShouldReturnTrue()
    {
        var request = new LoginRequest { Email = "test@example.com", Password = "password" };
        var result = _validator.Validate(request);

        Assert.IsTrue(result.IsValid);
    }

    [TestMethod]
    public void Validate_WhenEmptyEmail_ShouldReturnFalse()
    {
        var request = new LoginRequest { Email = "", Password = "password" };
        var result = _validator.Validate(request);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Exists(
            e => e.ErrorMessage == "'Email' must not be empty."
        ));
    }

    [TestMethod]
    public void Validate_WhenInvalidEmail_ShouldReturnFalse()
    {
        var request = new LoginRequest { Email = "invalid email", Password = "password" };
        var result = _validator.Validate(request);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Exists(
            e => e.ErrorMessage == "'Email' is not a valid email address."
        ));
    }

    [TestMethod]
    public void Validate_WhenEmptyPassword_ShouldReturnFalse()
    {
        var request = new LoginRequest { Email = "test@example.com", Password = "" };
        var result = _validator.Validate(request);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Exists(
            e => e.ErrorMessage == "'Password' must not be empty."
        ));
    }
}