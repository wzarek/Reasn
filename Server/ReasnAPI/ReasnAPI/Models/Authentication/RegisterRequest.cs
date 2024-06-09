using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using ReasnAPI.Models.DTOs;

namespace ReasnAPI.Models.Authentication;

[ValidateNever]
public class RegisterRequest
{
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string? Phone { get; set; }
    public AddressDto Address { get; set; } = null!;
    public string Role { get; set; } = null!;
}