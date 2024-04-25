using ReasnAPI.Models.Database;
using ReasnAPI.Models.DTOs;
using System.Linq.Expressions;

namespace ReasnAPI.Services {
    public class UserService (ReasnContext context) {
        private readonly ReasnContext _context = context;

        // TODO: create, update, delete user's interests

        public UserDto? CreateUser(UserDto? userDto) {
            if (userDto is null)
                return null;

            // check if user with the same username already exists
            var userDb = _context.Users.FirstOrDefault(r => r.Username == userDto.Username);

            if (userDb is not null)
                return null;

            var user = new User() {
                Name = userDto.Name,
                Surname = userDto.Surname,
                Username = userDto.Username,
                Email = userDto.Email,
                Phone = userDto.Phone,
                RoleId = userDto.RoleId,
                AddressId = userDto.AddressId,

                CreatedAt = DateTime.Now,
                IsActive = true
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return userDto;
        }

        public UserDto? UpdateUser(int userId, UserDto? userDto) {
            if (userDto is null)
                return null;

            var user = _context.Users.FirstOrDefault(r => r.Id == userId);

            if (user is null) 
                return null;

            user.Name = userDto.Name;
            user.Surname = userDto.Surname;
            user.Username = userDto.Username;
            user.Email = userDto.Email;
            user.Phone = userDto.Phone;
            user.RoleId = userDto.RoleId;
            user.AddressId = userDto.AddressId;

            user.UpdatedAt = DateTime.Now;

            _context.SaveChanges();

            return MapToUserDto(user);
        }

        public void DeleteUser(int userId) {
            var user = _context.Users.FirstOrDefault(r => r.Id == userId);

            if (user is not null) { 
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }

        public UserDto? GetUserById(int userId) {
            return MapToUserDto(_context.Users.FirstOrDefault(r => r.Id == userId));
        }

        public IEnumerable<UserDto?> GetUsersByFilter(Expression<Func<User, bool>> filter) {
            return _context.Users
                           .Where(filter)
                           .Select(user => MapToUserDto(user))
                           .ToList();
        }

        public IEnumerable<UserDto?> GetAllUsers() {
            return _context.Users
                           .Select(user => MapToUserDto(user))
                           .ToList();
        }

        private static UserDto? MapToUserDto(User? user) {
            if (user is null)
                return null;

            var userDto = new UserDto();

            userDto.Username = user.Username;
            userDto.Name = user.Name;
            userDto.Surname = user.Surname;
            userDto.Email = user.Email;
            userDto.Phone = user.Phone;
            userDto.RoleId = user.Role.Id;
            if (user.Address is not null)
                userDto.AddressId = user.Address.Id;

            return userDto;
        }
    }
}
