using ReasnAPI.Models.Database;
using ReasnAPI.Models.DTOs;

namespace ReasnAPI.Mappers;

public static class UserInterestMapper
{
    public static UserInterestDto ToDto(this UserInterest userInterest)
    {
        return new UserInterestDto
        {
            Interest = userInterest.Interest.ToDto(),
            Level = userInterest.Level
        };
    }

    public static List<UserInterestDto> ToDtoList(this IEnumerable<UserInterest> userInterests)
    {
        return userInterests.Select(ToDto).ToList();
    }

    public static UserInterest ToEntity(this UserInterestDto userInterestDto)
    {
        return new UserInterest
        {
            Interest = userInterestDto.Interest.ToEntity(),
            Level = userInterestDto.Level
        };
    }
}