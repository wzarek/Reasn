using ReasnAPI.Models.Database;
using ReasnAPI.Models.DTOs;
using System.Linq.Expressions;

namespace ReasnAPI.Services;
public class StatusService (ReasnContext context) 
{
    public StatusDto CreateStatus(StatusDto statusDto)
    {
        var status = new Status
        {
            Name = statusDto.Name
        };

        context.Statuses.Add(status);
        context.SaveChanges();

        return statusDto;
    }

    public StatusDto UpdateStatus(int statusId,StatusDto statusDto)
    {
        var status = context.Statuses.FirstOrDefault(r => r.Id == statusId);
        if(status == null)
        {
            return null;
        }

        status.Name = statusDto.Name;

        context.Statuses.Update(status);
        context.SaveChanges();

        return statusDto;
    }

    public void DeleteStatus(int statusId)
    {
        var status = context.Statuses.FirstOrDefault(r => r.Id == statusId);

        if (status == null)
        {
            return;
        }
        context.Statuses.Remove(status);
        context.SaveChanges();

    }

    public StatusDto GetStatusById(int statusId)
    {
        var status = context.Statuses.FirstOrDefault(r => r.Id == statusId);
        if(status == null)
        {
            return null;
        }

        return new StatusDto
        {
            Name = status.Name
        };
    }

    public IEnumerable<StatusDto> GetAllStatuses()
    {
        var statuses = context.Statuses.ToList();

        return statuses.Select(status => new StatusDto { Name = status.Name }).ToList();
    }

    public IEnumerable<StatusDto> GetStatusesByFilter(Expression<Func<Status, bool>> filter)
    {
        var statuses = context.Statuses.Where(filter).ToList();

        return statuses.Select(status => new StatusDto { Name = status.Name }).ToList();



    }

}
