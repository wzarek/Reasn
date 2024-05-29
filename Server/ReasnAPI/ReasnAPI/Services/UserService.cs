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

            var userDb = context.Users.FirstOrDefault(r => r.Username == userDto.Username);

            if (userDb is not null)
            {
                return null;
            }

            Console.WriteLine(userDto.Role);

            var user = new User
            {
                Name = userDto.Name,
                Surname = userDto.Surname,
                Username = userDto.Username,
                Email = userDto.Email,
                Phone = userDto.Phone,
                Role = userDto.Role,
                AddressId = userDto.AddressId,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };

            context.Users.Add(user);

            if (userDto.Interests is null)
            {
                context.SaveChanges();
                return userDto;
            }

            // Add user interests
            foreach (var interest in userDto.Interests)
            {
                var interestDb = context.Interests.FirstOrDefault(i => i.Name == interest.Interest.Name);

                if (interestDb is null)
                {
                    continue;
                }

                var newInterest = new UserInterest
                {
                    UserId = user.Id,
                    InterestId = interestDb.Id,
                    Level = interest.Level
                };

                context.UserInterests.Add(newInterest);
            }

            context.SaveChanges();

            return userDto;
        }

        public UserDto? UpdateUser(int userId, UserDto? userDto)
        {
            if (userDto is null)
            {
                return null;
            }

            var user = context.Users.FirstOrDefault(r => r.Id == userId);

            if (user is null)
            {
                return null;
            }

            // check if username already exists
            var usernameExists = context.Users.Any(r => r.Username == userDto.Username && r.Id != userId);

            if (usernameExists)
            {
                return null;
            }
            else
            {
                user.Username = userDto.Username;
            }

            user.Name = userDto.Name;
            user.Surname = userDto.Surname;
            user.Username = userDto.Username;
            user.Email = userDto.Email;
            user.Phone = userDto.Phone;
            user.Role = userDto.Role;
            user.AddressId = userDto.AddressId;
            user.UpdatedAt = DateTime.UtcNow;

            context.Users.Update(user);

            if (userDto.Interests is null)
            {
                context.SaveChanges();
                return MapToUserDto(user);
            }

            // TODO: 
            // Update user interests
            // get interests from db
            // check if interest from db is in interests from dto
            // if not, remove from db

            //var userInterests = context.UserInterests.Where(ui => ui.UserId == userId).ToList();

            //// Update or add new interests
            //foreach (var interest in userDto.Interests)
            //{
            //    var interestDb = context.Interests.FirstOrDefault(i => i.Name == interest.Interest.Name);

            //    if (interestDb is null)
            //    {
            //        continue;
            //    }

            //    var userInterest = context.UserInterests.FirstOrDefault(ui => ui.UserId == userId && ui.InterestId == interestDb.Id);

            //    if (userInterest is null)
            //    {
            //        var newInterest = new UserInterest
            //        {
            //            UserId = userId,
            //            InterestId = interestDb.Id,
            //            Level = interest.Level
            //        };

            //        context.UserInterests.Add(newInterest);
            //    }

            //    else
            //    {
            //        userInterest.Level = interest.Level;
            //        context.UserInterests.Update(userInterest);
            //    }
            //}

            context.SaveChanges();

            return MapToUserDto(user);
        }

        public bool DeleteUser(int userId)
        {
            var user = context.Users.FirstOrDefault(r => r.Id == userId);

            if (user is null)
            {
                return false;
            }

            // Remove all data related to the user
            //var userInterests = context.UserInterests.Where(ui => ui.UserId == userId);
            //foreach (var interest in userInterests)
            //{
            //    context.UserInterests.Remove(interest);
            //}

            //var userAddress = context.Addresses.FirstOrDefault(r => r.Id == user.AddressId);
            //if (userAddress is not null)
            //{
            //    context.Addresses.Remove(userAddress);
            //}

            //var userComments = context.Comments.Where(c => c.UserId == userId);
            //foreach (var comment in userComments)
            //{
            //    context.Comments.Remove(comment);
            //}

            //var userEvents = context.Events.Where(e => e.OrganizerId == userId);
            //foreach (var ev in userEvents)
            //{
            //    context.Events.Remove(ev);
            //}

            context.Users.Remove(user);
            context.SaveChanges();

            return true;
        }

        public UserDto? GetUserById(int userId)
        {
            var user = context.Users.Find(userId);

            if (user is null)
            {
                return null;
            }

            return MapToUserDto(user);
        }

        public IEnumerable<UserDto?> GetUsersByFilter(Expression<Func<User, bool>> filter)
        {
            return context.Users
                           .Where(filter)
                           .Select(user => MapToUserDto(user))
                           .AsEnumerable();
        }

        public IEnumerable<UserDto?> GetAllUsers()
        {
            return context.Users
                           .Select(user => MapToUserDto(user))
                           .AsEnumerable();
        }

        private static UserDto MapToUserDto(User user)
        {
            var userInterests = user.UserInterests.Select(ui => new UserInterestDto
            {
                Interest = new InterestDto
                {
                    Name = ui.Interest.Name
                },
                Level = ui.Level
            }).ToList();

            var userDto = new UserDto
            {
                Username = user.Username,
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                Phone = user.Phone,
                Role = user.Role,
                AddressId = user.AddressId,
                Interests = userInterests
            };

            return userDto;
        }
    }
}
