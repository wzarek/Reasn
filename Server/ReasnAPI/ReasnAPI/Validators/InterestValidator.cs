using ReasnAPI.Models.DTOs;
using FluentValidation;

namespace ReasnAPI.Validators;

public class InterestValidator : AbstractValidator<InterestDto>
{
    public InterestValidator()
    {
        RuleFor(i => i.Name)
            .NotEmpty()
            .MaximumLength(32)
            .Matches(@"^\p{Lu}\p{Ll}+(?:\s\p{L}+)*$");
    }
}