using ReasnAPI.Models.Database;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ReasnAPI.Validators
{
    public class RoleValidator : IValidator<Role>
    {
        public static IEnumerable<ValidationResult> Validate(Role role)
        {
            if (string.IsNullOrWhiteSpace(role.Name))
            {
                yield return new ValidationResult("Role name is required", [nameof(role.Name)]);
            }

            if (role.Name.Length > 32)
            {
                yield return new ValidationResult("Role name is too long", [nameof(role.Name)]);
            }

            if (new Regex("^(Organizer|Admin|User)$").IsMatch(role.Name) is false)
            {
                yield return new ValidationResult("Role name is invalid", [nameof(role.Name)]);
            }
        }
    }
}
