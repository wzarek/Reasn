using ReasnAPI.Models.Database;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace ReasnAPI.Validators
{
    public class CommentValidator : IValidator<Comment>
    {
        public static IEnumerable<ValidationResult> Validate(Comment comment)
        {
            if (string.IsNullOrWhiteSpace(comment.EventId.ToString()))
            {
                yield return new ValidationResult("EventId is required", [nameof(comment.EventId)]);
            }

            if (string.IsNullOrWhiteSpace(comment.Content))
            {
                yield return new ValidationResult("Content is required", [nameof(comment.Content)]);
            }

            if (comment.Content.Length > 1024)
            {
                yield return new ValidationResult("Content is too long", [nameof(comment.Content)]);
            }

            if (string.IsNullOrWhiteSpace(comment.CreatedAt.ToString()))
            {
                yield return new ValidationResult("CreatedAt is required", [nameof(comment.CreatedAt)]);
            }

            if (comment.CreatedAt > DateTime.Now)
            {
                yield return new ValidationResult("CreatedAt is in the future", [nameof(comment.CreatedAt)]);
            }

            if (string.IsNullOrWhiteSpace(comment.UserId.ToString()))
            {
                yield return new ValidationResult("UserId is required", [nameof(comment.UserId)]);
            }
        }
    }
}
