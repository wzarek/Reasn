using ReasnAPI.Models.DTOs;
using ReasnAPI.Validators;

namespace ReasnAPI.Tests.UnitTests.Validators;

[TestClass]
public class InterestValidatorTests
{
    private InterestValidator _validator = null!;

    [TestInitialize]
    public void Setup()
    {
        _validator = new InterestValidator();
    }

    [TestMethod]
    public void Validate_WhenInterestIsValid_ShouldReturnTrue()
    {
        var interest = new InterestDto
        {
            Name = "Interest"
        };

        var result = _validator.Validate(interest);

        Assert.IsTrue(result.IsValid);
    }

    [TestMethod]
    public void Validate_WhenNameIsEmpty_ShouldReturnFalse()
    {
        var interest = new InterestDto
        {
            Name = ""
        };

        var result = _validator.Validate(interest);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Exists(
            e => e.ErrorMessage == "'Name' must not be empty."
        ));
    }

    [TestMethod]
    public void Validate_WhenNameIsTooLong_ShouldReturnFalse()
    {
        var interest = new InterestDto
        {
            Name = new string('a', 33)
        };

        var result = _validator.Validate(interest);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Exists(
            e => e.ErrorMessage ==
                 "The length of 'Name' must be 32 characters or fewer. You entered 33 characters."
        ));
    }

    [TestMethod]
    public void Validate_WhenInvalidName_ShouldReturnFalse()
    {
        var interest = new InterestDto
        {
            Name = "123"
        };

        var result = _validator.Validate(interest);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Exists(
            e => e.ErrorMessage == "'Name' is not in the correct format."
        ));
    }
}