using ReasnAPI.Models.Database;
using ReasnAPI.Models.DTOs;

namespace ReasnAPI.Mappers;

public static class InterestMapper
{
    public static InterestDto ToDto(this Interest interest)
    {
        return new InterestDto
        {
            Name = interest.Name
        };
    }

    public static List<InterestDto> ToDtoList(this IEnumerable<Interest> interests)
    {
        return interests.Select(ToDto).ToList();
    }

    public static Interest ToEntity(this InterestDto interestDto)
    {
        return new Interest
        {
            Name = interestDto.Name
        };
    }
}