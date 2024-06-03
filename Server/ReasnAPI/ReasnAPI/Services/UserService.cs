using ReasnAPI.Models.Database;
using ReasnAPI.Models.DTOs;
using System.Linq.Expressions;

namespace ReasnAPI.Services
{
    public class UserService(ReasnContext context)
    {
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

            var usernameExists = context.Users.Any(r => r.Username == userDto.Username && r.Id != userId);

            if (usernameExists)
            {
                return null;
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
                return MapToUserDto(user);
            }

            var userInterestsDb = context.UserInterests
                                       .Where(ui => ui.UserId == userId)
                                       .ToList();

            foreach (var userInterest in userInterestsDb)
            {
                var interestDb = context.Interests.Find(userInterest.InterestId);

                if (interestDb is null)
                {
                    continue;
                }

                if (userDto.Interests.Exists(i => i.Interest.Name == interestDb.Name))
                {
                    var userInterestDto = userDto.Interests.Find(i => i.Interest.Name == interestDb.Name);   

                    if (userInterestDto is null)
                    {
                        continue;
                    }

                    userInterest.Level = userInterestDto.Level;
                    context.UserInterests.Update(userInterest);
                    userDto.Interests.RemoveAll(i => i.Interest.Name == interestDb.Name);
                }
                else
                {
                    context.UserInterests.Remove(userInterest);
                }
            }

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

            return GetUserById(userId);
        }

        public UserDto? GetUserById(int userId)
        {
            var user = context.Users.Find(userId);

            if (user is null)
            {
                return null;
            }

            var userInterests = context.UserInterests
                                       .Where(ui => ui.UserId == userId)
                                       .ToList();

            if (userInterests.Count == 0)
            {
                return MapToUserDto(user);
            }

            var userInterestDtos = new List<UserInterestDto>();

            foreach (var userInterest in userInterests) 
            {
                var interestDb = context.Interests.Find(userInterest.InterestId);

                if (interestDb is null)
                {
                    continue;
                }

                userInterestDtos.Add(new UserInterestDto
                {
                    Interest = new InterestDto
                    {
                        Name = interestDb.Name
                    },
                    Level = userInterest.Level
                });
            }

            var userDto = new UserDto
            {
                Username = user.Username,
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                Phone = user.Phone,
                Role = user.Role,
                AddressId = user.AddressId,
                Interests = userInterestDtos
            };

            return userDto;
        }

        public IEnumerable<UserDto?> GetUsersByFilter(Expression<Func<User, bool>> filter)
        {
            var users = context.Users.Where(filter).ToList();
            var usersDto = new List<UserDto>();

            foreach (var user in users)
            {
                var userInterests = context.UserInterests
                                           .Where(ui => ui.UserId == user.Id)
                                           .ToList();

                if (userInterests.Count == 0)
                {
                    usersDto.Add(MapToUserDto(user));
                    continue;
                }

                var userInterestDtos = new List<UserInterestDto>();

                foreach (var userInterest in userInterests)
                {
                    var interestDb = context.Interests.Find(userInterest.InterestId);

                    if (interestDb is null)
                    {
                        continue;
                    }

                    userInterestDtos.Add(new UserInterestDto
                    {
                        Interest = new InterestDto
                        {
                            Name = interestDb.Name
                        },
                        Level = userInterest.Level
                    });
                }

                var userDto = new UserDto
                {
                    Username = user.Username,
                    Name = user.Name,
                    Surname = user.Surname,
                    Email = user.Email,
                    Phone = user.Phone,
                    Role = user.Role,
                    AddressId = user.AddressId,
                    Interests = userInterestDtos
                };

                usersDto.Add(userDto);
            }

            return usersDto.AsEnumerable();
        }

        public IEnumerable<UserDto?> GetAllUsers()
        {
            var users = context.Users.ToList();
            var usersDto = new List<UserDto>();

            foreach (var user in users)
            {
                var userInterests = context.UserInterests
                                           .Where(ui => ui.UserId == user.Id)
                                           .ToList();

                if (userInterests.Count == 0)
                {
                    usersDto.Add(MapToUserDto(user));
                    continue;
                }

                var userInterestDtos = new List<UserInterestDto>();

                foreach (var userInterest in userInterests)
                {
                    var interestDb = context.Interests.Find(userInterest.InterestId);

                    if (interestDb is null)
                    {
                        continue;
                    }

                    userInterestDtos.Add(new UserInterestDto
                    {
                        Interest = new InterestDto
                        {
                            Name = interestDb.Name
                        },
                        Level = userInterest.Level
                    });
                }

                var userDto = new UserDto
                {
                    Username = user.Username,
                    Name = user.Name,
                    Surname = user.Surname,
                    Email = user.Email,
                    Phone = user.Phone,
                    Role = user.Role,
                    AddressId = user.AddressId,
                    Interests = userInterestDtos
                };

                usersDto.Add(userDto);
            }

            return usersDto.AsEnumerable();
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
                Role = user.Role,
                AddressId = user.AddressId,
                Interests = []
            };

            return userDto;
        }
    }
}
