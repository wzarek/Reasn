using ReasnAPI.Models.Database;
using ReasnAPI.Models.DTOs;

namespace ReasnAPI.Mappers;

public static class UserMapper
{
    public static UserDto ToDto(this User user)
    {
        return new UserDto
        {
            Username = user.Username,
            Name = user.Name,
            Surname = user.Surname,
            Email = user.Email,
            Phone = user.Phone,
            Role = user.Role,
            AddressId = user.AddressId,
            Interests = user.UserInterests.ToDtoList()
        };
    }

    public static List<UserDto> ToDtoList(this IEnumerable<User> users)
    {
        return users.Select(ToDto).ToList();
    }
}