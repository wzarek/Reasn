using ReasnAPI.Models.DTOs;
using FluentValidation;

namespace ReasnAPI.Validators;

public class ParameterValidator : AbstractValidator<ParameterDto>
{
    private const int MaxKeyLength = 32;
    private const int MaxValueLength = 64;

    private const string KeyRegex = @"^\p{L}+(?:\s\p{L}+)*$";
    private const string ValueRegex = @"^[\p{L}\d]+(?:\s[\p{L}\d]+)*$";

    public ParameterValidator()
    {
        RuleFor(p => p.Key)
            .NotEmpty()
            .MaximumLength(MaxKeyLength)
            .Matches(KeyRegex);

        RuleFor(p => p.Value)
            .NotEmpty()
            .MaximumLength(MaxValueLength)
            .Matches(ValueRegex);
    }
}