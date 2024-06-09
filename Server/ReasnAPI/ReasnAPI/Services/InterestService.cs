using ReasnAPI.Models.Database;
using ReasnAPI.Models.DTOs;
using ReasnAPI.Services.Exceptions;
using System.Linq.Expressions;

namespace ReasnAPI.Services;

public class InterestService(ReasnContext context)
{
    public InterestDto CreateInterest(InterestDto interestDto)
    {
        var interest = context.Interests.FirstOrDefault(r => r.Name == interestDto.Name);
        if (interest is not null)
        {
            throw new ObjectExistsException("Interest already exists");
        }

        var newInterest = MapInterestFromInterestDto(interestDto);

        context.Interests.Add(newInterest);
        context.SaveChanges();
        return interestDto;
    }

    public InterestDto UpdateInterest(int interestId, InterestDto interestDto)
    {
        var interest = context.Interests.FirstOrDefault(r => r.Id == interestId);
        if (interest is null)
        {
            throw new NotFoundException("Interest not found");
        }

        interest.Name = interestDto.Name;

        context.Interests.Update(interest);
        context.SaveChanges();
        return interestDto;
    }

    public void DeleteInterest(int id)
    {
        var interest = context.Interests.FirstOrDefault(r => r.Id == id);
        if (interest is null)
        {
            throw new NotFoundException("Interest not found");
        }

        var eventInterest = context.UserInterests.FirstOrDefault(r => r.InterestId == id);
        if (eventInterest is not null) 
        {
            throw new ObjectInUseException("Interest is in use");
        }

        context.Interests.Remove(interest);
        context.SaveChanges();
    }

    public InterestDto GetInterestById(int interestId)
    {
        var interest = context.Interests.Find(interestId);
        if (interest is null)
        {
            throw new NotFoundException("Interest not found");
        }

        var interestDto = MapInterestDtoFromInterest(interest);

        return interestDto;
    }

    public IEnumerable<InterestDto> GetAllInterests()
    {
        var interests = context.Interests.ToList();

        return interests.Select(interest => MapInterestDtoFromInterest(interest)).AsEnumerable();
    }

    public IEnumerable<InterestDto> GetInterestsByFilter(Expression<Func<Interest, bool>> filter)
    {
        var interests = context.Interests.Where(filter).ToList();

        var interestDtos = interests.Select(interest => MapInterestDtoFromInterest(interest)).AsEnumerable();

        return interestDtos;
    }

    private InterestDto MapInterestDtoFromInterest(Interest interest)
    {
        return new InterestDto
        {
            Name = interest.Name
        };
    }

    private Interest MapInterestFromInterestDto(InterestDto interestDto)
    {
        return new Interest
        {
            Name = interestDto.Name
        };
    }

}