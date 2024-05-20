using ReasnAPI.Models.DTOs;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ReasnAPI.Validators
{
    public class ImageValidator : IValidator<ImageDto>
    {
        private const string ObjectTypeRegexPattern = "^(Event|User)$";

        public static IEnumerable<ValidationResult> Validate(ImageDto image)
        {
            if (!new Regex(ObjectTypeRegexPattern).IsMatch(image.ObjectType.ToString()))
            {
                yield return new ValidationResult("Object type is invalid", [nameof(image.ObjectType)]);
            }
        }
    }
}
