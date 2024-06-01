using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ReasnAPI.Models.Authentication;
using ReasnAPI.Models.Database;
using ReasnAPI.Models.Enums;
using ReasnAPI.Services.Exceptions;

namespace ReasnAPI.Services.Authentication;

public class AuthService
{
    private readonly ReasnContext _context;
    private readonly PasswordHasher<User> _hasher;

    public AuthService(ReasnContext context)
    {
        _context = context;
        _hasher = new PasswordHasher<User>();
    }

    public User Login(LoginRequest request)
    {
        var user = _context.Users.FirstOrDefault(u => 
            EF.Functions.ILike(u.Email, request.Email));

        if (user is null)
        {
            throw new NotFoundException("Not found user related with provided email");
        }
        
        var result = _hasher.VerifyHashedPassword(
            user, user.Password, request.Password);

        if (result != PasswordVerificationResult.Success)
        {
            throw new VerificationException("Provided password is incorrect");
        }
        
        return user;
    }
    
    public User Register(RegisterRequest request)
    {
        var userAlreadyExists = _context.Users.Any(u =>
            EF.Functions.ILike(u.Email, request.Email) ||
            EF.Functions.ILike(u.Username, request.Username));

        if (userAlreadyExists)
        {
            throw new BadRequestException(
                "User with provided email or username already exists");
        }
        
        var user = new User
        {
            Name = request.Name,
            Surname = request.Surname,
            Email = request.Email,
            Username = request.Username,
            Role = Enum.Parse<UserRole>(request.Role),
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            Address = new Address
            {
                Country = request.Address.Country,
                City = request.Address.City,
                Street = request.Address.Street,
                State = request.Address.State,
            }
        };
        _context.Users.Add(user);
        
        user.Password = _hasher.HashPassword(user, request.Password);
        _context.SaveChanges();
        
        return user;
    }
}