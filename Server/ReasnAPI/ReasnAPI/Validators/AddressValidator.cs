using ReasnAPI.Models.DTOs;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ReasnAPI.Validators
{
    public class AddressValidator : IValidator<AddressDto>
    {
        public static IEnumerable<ValidationResult> Validate(AddressDto address)
        {
            if (string.IsNullOrWhiteSpace(address.City))
            {
                yield return new ValidationResult("City is required", [nameof(address.City)]);
            }

            if (address.City.Length > 64)
            {
                yield return new ValidationResult("City is too long", [nameof(address.City)]);
            }

            if (!new Regex("^\\p{Lu}\\p{Ll}+(?:(\\s|-)\\p{Lu}\\p{Ll}+)*$").IsMatch(address.City))
            {
                yield return new ValidationResult("City is invalid", [nameof(address.City)]);
            }

            if (string.IsNullOrWhiteSpace(address.Country))
            {
                yield return new ValidationResult("Country is required", [nameof(address.Country)]);
            }

            if (address.Country.Length > 64)
            {
                yield return new ValidationResult("Country is too long", [nameof(address.Country)]);
            }

            if (!new Regex("^\\p{Lu}\\p{Ll}+(?:(\\s|-)(\\p{Lu}\\p{Ll}+|i|of|and|the)){0,5}$").IsMatch(address.Country))
            {
                yield return new ValidationResult("Country is invalid", [nameof(address.Country)]);
            }

            if (string.IsNullOrWhiteSpace(address.Street))
            {
                yield return new ValidationResult("Street is required", [nameof(address.Street)]);
            }

            if (address.Street.Length > 64)
            {
                yield return new ValidationResult("Street is too long", [nameof(address.Street)]);
            }

            if (!new Regex("^[\\p{L}\\d]+(?:(\\s)\\p{L}+)*(\\s(?:(\\d+\\p{L}?(/\\d*\\p{L}?)?)))?$").IsMatch(address.Street))
            {
                yield return new ValidationResult("Street is invalid", [nameof(address.Street)]);
            }

            if (string.IsNullOrWhiteSpace(address.State))
            {
                yield return new ValidationResult("State is required", [nameof(address.State)]);
            }

            if (address.State.Length > 64)
            {
                yield return new ValidationResult("State is too long", [nameof(address.State)]);
            }

            if (!new Regex("^\\p{Lu}\\p{Ll}+(?:(\\s|-)\\p{L}+)*$").IsMatch(address.State))
            {
                yield return new ValidationResult("State is invalid", [nameof(address.State)]);
            }

            if (!string.IsNullOrWhiteSpace(address.ZipCode))
            {
                if (address.ZipCode.Length > 8)
                {
                    yield return new ValidationResult("ZipCode is too long", [nameof(address.ZipCode)]);
                }

                if (!new Regex("^[\\p{L}\\d\\s-]{3,}$").IsMatch(address.ZipCode))
                {
                    yield return new ValidationResult("ZipCode is invalid", [nameof(address.ZipCode)]);
                }
            }
        }
    }
}
