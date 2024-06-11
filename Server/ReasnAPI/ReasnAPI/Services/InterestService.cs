using ReasnAPI.Exceptions;
using ReasnAPI.Models.Database;
using ReasnAPI.Models.DTOs;
using System.Linq.Expressions;
using ReasnAPI.Mappers;

namespace ReasnAPI.Services;

public class InterestService(ReasnContext context)
{
    public InterestDto CreateInterest(InterestDto interestDto)
    {
        var interest = context.Interests.FirstOrDefault(r => r.Name == interestDto.Name);
        if (interest is not null)
        {
            throw new BadRequestException("Interest already exists");
        }

        var newInterest = interestDto.ToEntity();

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

        var userInterest = context.UserInterests.FirstOrDefault(r => r.InterestId == id);
        if (userInterest is not null)
        {
            throw new BadRequestException("Interest is in use");
        }

        context.Interests.Remove(interest);
        context.SaveChanges();
    }

    public void ForceDeleteInterest(int id)
    {
        var interest = context.Interests.FirstOrDefault(r => r.Id == id);
        if (interest is null)
        {
            throw new NotFoundException("Interest not found");
        }

        var userInterests = context.UserInterests.Where(r => r.InterestId == id).ToList();
        context.UserInterests.RemoveRange(userInterests);

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

        return interest.ToDto();
    }

    public IEnumerable<InterestDto> GetAllInterests()
    {
        return context.Interests
            .ToDtoList()
            .AsEnumerable();
    }

    public IEnumerable<InterestDto> GetInterestsByFilter(Expression<Func<Interest, bool>> filter)
    {
        return context.Interests
            .Where(filter)
            .ToDtoList()
            .AsEnumerable();
    }

}