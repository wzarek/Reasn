using FluentValidation;
using ReasnAPI.Models.DTOs;

namespace ReasnAPI.Validators;

public class AddressValidator : AbstractValidator<AddressDto>
{
    public AddressValidator()
    {
        RuleFor(a => a.Country)
            .NotEmpty()
            .MaximumLength(64)
            .Matches(@"^\p{Lu}[\p{L}\s'-]*(?<![\s-])$");

        RuleFor(a => a.City)
            .NotEmpty()
            .MaximumLength(64)
            .Matches(@"^\p{Lu}[\p{Ll}'.]+(?:[\s-][\p{L}'.]+)*$");

        RuleFor(a => a.Street)
            .NotEmpty()
            .MaximumLength(64)
            .Matches(@"^[\p{L}\d\s\-/.,#']+(?<![-\s#,])$");

        RuleFor(a => a.State)
            .NotEmpty()
            .MaximumLength(64)
            .Matches(@"^\p{Lu}\p{Ll}+(?:(\s|-)\p{L}+)*$");

        RuleFor(r => r.ZipCode)
            .MaximumLength(8)
            .Matches(@"^[\p{L}\d\s-]{3,}$")
            .When(r => !string.IsNullOrEmpty(r.ZipCode));
    }
}