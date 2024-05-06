using ReasnAPI.Models.Database;
using System.ComponentModel.DataAnnotations;

namespace ReasnAPI.Validators
{
    public class AddressValidator : IValidator<Address>
    {
        public static IEnumerable<ValidationResult> Validate(Address address)
        {
            if (string.IsNullOrWhiteSpace(address.City))
            {
                yield return new ValidationResult("City is required", [nameof(address.City)]);
            }

            if (address.City.Length > 64)
            {
                yield return new ValidationResult("City is too long", [nameof(address.City)]);
            }

            if (string.IsNullOrWhiteSpace(address.Country))
            {
                yield return new ValidationResult("Country is required", [nameof(address.Country)]);
            }

            if (address.Country.Length > 64)
            {
                yield return new ValidationResult("Country is too long", [nameof(address.Country)]);
            }

            if (string.IsNullOrWhiteSpace(address.Street))
            {
                yield return new ValidationResult("Street is required", [nameof(address.Street)]);
            }

            if (address.Street.Length > 64)
            {
                yield return new ValidationResult("Street is too long", [nameof(address.Street)]);
            }

            if (string.IsNullOrWhiteSpace(address.State))
            {
                yield return new ValidationResult("State is required", [nameof(address.State)]);
            }

            if (address.State.Length > 64)
            {
                yield return new ValidationResult("State is too long", [nameof(address.State)]);
            }

            if (!string.IsNullOrWhiteSpace(address.ZipCode) && address.ZipCode.Length > 8 )
            {
                yield return new ValidationResult("ZipCode is too long", [nameof(address.ZipCode)]);
            }
        }
    }
}
