using ReasnAPI.Models.DTOs;
using System.ComponentModel.DataAnnotations;

namespace ReasnAPI.Validators
{
    public class CommentValidator : IValidator<CommentDto>
    {
        public static IEnumerable<ValidationResult> Validate(CommentDto comment)
        {
            if (string.IsNullOrWhiteSpace(comment.Content))
            {
                yield return new ValidationResult("Content is required", [nameof(comment.Content)]);
            }

            if (comment.Content.Length > 1024)
            {
                yield return new ValidationResult("Content is too long", [nameof(comment.Content)]);
            }

            if (comment.CreatedAt >= DateTime.Now)
            {
                yield return new ValidationResult("CreatedAt is in the future", [nameof(comment.CreatedAt)]);
            }
        }
    }
}
