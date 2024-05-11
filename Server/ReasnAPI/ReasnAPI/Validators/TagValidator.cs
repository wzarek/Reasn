using ReasnAPI.Models.Database;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ReasnAPI.Validators
{
    public class TagValidator : IValidator<Tag>
    {
        public static IEnumerable<ValidationResult> Validate(Tag tag)
        {
            if (string.IsNullOrWhiteSpace(tag.Name))
            {
                yield return new ValidationResult("Name is required", [nameof(tag.Name)]);
            }

            if (tag.Name.Length > 64)
            {
                yield return new ValidationResult("Name is too long", [nameof(tag.Name)]);
            }

            if (new Regex("^\\p{L}+(?:\\s\\p{L}+)*$").IsMatch(tag.Name))
            {
                yield return new ValidationResult("Name is invalid", [nameof(tag.Name)]);
            }
        }
    }
}
