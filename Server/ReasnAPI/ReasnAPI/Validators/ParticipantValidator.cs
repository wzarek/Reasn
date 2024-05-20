using ReasnAPI.Models.DTOs;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ReasnAPI.Validators
{
    public class ParticipantValidator : IValidator<ParticipantDto>
    {
        private const string StatusRegexPattern = "^(Interested|Participating)$";

        public static IEnumerable<ValidationResult> Validate(ParticipantDto participant)
        {
            if (!new Regex(StatusRegexPattern).IsMatch(participant.Status.ToString()))
            {
                yield return new ValidationResult("Status is invalid", [nameof(participant.Status)]);
            }
        }
    }
}
