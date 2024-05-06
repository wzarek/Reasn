using ReasnAPI.Models.Database;
using System.ComponentModel.DataAnnotations;

namespace ReasnAPI.Validators
{
    public class InterestValidator : IValidator<Interest>
    {
        public static IEnumerable<ValidationResult> Validate(Interest interest)
        {
            if (string.IsNullOrWhiteSpace(interest.Name))
            {
                yield return new ValidationResult("Name is required", [nameof(interest.Name)]);
            }

            if (interest.Name.Length > 32)
            {
                yield return new ValidationResult("Name is too long", [nameof(interest.Name)]);
            }
        }
    }
}
