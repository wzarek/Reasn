using ReasnAPI.Models.Database;
using System.ComponentModel.DataAnnotations;

namespace ReasnAPI.Validators
{
    public class ImageValidator : IValidator<Image>
    {
        public static IEnumerable<ValidationResult> Validate(Image image)
        {
            if (string.IsNullOrWhiteSpace(image.ObjectTypeId.ToString()))
            {
                yield return new ValidationResult("ObjectTypeId is required", [nameof(image.ObjectTypeId)]);
            }

            if (string.IsNullOrWhiteSpace(image.ObjectId.ToString()))
            {
                yield return new ValidationResult("ObjectId is required", [nameof(image.ObjectId)]);
            }
        }
    }
}
