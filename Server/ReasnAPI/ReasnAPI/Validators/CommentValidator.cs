using ReasnAPI.Models.DTOs;
using FluentValidation;

namespace ReasnAPI.Validators;

public class CommentValidator : AbstractValidator<CommentDto>
{
    private const int MaxContentLength = 1024;

    public CommentValidator()
    {
        RuleFor(c => c.Content)
            .NotEmpty()
            .MaximumLength(MaxContentLength);
    }
}
