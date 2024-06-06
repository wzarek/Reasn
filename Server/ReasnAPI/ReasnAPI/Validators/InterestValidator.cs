using ReasnAPI.Models.DTOs;
using FluentValidation;

namespace ReasnAPI.Validators;

public class InterestValidator : AbstractValidator<InterestDto>
{
    private const int MaxNameLength = 32;

    private const string NameRegex = @"^\p{Lu}\p{Ll}+(?:\s\p{L}+)*$";

    public InterestValidator()
    {
        RuleFor(i => i.Name)
            .NotEmpty()
            .MaximumLength(MaxNameLength)
            .Matches(NameRegex);
    }
}