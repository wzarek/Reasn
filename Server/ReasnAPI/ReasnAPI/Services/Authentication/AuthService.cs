using Microsoft.AspNetCore.Identity;
using ReasnAPI.Models.Authentication;
using ReasnAPI.Models.Database;
using ReasnAPI.Models.Enums;
using ReasnAPI.Services.Exceptions;
using Exception = System.Exception;

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
            u.Email.ToUpper() == request.Email.ToUpper());

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
            u.Email.ToUpper() == request.Email.ToUpper() ||
            u.Username.ToUpper() == request.Username.ToUpper() ||
            (!string.IsNullOrEmpty(request.Phone) && u.Phone == request.Phone));

        if (userAlreadyExists)
        {
            throw new BadRequestException(
                "User with provided email, username or phone already exists");
        }

        var user = new User
        {
            Name = request.Name,
            Surname = request.Surname,
            Email = request.Email,
            Username = request.Username,
            Role = Enum.Parse<UserRole>(request.Role),
            Phone = request.Phone,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            Address = new Address
            {
                Country = request.Address.Country,
                City = request.Address.City,
                Street = request.Address.Street,
                State = request.Address.State,
                ZipCode = request.Address.ZipCode
            }
        };
        _context.Users.Add(user);

        user.Password = _hasher.HashPassword(user, request.Password);
        _context.SaveChanges();

        return user;
    }
}