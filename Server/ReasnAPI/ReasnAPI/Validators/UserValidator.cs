using ReasnAPI.Models.DTOs;
using FluentValidation;

namespace ReasnAPI.Validators;

public class UserValidator : AbstractValidator<UserDto>
{
    public UserValidator()
    {
        RuleFor(r => r.Name)
            .NotEmpty()
            .MaximumLength(64)
            .Matches(@"^\p{Lu}[\p{Ll}\s'-]+$");

        RuleFor(r => r.Surname)
            .NotEmpty()
            .MaximumLength(64)
            .Matches(@"^\p{L}+(?:[\s'-]\p{L}+)*$");

        RuleFor(r => r.Username)
            .NotEmpty()
            .MaximumLength(64)
            .Matches(@"^[\p{L}\d._%+-]{4,}$");

        RuleFor(r => r.Email)
            .NotEmpty()
            .MaximumLength(255)
            .EmailAddress();

        RuleFor(r => r.Phone)
            .Matches(@"^\+\d{1,3}\s\d{1,15}$")
            .When(r => !string.IsNullOrEmpty(r.Phone));
    }
}