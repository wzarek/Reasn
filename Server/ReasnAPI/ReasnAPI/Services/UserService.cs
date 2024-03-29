using ReasnAPI.Models.Database;
using ReasnAPI.Models.DTOs;
using System.Linq.Expressions;

namespace ReasnAPI.Services {
    public class UserService (ReasnContext context) {
        private readonly ReasnContext _context = context;

        /* TODO:
         * - get user's intrests
         * - update user's intrests
         */

        public UserDto CreateUser(UserDto userDto) {
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

        public UserDto UpdateUser(int userId, UserDto userDto) { 
            var user = _context.Users.FirstOrDefault(r => r.Id == userId);

            if (user == null) {
                return null;
            }

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

            if (user != null) { 
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }

        public UserDto GetUserById(int userId) {
            return MapToUserDto(_context.Users.FirstOrDefault(r => r.Id == userId));
        }

        public List<UserDto> GetUsersByFilter(Expression<Func<User, bool>> filter) {
            return _context.Users.Where(filter).Select(user => MapToUserDto(user)).ToList();
        }

        public List<UserDto> GetAllUsers() {
            return _context.Users.Select(user => MapToUserDto(user)).ToList();
        }

        private static UserDto MapToUserDto(User user) {
            return new UserDto {
                Username = user.Username,
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                Phone = user.Phone,
                RoleId = user.RoleId,
                AddressId = user.AddressId
            };
        }
    }
}
