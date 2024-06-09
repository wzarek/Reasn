using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ReasnAPI.Models.Authentication;

[ValidateNever]
public class LoginRequest
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}