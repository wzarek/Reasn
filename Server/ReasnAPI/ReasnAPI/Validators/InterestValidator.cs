using ReasnAPI.Models.DTOs;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ReasnAPI.Validators
{
    public class InterestValidator : IValidator<InterestDto>
    {
        private const int NameMaxLength = 32;
        private const string NameRegexPattern = "^\\p{Lu}\\p{Ll}+(?:\\s\\p{L}+)*$";

        public static IEnumerable<ValidationResult> Validate(InterestDto interest)
        {
            if (string.IsNullOrWhiteSpace(interest.Name))
            {
                yield return new ValidationResult("Name is required", [nameof(interest.Name)]);
            }

            if (interest.Name.Length > NameMaxLength)
            {
                yield return new ValidationResult("Name is too long", [nameof(interest.Name)]);
            }

            if (!new Regex(NameRegexPattern).IsMatch(interest.Name))
            {
                yield return new ValidationResult("Name is invalid", [nameof(interest.Name)]);
            }
        }
    }
}