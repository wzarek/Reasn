using ReasnAPI.Models.DTOs;
using ReasnAPI.Validators;

namespace ReasnAPI.Tests.UnitTests.Validators;

[TestClass]
public class TagValidatorTests
{
    private TagValidator _validator = null!;

    [TestInitialize]
    public void Setup()
    {
        _validator = new TagValidator();
    }

    [TestMethod]
    public void Validate_WhenTagIsValid_ShouldReturnTrue()
    {
        var tag = new TagDto
        {
            Name = "Tag"
        };

        var result = _validator.Validate(tag);

        Assert.IsTrue(result.IsValid);
    }

    [TestMethod]
    public void Validate_WhenNameIsEmpty_ShouldReturnFalse()
    {
        var tag = new TagDto
        {
            Name = ""
        };

        var result = _validator.Validate(tag);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Exists(
            e => e.ErrorMessage == "'Name' must not be empty."
        ));
    }

    [TestMethod]
    public void Validate_WhenNameIsTooLong_ShouldReturnFalse()
    {
        var tag = new TagDto
        {
            Name = new string('a', 65)
        };

        var result = _validator.Validate(tag);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Exists(
            e => e.ErrorMessage ==
                 "The length of 'Name' must be 64 characters or fewer. You entered 65 characters."
        ));
    }

    [TestMethod]
    public void Validate_WhenNameContainsInvalidCharacters_ShouldReturnFalse()
    {
        var tag = new TagDto
        {
            Name = "123"
        };

        var result = _validator.Validate(tag);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Exists(
            e => e.ErrorMessage == "'Name' is not in the correct format."
        ));
    }
}