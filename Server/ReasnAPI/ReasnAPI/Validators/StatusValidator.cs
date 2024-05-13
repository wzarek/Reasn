using ReasnAPI.Models.DTOs;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ReasnAPI.Validators
{
    public class StatusValidator : IValidator<StatusDto>
    {
        public static IEnumerable<ValidationResult> Validate(StatusDto status)
        {
            if (string.IsNullOrWhiteSpace(status.Name))
            {
                yield return new ValidationResult("Name is required", [nameof(status.Name)]);
            }

            if (!new Regex("^(Interested|Participating|Completed|In progress|Waiting for approval)$").IsMatch(status.Name))
            {
                yield return new ValidationResult("Name is invalid", [nameof(status.Name)]);
            }
        }
    }
}
