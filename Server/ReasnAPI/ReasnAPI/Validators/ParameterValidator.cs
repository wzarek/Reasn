using ReasnAPI.Models.Database;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ReasnAPI.Validators
{
    public class ParameterValidator : IValidator<Parameter>
    {
        public static IEnumerable<ValidationResult> Validate(Parameter parameter)
        {
            if (string.IsNullOrWhiteSpace(parameter.Key))
            {
                yield return new ValidationResult("Key is required", [nameof(parameter.Key)]);
            }

            if (parameter.Key.Length > 32)
            {
                yield return new ValidationResult("Key is too long", [nameof(parameter.Key)]);
            }

            if (new Regex("^\\p{L}+(?:\\s\\p{L}+)*$").IsMatch(parameter.Key))
            {
                yield return new ValidationResult("Key is invalid", [nameof(parameter.Key)]);
            }

            if (string.IsNullOrWhiteSpace(parameter.Value))
            {
                yield return new ValidationResult("Value is required", [nameof(parameter.Value)]);
            }

            if (parameter.Value.Length > 64)
            {
                yield return new ValidationResult("Value is too long", [nameof(parameter.Value)]);
            }

            if (new Regex("^[\\p{L}\\d]+(?:\\s[\\p{L}\\d]+)*$").IsMatch(parameter.Value))
            {
                yield return new ValidationResult("Value is invalid", [nameof(parameter.Value)]);
            }
        }
    }
}
