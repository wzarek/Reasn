using ReasnAPI.Models.DTOs;
using FluentValidation;

namespace ReasnAPI.Validators;

public class ParameterValidator : AbstractValidator<ParameterDto>
{
    public ParameterValidator()
    {
        RuleFor(p => p.Key)
            .NotEmpty()
            .MaximumLength(32)
            .Matches(@"^\p{L}+(?:\s\p{L}+)*$");

        RuleFor(p => p.Value)
            .NotEmpty()
            .MaximumLength(64)
            .Matches(@"^[\p{L}\d]+(?:\s[\p{L}\d]+)*$");
    }
}