using System.Security.Claims;
using ReasnAPI.Models.Database;
using ReasnAPI.Services.Exceptions;

namespace ReasnAPI.Services;

public class UserService
{
    private readonly ReasnContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserService(ReasnContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public User GetCurrentUser()
    {
        var context = _httpContextAccessor.HttpContext;
        if (context == null)
        {
            throw new InvalidOperationException("No HTTP context available");
        }

        var email = context.User.FindFirstValue(ClaimTypes.Email);
        if (string.IsNullOrEmpty(email))
        {
            throw new UnauthorizedAccessException("No email claim found in token");
        }

        var user = _context.Users.FirstOrDefault(u => u.Email == email);
        if (user is null)
        {
            throw new NotFoundException("User associated with email not found");
        }

        return user;
    }
}