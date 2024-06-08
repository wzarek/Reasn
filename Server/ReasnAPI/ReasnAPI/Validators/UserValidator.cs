using ReasnAPI.Models.DTOs;
using FluentValidation;

namespace ReasnAPI.Validators;

public class UserValidator : AbstractValidator<UserDto>
{
    private const int MaxNameLength = 64;
    private const int MaxSurnameLength = 64;
    private const int MaxUsernameLength = 64;
    private const int MaxEmailLength = 255;

    private const string NameRegex = @"^\p{Lu}[\p{Ll}\s'-]+$";
    private const string SurnameRegex = @"^\p{L}+(?:[\s'-]\p{L}+)*$";
    private const string UsernameRegex = @"^[\p{L}\d._%+-]{4,}$";
    private const string PhoneRegex = @"^\+\d{1,3}\s\d{1,15}$";

    public UserValidator()
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
            .MaximumLength(MaxEmailLength)
            .EmailAddress();

        RuleFor(r => r.Phone)
            .Matches(PhoneRegex)
            .When(r => !string.IsNullOrEmpty(r.Phone));
    }
}