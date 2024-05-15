using ReasnAPI.Models.DTOs;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ReasnAPI.Validators
{
    public class ParameterValidator : IValidator<ParameterDto>
    {
        private const int KeyMaxLength = 32;
        private const string KeyRegexPattern = "^\\p{L}+(?:\\s\\p{L}+)*$";
        private const int ValueMaxLength = 64;
        private const string ValueRegexPattern = "^[\\p{L}\\d]+(?:\\s[\\p{L}\\d]+)*$";

        public static IEnumerable<ValidationResult> Validate(ParameterDto parameter)
        {
            if (string.IsNullOrWhiteSpace(parameter.Key))
            {
                yield return new ValidationResult("Key is required", [nameof(parameter.Key)]);
            }

            if (parameter.Key.Length > KeyMaxLength)
            {
                yield return new ValidationResult("Key is too long", [nameof(parameter.Key)]);
            }

            if (!new Regex(KeyRegexPattern).IsMatch(parameter.Key))
            {
                yield return new ValidationResult("Key is invalid", [nameof(parameter.Key)]);
            }

            if (string.IsNullOrWhiteSpace(parameter.Value))
            {
                yield return new ValidationResult("Value is required", [nameof(parameter.Value)]);
            }

            if (parameter.Value.Length > ValueMaxLength)
            {
                yield return new ValidationResult("Value is too long", [nameof(parameter.Value)]);
            }

            if (!new Regex(ValueRegexPattern).IsMatch(parameter.Value))
            {
                yield return new ValidationResult("Value is invalid", [nameof(parameter.Value)]);
            }
        }
    }
}