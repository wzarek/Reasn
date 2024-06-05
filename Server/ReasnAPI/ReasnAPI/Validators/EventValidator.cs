using ReasnAPI.Models.DTOs;
using FluentValidation;

namespace ReasnAPI.Validators;

public class EventValidator : AbstractValidator<EventDto>
{
    public EventValidator()
    {
        RuleFor(e => e.Name)
            .NotEmpty()
            .MaximumLength(64);

        RuleFor(e => e.Description)
            .NotEmpty()
            .MaximumLength(4048);

        RuleFor(e => e.StartAt)
            .LessThan(e => e.EndAt)
            .WithMessage("'StartAt' must be before 'EndAt'.");

        RuleFor(e => e.Slug)
            .NotEmpty()
            .MaximumLength(128)
            .Matches(@"^[\p{L}\d]+[\p{L}\d-]*$");

        RuleForEach(e => e.Tags)
            .SetValidator(new TagValidator())
            .When(e => e.Tags?.Count > 0);

        RuleForEach(e => e.Parameters)
            .SetValidator(new ParameterValidator())
            .When(e => e.Parameters?.Count > 0);
    }
}