using FluentValidation;
using ReasnAPI.Models.Authentication;

namespace ReasnAPI.Validators.Authentication;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(r => r.Email)
            .NotEmpty()
            .EmailAddress()
            .WithMessage("Email is not in expected format");

        RuleFor(r => r.Password)
            .NotEmpty();
    }
}