using Microsoft.EntityFrameworkCore;
using ReasnAPI.Exceptions;
using ReasnAPI.Mappers;
using ReasnAPI.Models.Database;
using ReasnAPI.Models.DTOs;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Transactions;

namespace ReasnAPI.Services;

public class UserService
{
    private readonly ReasnContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserService(ReasnContext context)
    {
        _context = context;
    }

    public UserService(ReasnContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public User GetCurrentUser()
    {
        var httpContext = _httpContextAccessor.HttpContext;

        if (httpContext is null)
        {
            throw new InvalidOperationException("No HTTP context available");
        }

        var email = httpContext.User.FindFirstValue(ClaimTypes.Email);
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

    public UserDto UpdateUser(int userId, UserDto userDto)
    {
        using (var scope = new TransactionScope())
        {
            ArgumentNullException.ThrowIfNull(userDto);

            var user = _context.Users
                                .Include(u => u.UserInterests)
                                .ThenInclude(ui => ui.Interest)
                                .FirstOrDefault(r => r.Id == userId);

            if (user is null)
            {
                throw new NotFoundException("User not found");
            }

            var usernameExists = _context.Users.Any(r => r.Username == userDto.Username && r.Id != userId);

            if (usernameExists)
            {
                throw new BadRequestException("User with given username already exists");
            }

            var emailExists = _context.Users.Any(r => r.Email == userDto.Email && r.Id != userId);

            if (emailExists)
            {
                throw new BadRequestException("User with given email already exists");
            }

            var phoneExists = _context.Users.Any(r => r.Phone == userDto.Phone && r.Id != userId);

            if (phoneExists)
            {
                throw new BadRequestException("User with given phone number already exists");
            }

            user.Username = userDto.Username;
            user.Name = userDto.Name;
            user.Surname = userDto.Surname;
            user.Username = userDto.Username;
            user.Email = userDto.Email;
            user.Phone = userDto.Phone;
            user.Role = userDto.Role;
            user.AddressId = userDto.AddressId;
            user.UpdatedAt = DateTime.UtcNow;

            _context.Users.Update(user);

            if (userDto.Interests is null || userDto.Interests.Count == 0)
            {
                _context.SaveChanges();
                scope.Complete();
                return userDto;
            }

            var interestsToRemove = user.UserInterests
                                        .Where(ui => !userDto.Interests.Exists(uid => uid.Interest.Name == ui.Interest.Name));

            _context.UserInterests.RemoveRange(interestsToRemove);

            var interestsToAdd = userDto.Interests
                                        .Where(uid => !user.UserInterests.Any(ui => ui.Interest.Name == uid.Interest.Name))
                                        .Select(uid => uid.ToEntity())
                                        .ToList();

            _context.UserInterests.AddRange(interestsToAdd);

            var interestsToUpdate = user.UserInterests
                                        .Where(ui => userDto.Interests.Exists(uid => uid.Interest.Name == ui.Interest.Name))
                                        .ToList();

            foreach (var interest in interestsToUpdate)
            {
                var updatedInterest = userDto.Interests.Find(uid => uid.Interest.Name == interest.Interest.Name);

                if (updatedInterest is null)
                {
                    continue;
                }

                interest.Level = updatedInterest.Level;
                _context.UserInterests.Update(interest);
            }

            _context.SaveChanges();
            scope.Complete();
        }

        return userDto;
    }

    public UserDto GetUserById(int userId)
    {
        var user = _context.Users
                            .Include(u => u.UserInterests)
                            .FirstOrDefault(u => u.Id == userId);

        if (user is null)
        {
            throw new NotFoundException("User not found");
        }

        return user.ToDto();
    }

    public UserDto GetUserByUsername(string username)
    {
        var user = _context.Users
                            .Include(u => u.UserInterests)
                            .FirstOrDefault(u => u.Username == username);

        if (user is null)
        {
            throw new NotFoundException("User not found");
        }

        return user.ToDto();
    }

    public int GetUserIdByUsername(string username)
    {
        var user = _context.Users
            .Include(u => u.UserInterests)
            .FirstOrDefault(u => u.Username == username);

        if (user is null)
        {
            throw new NotFoundException("User not found");
        }

        return user.Id;
    }

    public string GetUserUsernameById(int id)
    {
        var user = _context.Users
            .FirstOrDefault(u => u.Id == id);

        if (user is null)
        {
            throw new NotFoundException("User not found");
        }

        return user.Username;

    }

    public IEnumerable<UserDto> GetUsersByFilter(Expression<Func<User, bool>> filter)
    {
        return _context.Users
                        .Include(u => u.UserInterests)
                        .ThenInclude(ui => ui.Interest)
                        .Where(filter)
                        .ToDtoList()
                        .AsEnumerable();
    }

    public IEnumerable<UserDto> GetAllUsers()
    {
        var users = _context.Users
                            .Include(u => u.UserInterests)
                            .ThenInclude(ui => ui.Interest)
                            .ToList();

        return users.ToDtoList().AsEnumerable();
    }
}