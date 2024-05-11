using ReasnAPI.Models.Database;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ReasnAPI.Validators
{
    public class EventValidator : IValidator<Event>
    {
        public static IEnumerable<ValidationResult> Validate(Event eventData)
        {
            if (string.IsNullOrWhiteSpace(eventData.Name))
            {
                yield return new ValidationResult("Name is required", [nameof(eventData.Name)]);
            }

            if (eventData.Name.Length > 64)
            {
                yield return new ValidationResult("Name is too long", [nameof(eventData.Name)]);
            }

            if (string.IsNullOrWhiteSpace(eventData.AddressId.ToString()))
            {
                yield return new ValidationResult("AddressId is required", [nameof(eventData.AddressId)]);
            }

            if (string.IsNullOrWhiteSpace(eventData.Description))
            {
                yield return new ValidationResult("Description is required", [nameof(eventData.Description)]);
            }

            if (eventData.Description.Length > 4048)
            {
                yield return new ValidationResult("Description is too long", [nameof(eventData.Description)]);
            }

            if (string.IsNullOrWhiteSpace(eventData.OrganizerId.ToString()))
            {
                yield return new ValidationResult("OrganizerId is required", [nameof(eventData.OrganizerId)]);
            }

            if (string.IsNullOrWhiteSpace(eventData.StartAt.ToString()))
            {
                yield return new ValidationResult("StartTime is required", [nameof(eventData.StartAt)]);
            }

            if (string.IsNullOrWhiteSpace(eventData.EndAt.ToString()))
            {
                yield return new ValidationResult("EndTime is required", [nameof(eventData.EndAt)]);
            }

            if (eventData.StartAt > eventData.EndAt)
            {
                yield return new ValidationResult("StartTime is after EndTime", [nameof(eventData.StartAt)]);
            }

            if (string.IsNullOrWhiteSpace(eventData.CreatedAt.ToString()))
            {
                yield return new ValidationResult("CreatedAt is required", [nameof(eventData.CreatedAt)]);
            }

            if (string.IsNullOrWhiteSpace(eventData.UpdatedAt.ToString()))
            {
                yield return new ValidationResult("UpdatedAt is required", [nameof(eventData.UpdatedAt)]);
            }

            if (string.IsNullOrWhiteSpace(eventData.Slug))
            {
                yield return new ValidationResult("Slug is required", [nameof(eventData.Slug)]);
            }

            if (eventData.Slug.Length > 128)
            {
                yield return new ValidationResult("Slug is too long", [nameof(eventData.Slug)]);
            }

            if (new Regex("^[\\p{L}\\d]+[\\p{L}\\d-]*$").IsMatch(eventData.Slug))
            {
                yield return new ValidationResult("Slug is invalid", [nameof(eventData.Name)]);
            }

            if (string.IsNullOrWhiteSpace(eventData.StatusId.ToString()))
            {
                yield return new ValidationResult("StatusId is required", [nameof(eventData.StatusId)]);
            }
        }
    }
}
