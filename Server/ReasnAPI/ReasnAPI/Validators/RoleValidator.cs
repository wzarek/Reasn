using ReasnAPI.Models.DTOs;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ReasnAPI.Validators
{
    public class RoleValidator : IValidator<RoleDto>
    {
        public static IEnumerable<ValidationResult> Validate(RoleDto role)
        {
            if (string.IsNullOrWhiteSpace(role.Name))
            {
                yield return new ValidationResult("Role name is required", [nameof(role.Name)]);
            }

            if (!new Regex("^(Organizer|Admin|User)$").IsMatch(role.Name))
            {
                yield return new ValidationResult("Role name is invalid", [nameof(role.Name)]);
            }
        }
    }
}