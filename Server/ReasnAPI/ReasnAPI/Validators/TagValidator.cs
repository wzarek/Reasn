using ReasnAPI.Models.DTOs;
using FluentValidation;

namespace ReasnAPI.Validators;

public class TagValidator : AbstractValidator<TagDto>
{
    public TagValidator()
    {
        RuleFor(t => t.Name)
            .NotEmpty()
            .MaximumLength(64)
            .Matches(@"^\p{L}+(?:\s\p{L}+)*$");
    }
}