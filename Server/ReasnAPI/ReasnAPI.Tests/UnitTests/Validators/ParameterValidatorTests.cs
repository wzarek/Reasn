using ReasnAPI.Models.DTOs;
using ReasnAPI.Validators;

namespace ReasnAPI.Tests.UnitTests.Validators;

[TestClass]
public class ParameterValidatorTests
{
    private ParameterValidator _validator = null!;

    [TestInitialize]
    public void Setup()
    {
        _validator = new ParameterValidator();
    }

    [TestMethod]
    public void Validate_WhenParameterIsValid_ShouldReturnTrue()
    {
        var parameter = new ParameterDto
        {
            Key = "Key",
            Value = "Value"
        };

        var result = _validator.Validate(parameter);

        Assert.IsTrue(result.IsValid);
    }

    [TestMethod]
    public void Validate_WhenKeyIsEmpty_ShouldReturnFalse()
    {
        var parameter = new ParameterDto
        {
            Key = ""
        };

        var result = _validator.Validate(parameter);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Exists(
            e => e.ErrorMessage == "'Key' must not be empty."
        ));
    }

    [TestMethod]
    public void Validate_WhenKeyIsTooLong_ShouldReturnFalse()
    {
        var parameter = new ParameterDto
        {
            Key = new string('a', 33)
        };

        var result = _validator.Validate(parameter);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Exists(
            e => e.ErrorMessage ==
                 "The length of 'Key' must be 32 characters or fewer. You entered 33 characters."
        ));
    }

    [TestMethod]
    public void Validate_WhenInvalidKey_ShouldReturnFalse()
    {
        var parameter = new ParameterDto
        {
            Key = "123"
        };

        var result = _validator.Validate(parameter);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Exists(
            e => e.ErrorMessage == "'Key' is not in the correct format."
        ));
    }

    [TestMethod]
    public void Validate_WhenValueIsEmpty_ShouldReturnFalse()
    {
        var parameter = new ParameterDto
        {
            Value = ""
        };

        var result = _validator.Validate(parameter);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Exists(
            e => e.ErrorMessage == "'Value' must not be empty."
        ));
    }

    [TestMethod]
    public void Validate_WhenValueIsTooLong_ShouldReturnFalse()
    {
        var parameter = new ParameterDto
        {
            Value = new string('a', 65)
        };

        var result = _validator.Validate(parameter);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Exists(
            e => e.ErrorMessage ==
                 "The length of 'Value' must be 64 characters or fewer. You entered 65 characters."
        ));
    }

    [TestMethod]
    public void Validate_WhenInvalidValue_ShouldReturnFalse()
    {
        var parameter = new ParameterDto
        {
            Value = "Not a valid value!"
        };

        var result = _validator.Validate(parameter);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Exists(
            e => e.ErrorMessage == "'Value' is not in the correct format."
        ));
    }
}