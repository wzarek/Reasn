using ReasnAPI.Models.DTOs;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ReasnAPI.Validators
{
    public class InterestValidator : IValidator<IntrestDto>
    {
        public static IEnumerable<ValidationResult> Validate(IntrestDto interest)
        {
            if (string.IsNullOrWhiteSpace(interest.Name))
            {
                yield return new ValidationResult("Name is required", [nameof(interest.Name)]);
            }

            if (interest.Name.Length > 32)
            {
                yield return new ValidationResult("Name is too long", [nameof(interest.Name)]);
            }

            if (!new Regex("^\\p{Lu}\\p{Ll}+(?:\\s\\p{L}+)*$").IsMatch(interest.Name))
            {
                yield return new ValidationResult("Name is invalid", [nameof(interest.Name)]);
            }
        }
    }
}