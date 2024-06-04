using FluentValidation;
using ReasnAPI.Models.DTOs;

namespace ReasnAPI.Validators;

public class AddressValidator : AbstractValidator<AddressDto>
{
    public AddressValidator()
    {
        RuleFor(r => r.Country)
            .NotEmpty()
            .MaximumLength(64)
            .Matches(@"^\p{Lu}\p{Ll}+(?:(\s|-)(\p{Lu}\p{Ll}+|i|of|and|the)){0,5}$");

        RuleFor(r => r.City)
            .NotEmpty()
            .MaximumLength(64)
            .Matches(@"^\p{Lu}\p{Ll}+(?:(\s|-)\p{Lu}\p{Ll}+)*$");

        RuleFor(r => r.Street)
            .NotEmpty()
            .MaximumLength(64)
            .Matches(@"^[\p{L}\d]+(?:(\s)\p{L}+)*(\s(?:(\d+\p{L}?(/\d*\p{L}?)?)))?$");

        RuleFor(r => r.State)
            .NotEmpty()
            .MaximumLength(64)
            .Matches(@"^\p{Lu}\p{Ll}+(?:(\s|-)\p{L}+)*$");

        RuleFor(r => r.ZipCode)
            .MaximumLength(8)
            .Matches(@"^[\p{L}\d\s-]{3,}$")
            .When(r => !string.IsNullOrEmpty(r.ZipCode));
    }
}