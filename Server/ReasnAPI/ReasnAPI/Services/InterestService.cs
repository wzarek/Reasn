using ReasnAPI.Models.Database;
using ReasnAPI.Models.DTOs;
using System.Linq.Expressions;

namespace ReasnAPI.Services;

public class InterestService(ReasnContext context)
{
    public InterestDto CreateInterest(InterestDto interestDto)
    {
        var interest = context.Interests.FirstOrDefault(r => r.Name == interestDto.Name);
        if (interest != null)
        {
            return null;
        }

        var newInterest = new Interest
        {
            Name = interestDto.Name
        };

        context.Interests.Add(newInterest);
        context.SaveChanges();
        return interestDto;
    }

    public InterestDto UpdateInterest(int interestId, InterestDto interestDto)
    {
        var interest = context.Interests.FirstOrDefault(r => r.Id == interestId);
        if (interest == null)
        {
            return null;
        }

        interest.Name = interestDto.Name;

        context.Interests.Update(interest);
        context.SaveChanges();
        return interestDto;
    }
    public bool DeleteInterest(int id)
    {
        var interest = context.Interests.FirstOrDefault(r => r.Id == id);

        if (interest == null)
        {
            return false;
        }
        context.Interests.Remove(interest);
        context.SaveChanges();

        return true;
    }

    public InterestDto GetInterestById(int interestId)
    {
        var interest = context.Interests.FirstOrDefault(r => r.Id == interestId);
        if (interest == null)
        {
            return null;
        }

        var interestDto = new InterestDto
        {
            Name = interest.Name
        };

        return interestDto;
    }

    public IEnumerable<InterestDto> GetAllInterests()
    {
        var interests = context.Interests.ToList();

        return interests.Select(interest => new InterestDto { Name = interest.Name }).ToList();
    }

    public IEnumerable<InterestDto> GetInterestsByFilter(Expression<Func<Interest, bool>> filter)
    {
        var interests = context.Interests.Where(filter).ToList();

        var interestDtos = interests.Select(interest => new InterestDto
        {
            Name = interest.Name
        }).ToList();

        return interestDtos;
    }

}