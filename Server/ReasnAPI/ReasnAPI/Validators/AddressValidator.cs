using FluentValidation;
using ReasnAPI.Models.DTOs;

namespace ReasnAPI.Validators;

public class AddressValidator : AbstractValidator<AddressDto>
{
    private const int MaxCountryLength = 64;
    private const int MaxCityLength = 64;
    private const int MaxStreetLength = 64;
    private const int MaxStateLength = 64;
    private const int MaxZipCodeLength = 8;

    private const string CountryRegex = @"^\p{Lu}[\p{L}\s'-]*(?<![\s-])$";
    private const string CityRegex = @"^\p{Lu}[\p{Ll}'.]+(?:[\s-][\p{L}'.]+)*$";
    private const string StreetRegex = @"^[\p{L}\d\s\-/.,#']+(?<![-\s#,])$";
    private const string StateRegex = @"^\p{Lu}\p{Ll}+(?:(\s|-)\p{L}+)*$";
    private const string ZipCodeRegex = @"^[\p{L}\d\s-]{3,}$";

    public AddressValidator()
    {
        RuleFor(a => a.Country)
            .NotEmpty()
            .MaximumLength(MaxCountryLength)
            .Matches(CountryRegex);

        RuleFor(a => a.City)
            .NotEmpty()
            .MaximumLength(MaxCityLength)
            .Matches(CityRegex);

        RuleFor(a => a.Street)
            .NotEmpty()
            .MaximumLength(MaxStreetLength)
            .Matches(StreetRegex);

        RuleFor(a => a.State)
            .NotEmpty()
            .MaximumLength(MaxStateLength)
            .Matches(StateRegex);

        RuleFor(r => r.ZipCode)
            .MaximumLength(MaxZipCodeLength)
            .Matches(ZipCodeRegex)
            .When(r => !string.IsNullOrEmpty(r.ZipCode));
    }
}