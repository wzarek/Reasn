using ReasnAPI.Models.DTOs;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ReasnAPI.Validators
{
    public class UserValidator : IValidator<UserDto>
    {
        private const int NameMaxLength = 64;
        private const string NameRegexPattern = "^\\p{Lu}\\p{Ll}+$";
        private const int SurnameMaxLength = 64;
        private const string SurnameRegexPattern = "^\\p{L}+(?:[\\s'-]?\\p{L}+)*$";
        private const int UsernameMaxLength = 64;
        private const string UsernameRegexPattern = "^[\\p{L}\\d._%+-]+$";
        private const int EmailMaxLength = 255;
        private const string EmailRegexPattern = "^[a-zA-Z\\d._%+-]+@[a-zA-Z\\d.-]+\\.[a-zA-Z]{2,6}$";
        private const int PhoneMaxLength = 16;
        private const string PhoneRegexPattern = "^\\+\\d{1,3}\\s\\d{1,15}$";

        public static IEnumerable<ValidationResult> Validate(UserDto user)
        {
            if (string.IsNullOrWhiteSpace(user.Name))
            {
                yield return new ValidationResult("Name is required", [nameof(user.Name)]);
            }

            if (user.Name.Length > NameMaxLength)
            {
                yield return new ValidationResult("Name is too long", [nameof(user.Name)]);
            }

            if (!new Regex(NameRegexPattern).IsMatch(user.Name))
            {
                yield return new ValidationResult("Name is invalid", [nameof(user.Name)]);
            }

            if (string.IsNullOrWhiteSpace(user.Surname))
            {
                yield return new ValidationResult("Surname is required", [nameof(user.Surname)]);
            }

            if (user.Surname.Length > SurnameMaxLength)
            {
                yield return new ValidationResult("Surname is too long", [nameof(user.Surname)]);
            }

            if (!new Regex(SurnameRegexPattern).IsMatch(user.Surname))
            {
                yield return new ValidationResult("Surname is invalid", [nameof(user.Surname)]);
            }

            if (string.IsNullOrWhiteSpace(user.Username))
            {
                yield return new ValidationResult("Username is required", [nameof(user.Username)]);
            }

            if (user.Username.Length > UsernameMaxLength)
            {
                yield return new ValidationResult("Username is too long", [nameof(user.Username)]);
            }

            if (!new Regex(UsernameRegexPattern).IsMatch(user.Username))
            {
                yield return new ValidationResult("Username is invalid", [nameof(user.Username)]);
            }

            if (string.IsNullOrWhiteSpace(user.Email))
            {
                yield return new ValidationResult("Email is required", [nameof(user.Email)]);
            }

            if (user.Email.Length > EmailMaxLength)
            {
                yield return new ValidationResult("Email is too long", [nameof(user.Email)]);
            }

            if (!new Regex(EmailRegexPattern).IsMatch(user.Email))
            {
                yield return new ValidationResult("Email is invalid", [nameof(user.Email)]);
            }

            if (!string.IsNullOrWhiteSpace(user.Phone))
            {
                if (user.Phone.Length > PhoneMaxLength)
                {
                    yield return new ValidationResult("Phone is too long", [nameof(user.Phone)]);
                }

                if (!new Regex(PhoneRegexPattern).IsMatch(user.Phone))
                {
                    yield return new ValidationResult("Phone is invalid", [nameof(user.Phone)]);
                }
            }
        }
    }
}