using ReasnAPI.Models.DTOs;
using FluentValidation;

namespace ReasnAPI.Validators;

public class EventValidator : AbstractValidator<EventDto>
{
    private const int MaxNameLength = 64;
    private const int MaxDescriptionLength = 4048;
    private const int MaxSlugLength = 128;

    private const string SlugRegex = @"^[\p{L}\d]+[\p{L}\d-]*$";

    public EventValidator()
    {
        RuleFor(e => e.Name)
            .NotEmpty()
            .MaximumLength(MaxNameLength);

        RuleFor(e => e.Description)
            .NotEmpty()
            .MaximumLength(MaxDescriptionLength);

        RuleFor(e => e.StartAt)
            .LessThan(e => e.EndAt)
            .WithMessage("'StartAt' must be before 'EndAt'.");

        RuleFor(e => e.Slug)
            .NotEmpty()
            .MaximumLength(MaxSlugLength)
            .Matches(SlugRegex);

        RuleForEach(e => e.Tags)
            .SetValidator(new TagValidator())
            .When(e => e.Tags?.Count > 0);

        RuleForEach(e => e.Parameters)
            .SetValidator(new ParameterValidator())
            .When(e => e.Parameters?.Count > 0);
    }
}