using FluentValidation;
using ReasnAPI.Models.Authentication;

namespace ReasnAPI.Validators.Authentication;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(lr => lr.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(lr => lr.Password)
            .NotEmpty();
    }
}