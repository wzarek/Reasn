using ReasnAPI.Models.DTOs;
using FluentValidation;

namespace ReasnAPI.Validators;

public class TagValidator : AbstractValidator<TagDto>
{
    private const int MaxNameLength = 64;

    private const string NameRegex = @"^\p{L}+(?:\s\p{L}+)*$";

    public TagValidator()
    {
        RuleFor(t => t.Name)
            .NotEmpty()
            .MaximumLength(MaxNameLength)
            .Matches(NameRegex);
    }
}