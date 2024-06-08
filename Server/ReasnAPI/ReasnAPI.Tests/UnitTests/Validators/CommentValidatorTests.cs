using ReasnAPI.Models.DTOs;
using ReasnAPI.Validators;

namespace ReasnAPI.Tests.UnitTests.Validators;

[TestClass]
public class CommentValidatorTests
{
    private CommentValidator _validator = null!;

    [TestInitialize]
    public void Setup()
    {
        _validator = new CommentValidator();
    }

    [TestMethod]
    public void Validate_WhenCommentIsValid_ShouldReturnTrue()
    {
        var comment = new CommentDto
        {
            Content = "Content",
            CreatedAt = DateTime.UtcNow
        };

        var result = _validator.Validate(comment);

        Assert.IsTrue(result.IsValid);
    }

    [TestMethod]
    public void Validate_WhenContentIsEmpty_ShouldReturnFalse()
    {
        var comment = new CommentDto
        {
            Content = ""
        };

        var result = _validator.Validate(comment);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Exists(
            e => e.ErrorMessage == "'Content' must not be empty."
        ));
    }

    [TestMethod]
    public void Validate_WhenContentIsTooLong_ShouldReturnFalse()
    {
        var comment = new CommentDto
        {
            Content = new string('a', 1025)
        };

        var result = _validator.Validate(comment);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Exists(
            e => e.ErrorMessage ==
                 "The length of 'Content' must be 1024 characters or fewer. You entered 1025 characters."
        ));
    }
}