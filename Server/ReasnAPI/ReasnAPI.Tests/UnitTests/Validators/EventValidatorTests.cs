using ReasnAPI.Models.DTOs;
using ReasnAPI.Models.Enums;
using ReasnAPI.Validators;

namespace ReasnAPI.Tests.UnitTests.Validators;

[TestClass]
public class EventValidatorTests
{
    private EventValidator _validator = null!;

    [TestInitialize]
    public void Setup()
    {
        _validator = new EventValidator();
    }

    [TestMethod]
    public void Validate_WhenEventIsValid_ShouldReturnTrue()
    {
        var eventDto = new EventDto
        {
            Name = "Event",
            Description = "Description",
            StartAt = DateTime.UtcNow,
            EndAt = DateTime.UtcNow.AddDays(1),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            Slug = "the-slug",
            Status = EventStatus.Approved
        };

        var result = _validator.Validate(eventDto);

        Assert.IsTrue(result.IsValid);
    }

    [TestMethod]
    public void Validate_WhenNameIsEmpty_ShouldReturnFalse()
    {
        var eventDto = new EventDto
        {
            Name = ""
        };

        var result = _validator.Validate(eventDto);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Exists(
            e => e.ErrorMessage == "'Name' must not be empty."
        ));
    }

    [TestMethod]
    public void Validate_WhenNameIsTooLong_ShouldReturnFalse()
    {
        var eventDto = new EventDto
        {
            Name = new string('a', 65)
        };

        var result = _validator.Validate(eventDto);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Exists(
            e => e.ErrorMessage ==
                 "The length of 'Name' must be 64 characters or fewer. You entered 65 characters."
        ));
    }

    [TestMethod]
    public void Validate_WhenDescriptionIsEmpty_ShouldReturnFalse()
    {
        var eventDto = new EventDto
        {
            Description = ""
        };

        var result = _validator.Validate(eventDto);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Exists(
            e => e.ErrorMessage == "'Description' must not be empty."
        ));
    }

    [TestMethod]
    public void Validate_WhenDescriptionIsTooLong_ShouldReturnFalse()
    {
        var eventDto = new EventDto
        {
            Description = new string('a', 4049)
        };

        var result = _validator.Validate(eventDto);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Exists(
            e => e.ErrorMessage ==
                 "The length of 'Description' must be 4048 characters or fewer. You entered 4049 characters."
        ));
    }

    [TestMethod]
    public void Validate_WhenStartAtIsAfterEndAt_ShouldReturnFalse()
    {
        var eventDto = new EventDto
        {
            StartAt = DateTime.UtcNow,
            EndAt = DateTime.UtcNow.AddDays(-1)
        };

        var result = _validator.Validate(eventDto);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Exists(
            e => e.ErrorMessage == "'StartAt' must be before 'EndAt'."
        ));
    }

    [TestMethod]
    public void Validate_WhenSlugIsEmpty_ShouldReturnTrue()
    {
        var eventDto = new EventDto
        {
            Slug = ""
        };

        var result = _validator.Validate(eventDto);

        Assert.IsTrue(result.IsValid);
    }

    [TestMethod]
    public void Validate_WhenSlugIsTooLong_ShouldReturnFalse()
    {
        var eventDto = new EventDto
        {
            Slug = new string('a', 129)
        };

        var result = _validator.Validate(eventDto);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Exists(
            e => e.ErrorMessage ==
                 "The length of 'Slug' must be 128 characters or fewer. You entered 129 characters."
        ));
    }

    [TestMethod]
    public void Validate_WhenInvalidSlug_ShouldReturnFalse()
    {
        var eventDto = new EventDto
        {
            Slug = "the-slug!"
        };

        var result = _validator.Validate(eventDto);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Exists(
            e => e.ErrorMessage == "'Slug' is not in the correct format."
        ));
    }

    [TestMethod]
    public void Validate_WhenTagsAreInvalid_ShouldReturnFalse()
    {
        var eventDto = new EventDto
        {
            Tags = new List<TagDto>
            {
                new() { Name = "" }
            }
        };

        var result = _validator.Validate(eventDto);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Exists(
            e => e.ErrorMessage == "'Name' must not be empty."
        ));
    }

    [TestMethod]
    public void Validate_WhenParametersAreInvalid_ShouldReturnFalse()
    {
        var eventDto = new EventDto
        {
            Parameters = new List<ParameterDto>
            {
                new() { Key = "", Value = "" }
            }
        };

        var result = _validator.Validate(eventDto);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Exists(
            e => e.ErrorMessage == "'Key' must not be empty."
        ));
    }

}