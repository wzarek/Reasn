using ReasnAPI.Models.DTOs;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ReasnAPI.Validators
{
    public class EventValidator : IValidator<EventDto>
    {
        private const int NameMaxLength = 64;
        private const int DescriptionMaxLength = 4048;
        private const int SlugMaxLength = 128;
        private const string SlugRegexPattern = "^[\\p{L}\\d]+[\\p{L}\\d-]*$";
        private const string StatusRegexPattern = "^(Completed|In progress|Approved|Waiting for aproval)$";

        public static IEnumerable<ValidationResult> Validate(EventDto eventData)
        {
            if (string.IsNullOrWhiteSpace(eventData.Name))
            {
                yield return new ValidationResult("Name is required", [nameof(eventData.Name)]);
            }

            if (eventData.Name.Length > NameMaxLength)
            {
                yield return new ValidationResult("Name is too long", [nameof(eventData.Name)]);
            }

            if (string.IsNullOrWhiteSpace(eventData.Description))
            {
                yield return new ValidationResult("Description is required", [nameof(eventData.Description)]);
            }

            if (eventData.Description.Length > DescriptionMaxLength)
            {
                yield return new ValidationResult("Description is too long", [nameof(eventData.Description)]);
            }

            if (eventData.StartAt > eventData.EndAt)
            {
                yield return new ValidationResult("StartTime is after EndTime", [nameof(eventData.StartAt)]);
            }

            if (eventData.CreatedAt >= DateTime.Now)
            {
                yield return new ValidationResult("CreatedAt is in the future", [nameof(eventData.CreatedAt)]);
            }

            if (eventData.UpdatedAt >= DateTime.Now)
            {
                yield return new ValidationResult("UpdatedAt is in the future", [nameof(eventData.UpdatedAt)]);
            }

            if (string.IsNullOrWhiteSpace(eventData.Slug))
            {
                yield return new ValidationResult("Slug is required", [nameof(eventData.Slug)]);
            }

            if (eventData.Slug.Length > SlugMaxLength)
            {
                yield return new ValidationResult("Slug is too long", [nameof(eventData.Slug)]);
            }

            if (!new Regex(SlugRegexPattern).IsMatch(eventData.Slug))
            {
                yield return new ValidationResult("Slug is invalid", [nameof(eventData.Name)]);
            }

            if (!new Regex(StatusRegexPattern).IsMatch(eventData.Status.ToString()))
            {
                yield return new ValidationResult("Status is invalid", [nameof(eventData.Status)]);
            }
        }
    }
}