using Microsoft.EntityFrameworkCore;
using ReasnAPI.Exceptions;
using ReasnAPI.Mappers;
using ReasnAPI.Models.Database;
using ReasnAPI.Models.DTOs;
using System.Linq.Expressions;
using System.Transactions;

namespace ReasnAPI.Services;

public class UserService(ReasnContext context)
{
    public UserDto UpdateUser(int userId, UserDto userDto)
    {
        using (var scope = new TransactionScope())
        {
            ArgumentNullException.ThrowIfNull(userDto);

            var user = context.Users
                                .Include(u => u.UserInterests)
                                .ThenInclude(ui => ui.Interest)
                                .FirstOrDefault(r => r.Id == userId);

            if (user is null)
            {
                throw new NotFoundException("User not found");
            }

            var usernameExists = context.Users.Any(r => r.Username == userDto.Username && r.Id != userId);

            if (usernameExists)
            {
                throw new BadRequestException("User with given username already exists");
            }

            var emailExists = context.Users.Any(r => r.Email == userDto.Email && r.Id != userId);

            if (emailExists)
            {
                throw new BadRequestException("User with given email already exists");
            }

            var phoneExists = context.Users.Any(r => r.Phone == userDto.Phone && r.Id != userId);

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

            context.Users.Update(user);

            if (userDto.Interests is null || userDto.Interests.Count == 0)
            {
                context.SaveChanges();
                scope.Complete();
                return userDto;
            }

            var interestsToRemove = user.UserInterests
                                        .Where(ui => !userDto.Interests.Exists(uid => uid.Interest.Name == ui.Interest.Name));

            context.UserInterests.RemoveRange(interestsToRemove);

            var interestsToAdd = userDto.Interests
                                        .Where(uid => !user.UserInterests.Any(ui => ui.Interest.Name == uid.Interest.Name))
                                        .Select(uid => uid.ToEntity())
                                        .ToList();

            context.UserInterests.AddRange(interestsToAdd);

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
                context.UserInterests.Update(interest);
            }

            context.SaveChanges();
            scope.Complete();
        }

        return userDto;
    }

    public UserDto GetUserById(int userId)
    {
        var user = context.Users
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
        var user = context.Users
                            .Include(u => u.UserInterests)
                            .FirstOrDefault(u => u.Username == username);

        if (user is null)
        {
            throw new NotFoundException("User not found");
        }

        return user.ToDto();
    }

    public IEnumerable<UserDto> GetUsersByFilter(Expression<Func<User, bool>> filter)
    {
        return context.Users
                        .Include(u => u.UserInterests)
                        .ThenInclude(ui => ui.Interest)
                        .Where(filter)
                        .ToDtoList()
                        .AsEnumerable();
    }

    public IEnumerable<UserDto> GetAllUsers()
    {
        var users = context.Users
                            .Include(u => u.UserInterests)
                            .ThenInclude(ui => ui.Interest)
                            .ToList();

        return users.ToDtoList().AsEnumerable();
    }
}