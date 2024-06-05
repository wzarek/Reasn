using FluentValidation;
using ReasnAPI.Models.Authentication;
using ReasnAPI.Models.Enums;

namespace ReasnAPI.Validators.Authentication;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
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

        RuleFor(r => r.Password)
            .NotEmpty()
            .Matches(@"^((?=\S*?[A-Z])(?=\S*?[a-z])(?=\S*?[0-9]).{6,})\S$")
            .WithMessage(
                "Password must contain at least one uppercase letter, " +
                "one lowercase letter, one number, and be at least 6 characters long.");

        RuleFor(r => r.Phone)
            .Matches(@"^\+\d{1,3}\s\d{1,15}$")
            .When(r => !string.IsNullOrEmpty(r.Phone));

        RuleFor(r => r.Address)
            .NotNull()
            .SetValidator(new AddressValidator()!);

        RuleFor(r => r.Role)
            .NotEmpty()
            .Must(r => r == UserRole.User.ToString() || r == UserRole.Organizer.ToString())
            .WithMessage($"Role must be either '{UserRole.User.ToString()}' or '{UserRole.Organizer.ToString()}'.");
    }
}