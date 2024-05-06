using ReasnAPI.Models.Database;
using ReasnAPI.Models.DTOs;
using System.Linq.Expressions;

namespace ReasnAPI.Services
{
    public class UserService(ReasnContext context)
    {
        private readonly ReasnContext _context = context;

        // TODO:
        // * create, update, delete user's interests
        // * fix delete service

        public UserDto? CreateUser(UserDto? userDto)
        {
            if (userDto is null)
            {
                return null;
            }

            // check if user with the same username already exists
            var userDb = _context.Users.FirstOrDefault(r => r.Username == userDto.Username);

            if (userDb is not null)
            {
                return null;
            }

            var user = new User
            {
                Name = userDto.Name,
                Surname = userDto.Surname,
                Username = userDto.Username,
                Email = userDto.Email,
                Phone = userDto.Phone,
                RoleId = userDto.RoleId,
                AddressId = userDto.AddressId,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return userDto;
        }

        public UserDto? UpdateUser(int userId, UserDto? userDto)
        {
            if (userDto is null)
            {
                return null;
            }

            var user = _context.Users.FirstOrDefault(r => r.Id == userId);

            if (user is null)
            {
                return null;
            }

            user.Name = userDto.Name;
            user.Surname = userDto.Surname;
            user.Username = userDto.Username;
            user.Email = userDto.Email;
            user.Phone = userDto.Phone;
            user.RoleId = userDto.RoleId;
            user.AddressId = userDto.AddressId;

            user.UpdatedAt = DateTime.UtcNow;

            _context.SaveChanges();

            return MapToUserDto(user);
        }

        public bool DeleteUser(int userId)
        {
            var user = _context.Users.FirstOrDefault(r => r.Id == userId);

            if (user is null)
            {
                return false;
            }

            _context.Users.Remove(user);
            _context.SaveChanges();

            return true;
        }

        public UserDto? GetUserById(int userId)
        {
            var user = _context.Users.FirstOrDefault(r => r.Id == userId);

            if (user is null)
            {
                return null;
            }

            return MapToUserDto(user);
        }

        public IEnumerable<UserDto?> GetUsersByFilter(Expression<Func<User, bool>> filter)
        {
            return _context.Users
                           .Where(filter)
                           .Select(user => MapToUserDto(user))
                           .AsEnumerable();
        }

        public IEnumerable<UserDto?> GetAllUsers()
        {
            return _context.Users
                           .Select(user => MapToUserDto(user))
                           .AsEnumerable();
        }

        private static UserDto MapToUserDto(User user)
        {
            var userDto = new UserDto
            {
                Username = user.Username,
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                Phone = user.Phone,
                RoleId = user.RoleId,
                AddressId = user.AddressId
            };

            return userDto;
        }
    }
}