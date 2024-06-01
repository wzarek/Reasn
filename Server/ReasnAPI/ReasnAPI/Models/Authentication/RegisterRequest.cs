using ReasnAPI.Models.DTOs;
using ReasnAPI.Models.Enums;

namespace ReasnAPI.Models.Authentication;

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