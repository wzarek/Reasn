using Microsoft.EntityFrameworkCore;
using ReasnAPI.Exceptions;
using ReasnAPI.Mappers;
using ReasnAPI.Models.Database;
using ReasnAPI.Models.DTOs;
using Serilog;
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

    public UserDto UpdateUser(string username, UserDto userDto)
    {
        using (var scope = new TransactionScope())
        {
            ArgumentNullException.ThrowIfNull(userDto);

            var user = _context.Users
                                .Include(u => u.UserInterests)
                                .ThenInclude(ui => ui.Interest)
                                .FirstOrDefault(r => r.Username == username);

            if (user is null)
            {
                throw new NotFoundException("User not found");
            }

            var usernameExists = _context.Users.Any(r => r.Username == userDto.Username && r.Id != user.Id);

            if (usernameExists)
            {
                throw new BadRequestException("User with given username already exists");
            }

            var emailExists = _context.Users.Any(r => r.Email == userDto.Email && r.Id != user.Id);

            if (emailExists)
            {
                throw new BadRequestException("User with given email already exists");
            }

            var phoneExists = _context.Users.Any(r => r.Phone == userDto.Phone && r.Id != user.Id);

            if (phoneExists)
            {
                throw new BadRequestException("User with given phone number already exists");
            }

            user.Name = userDto.Name;
            user.Surname = userDto.Surname;
            user.Username = userDto.Username;
            user.Email = userDto.Email;
            user.Phone = userDto.Phone;
            user.Role = userDto.Role;
            user.UpdatedAt = DateTime.UtcNow;

            _context.Users.Update(user);

            // Get list of interests to remove
            var interestsToRemove = user.UserInterests
                                        .Where(ui => !userDto.Interests!.Exists(uid => uid.Interest.Name == ui.Interest.Name));

            _context.UserInterests.RemoveRange(interestsToRemove);

            if (userDto.Interests is null || userDto.Interests.Count == 0)
            {
                _context.SaveChanges();
                scope.Complete();
                return userDto;
            }

            // Get list of interests to update
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

            // Get list of existing interests in the database
            var existingInterests = _context.Interests.ToList();

            // Get list of interests to add
            // Look for interests that are not already in the user's interests
            var interestsToAdd = userDto.Interests
                                        .Where(uid => !user.UserInterests.Any(ui => ui.Interest.Name == uid.Interest.Name))
                                        .Select(uid => uid.ToEntity(user.Id, existingInterests.Find(ei => ei.Name == uid.Interest.Name)!.Id))
                                        .ToList();

            // Update interests for
            interestsToAdd.ForEach(user.UserInterests.Add);
            _context.Users.Update(user);

            _context.SaveChanges();
            scope.Complete();
        }

        return userDto;
    }

    public UserDto GetUserById(int userId)
    {
        var user = _context.Users
                            .Include(a => a.Address)
                            .Include(u => u.UserInterests)
                            .ThenInclude(ui => ui.Interest)
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
                            .Include(a => a.Address)
                            .Include(u => u.UserInterests)
                            .ThenInclude(ui => ui.Interest)
                            .FirstOrDefault(u => u.Username == username);

        if (user is null)
        {
            throw new NotFoundException("User not found");
        }

        return user.ToDto();
    }

    public IEnumerable<UserDto> GetUsersByFilter(Expression<Func<User, bool>> filter)
    {
        return _context.Users
                        .Include(a => a.Address)
                        .Include(u => u.UserInterests)
                        .ThenInclude(ui => ui.Interest)
                        .Where(filter)
                        .ToDtoList()
                        .AsEnumerable();
    }

    public IEnumerable<UserDto> GetAllUsers()
    {
        var users = _context.Users
                            .Include(a => a.Address)
                            .Include(u => u.UserInterests)
                            .ThenInclude(ui => ui.Interest)
                            .ToList();

        return users.ToDtoList().AsEnumerable();
    }
}