using ReasnAPI.Models.Database;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ReasnAPI.Validators
{
    public class StatusValidator : IValidator<Status>
    {
        public static IEnumerable<ValidationResult> Validate(Status status)
        {
            if (string.IsNullOrWhiteSpace(status.Name))
            {
                yield return new ValidationResult("Name is required", [nameof(status.Name)]);
            }

            if (status.Name.Length > 32)
            {
                yield return new ValidationResult("Name is too long", [nameof(status.Name)]);
            }

            if (string.IsNullOrWhiteSpace(status.ObjectTypeId.ToString()))
            {
                yield return new ValidationResult("ObjectTypeId is required", [nameof(status.ObjectTypeId)]);
            }

            if (new Regex("^(Interested|Participating|Completed|In progress|Waiting for approval)$").IsMatch(status.Name) is false)
            {
                yield return new ValidationResult("Name is invalid", [nameof(status.Name)]);
            }
        }
    }
}
