using FluentValidation;
using ReasnAPI.Models.Authentication;
using ReasnAPI.Models.Enums;

namespace ReasnAPI.Validators.Authentication;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    private const int MaxNameLength = 64;
    private const int MaxSurnameLength = 64;
    private const int MaxUsernameLength = 64;
    private const int MaxEmailLength = 255;

    private const string NameRegex = @"^\p{Lu}[\p{Ll}\s'-]+$";
    private const string SurnameRegex = @"^\p{L}+(?:[\s'-]\p{L}+)*$";
    private const string UsernameRegex = @"^[\p{L}\d._%+-]{4,}$";
    private const string PasswordRegex = @"^((?=\S*?[A-Z])(?=\S*?[a-z])(?=\S*?[0-9]).{6,})\S$";
    private const string PhoneRegex = @"^\+\d{1,3}\s\d{1,15}$";

    public RegisterRequestValidator()
    {
        RuleFor(r => r.Name)
            .NotEmpty()
            .MaximumLength(MaxNameLength)
            .Matches(NameRegex);

        RuleFor(r => r.Surname)
            .NotEmpty()
            .MaximumLength(MaxSurnameLength)
            .Matches(SurnameRegex);

        RuleFor(r => r.Username)
            .NotEmpty()
            .MaximumLength(MaxUsernameLength)
            .Matches(UsernameRegex);

        RuleFor(r => r.Email)
            .NotEmpty()
            .MaximumLength(MaxEmailLength);

        RuleFor(r => r.Password)
            .NotEmpty()
            .Matches(PasswordRegex)
            .WithMessage(
                "Password must contain at least one uppercase letter, " +
                "one lowercase letter, one number, and be at least 6 characters long.");

        RuleFor(r => r.Phone)
            .Matches(PhoneRegex)
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