using ReasnAPI.Models.Database;
using ReasnAPI.Models.DTOs;
using System.Linq.Expressions;

namespace ReasnAPI.Services;

public class InterestService(ReasnContext context)
{
    public InterestDto? CreateInterest(InterestDto interestDto)
    {
        var interest = context.Interests.FirstOrDefault(r => r.Name == interestDto.Name);
        if (interest is not null)
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

    public InterestDto? UpdateInterest(int interestId, InterestDto interestDto)
    {
        var interest = context.Interests.FirstOrDefault(r => r.Id == interestId);
        if (interest is null)
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
        var eventInterest = context.UserInterests.FirstOrDefault(r => r.InterestId == id);
        if (eventInterest is not null) 
        {
            return false;
        }

        if (interest is null)
        {
            return false;
        }
        context.Interests.Remove(interest);
        context.SaveChanges();

        return true;
    }

    //public bool DeleteInterestFromUserInterests(int interestId, int userId)
    //{
    //    var userInterest = context.UserInterests.FirstOrDefault(r => r.InterestId == interestId && r.UserId == userId);
    //    if (userInterest == null)
    //    {
    //        return false;
    //    }

    //    context.UserInterests.Remove(userInterest);
    //    context.SaveChanges();

    //    return true;
    //}

    public InterestDto? GetInterestById(int interestId)
    {
        var interest = context.Interests.Find(interestId);
        if (interest is null)
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

        return interests.Select(interest => new InterestDto { Name = interest.Name }).AsEnumerable();
    }

    public IEnumerable<InterestDto> GetInterestsByFilter(Expression<Func<Interest, bool>> filter)
    {
        var interests = context.Interests.Where(filter).ToList();

        var interestDtos = interests.Select(interest => new InterestDto
        {
            Name = interest.Name
        }).AsEnumerable();

        return interestDtos;
    }

}