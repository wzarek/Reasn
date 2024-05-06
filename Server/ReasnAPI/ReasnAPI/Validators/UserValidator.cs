using ReasnAPI.Models.Database;
using System.ComponentModel.DataAnnotations;

namespace ReasnAPI.Validators
{
    public class UserValidator : IValidator<User>
    {
        public static IEnumerable<ValidationResult> Validate(User user)
        {
            if (string.IsNullOrWhiteSpace(user.Name))
            {
                yield return new ValidationResult("Name is required", [nameof(user.Name)]);
            }

            if (user.Name.Length > 64)
            {
                yield return new ValidationResult("Name is too long", [nameof(user.Name)]);
            }

            if (string.IsNullOrWhiteSpace(user.Surname))
            {
                yield return new ValidationResult("Surname is required", [nameof(user.Surname)]);
            }

            if (user.Surname.Length > 64)
            {
                yield return new ValidationResult("Surname is too long", [nameof(user.Surname)]);
            }

            if (string.IsNullOrWhiteSpace(user.Username))
            {
                yield return new ValidationResult("Username is required", [nameof(user.Username)]);
            }

            if (user.Username.Length > 64)
            {
                yield return new ValidationResult("Username is too long", [nameof(user.Username)]);
            }

            if (string.IsNullOrWhiteSpace(user.Password))
            {
                yield return new ValidationResult("Password is required", [nameof(user.Password)]);
            }

            if (string.IsNullOrWhiteSpace(user.CreatedAt.ToString()))
            {
                yield return new ValidationResult("CreatedAt is required", [nameof(user.CreatedAt)]);
            }

            if (string.IsNullOrWhiteSpace(user.UpdatedAt.ToString()))
            {
                yield return new ValidationResult("UpdatedAt is required", [nameof(user.UpdatedAt)]);
            }

            if (string.IsNullOrWhiteSpace(user.RoleId.ToString()))
            {
                yield return new ValidationResult("RoleId is required", [nameof(user.RoleId)]);
            }

            if (string.IsNullOrWhiteSpace(user.Email))
            {
                yield return new ValidationResult("Email is required", [nameof(user.Email)]);
            }

            if (user.Email.Length > 255)
            {
                yield return new ValidationResult("Email is too long", [nameof(user.Email)]);
            }

            if (string.IsNullOrWhiteSpace(user.AddressId.ToString()))
            {
                yield return new ValidationResult("AddressId is required", [nameof(user.AddressId)]);
            }

            if (string.IsNullOrWhiteSpace(user.Phone))
            {
                yield return new ValidationResult("Phone is required", [nameof(user.Phone)]);
            }

            if (!string.IsNullOrWhiteSpace(user.Phone) && user.Phone.Length > 16)
            {
                yield return new ValidationResult("Phone is too long", [nameof(user.Phone)]);
            }
        }
    }
}
