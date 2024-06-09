using Microsoft.EntityFrameworkCore;
using ReasnAPI.Exceptions;
using ReasnAPI.Models.Database;
using ReasnAPI.Models.DTOs;
using System.Linq.Expressions;
using System.Transactions;
using ReasnAPI.Models.Enums;
using ReasnAPI.Models.Mappers;
using ReasnAPI.Exceptions;

namespace ReasnAPI.Services;
public class EventService(ReasnContext context)
{
    public EventDto CreateEvent(EventDto eventDto)
    {
        using (var scope = new TransactionScope())
        {
            eventDto.Slug = CreateSlug(eventDto);
            var nowTime = DateTime.UtcNow;
            var newEvent = eventDto.ToEntity();
            newEvent.CreatedAt = nowTime;
            newEvent.UpdatedAt = nowTime;
            newEvent.Slug = eventDto.Slug;

            context.Events.Add(newEvent);
            context.SaveChanges();

            var eventId = newEvent.Id;

            if (eventDto.Tags is not null && eventDto.Tags.Any())
            {
                var newTags = eventDto.Tags
                    .Where(t => !context.Tags.Any(x => x.Name == t.Name))
                    .Select(t => new Tag { Name = t.Name })
                    .ToList();

                context.Tags.AddRange(newTags);
                newEvent.Tags = newTags;
            }

            if (eventDto.Parameters is not null && eventDto.Parameters.Any())
            {
                var newParameters = eventDto.Parameters
                    .Where(p => !context.Parameters.Any(x => x.Key == p.Key && x.Value == p.Value))
                    .Select(p => new Parameter { Key = p.Key, Value = p.Value })
                    .ToList();

                context.Parameters.AddRange(newParameters);

                newEvent.Parameters = newParameters;
            }

            context.SaveChanges();
            scope.Complete();
        }

        return eventDto;
    }

    public EventDto UpdateEvent(int eventId, EventDto eventDto)
    {
        using (var scope = new TransactionScope())
        {
            var eventToUpdate = context.Events.FirstOrDefault(r => r.Id == eventId);

            if (eventToUpdate is null)
            {
                throw new NotFoundException("Event not found");
            }

            eventToUpdate.Name = eventDto.Name;
            eventToUpdate.AddressId = eventDto.AddressId;
            eventToUpdate.Description = eventDto.Description;
            eventToUpdate.OrganizerId = eventDto.OrganizerId;
            eventToUpdate.StartAt = eventDto.StartAt;
            eventToUpdate.EndAt = eventDto.EndAt;
            eventToUpdate.UpdatedAt = DateTime.Now;
            eventToUpdate.Status = eventDto.Status;

            context.Events.Update(eventToUpdate);
            context.SaveChanges();

            if (eventDto.Tags is not null)
            {

                var newTags = eventDto.Tags
                    .Select(tagDto => new Tag
                    {
                        Name = tagDto.Name,
                    }).ToList();

                var tagsToRemove = eventToUpdate.Tags
                    .Where(existingTag => newTags.All(newTag => newTag.Name != existingTag.Name))
                    .ToList();

                tagsToRemove.ForEach(tag => eventToUpdate.Tags.Remove(tag));

                var existingTagsInDb = context.Tags
                    .Where(tag => newTags.Any(newTag => newTag.Name == tag.Name))
                    .ToList();

                var tagsToAdd = newTags
                    .Where(newTag => existingTagsInDb.All(existingTag => existingTag.Name != newTag.Name))
                    .ToList();

                context.Tags.AddRange(tagsToAdd);
                context.SaveChanges();

                var tagsToAttach = newTags
                    .Where(newTag => eventToUpdate.Tags.All(existingTag => existingTag.Name != newTag.Name))
                    .ToList();

                tagsToAttach.ForEach(eventToUpdate.Tags.Add);

                context.SaveChanges();
            }

            if (eventDto.Parameters is not null)
            {
                var newParameters = eventDto.Parameters
                    .Select(paramDto => new Parameter
                    {
                        Key = paramDto.Key,
                        Value = paramDto.Value
                    }).ToList();

                var paramsToRemove = eventToUpdate.Parameters
                    .Where(existingParam => newParameters.All(newParam => newParam.Key != existingParam.Key || newParam.Value != existingParam.Value))
                    .ToList();

                paramsToRemove.ForEach(param => eventToUpdate.Parameters.Remove(param));

                var existingParamsInDb = context.Parameters
                    .Where(param => newParameters.Any(newParam => newParam.Key == param.Key && newParam.Value == param.Value))
                    .ToList();

                var paramsToAdd = newParameters
                    .Where(newParam => existingParamsInDb.All(existingParam => existingParam.Key != newParam.Key || existingParam.Value != newParam.Value))
                    .ToList();

                context.Parameters.AddRange(paramsToAdd);
                context.SaveChanges();

                var paramsToAttach = newParameters
                    .Where(newParam => eventToUpdate.Parameters.All(existingParam => existingParam.Key != newParam.Key || existingParam.Value != newParam.Value))
                    .ToList();

                paramsToAttach.ForEach(eventToUpdate.Parameters.Add);

                context.SaveChanges();
            }
            scope.Complete();
        }

        return eventDto;
    }

    public void DeleteEvent(int eventId)
    {
        using (var scope = new TransactionScope())
        {
            var eventToDelete = context.Events.FirstOrDefault(r => r.Id == eventId);

            if (eventToDelete is null)
            {
                throw new NotFoundException("Event not found");
            }

            context.Events.Remove(eventToDelete);
            context.SaveChanges();
            scope.Complete();
        }
    }

    public EventDto? GetEventById(int eventId)
    {
        var eventToReturn = context.Events.Include(e => e.Tags).Include(e => e.Parameters).FirstOrDefault(e => e.Id == eventId);
        if (eventToReturn is null)
        {
            throw new NotFoundException("Event not found");
        }

        var eventDto = eventToReturn.ToDto();

        return eventDto;
    }

    public IEnumerable<EventDto> GetEventsByFilter(Expression<Func<Event, bool>> filter)
    {
        var events = context.Events.Include(e => e.Parameters).Include(e => e.Tags).Where(filter).ToList();
        var eventDtos = new List<EventDto>();
        foreach (var eventToReturn in events)
        {
            var eventDto = eventToReturn.ToDto();

            eventDtos.Add(eventDto);
        }

        return eventDtos;
    }

    public IEnumerable<EventDto> GetAllEvents()
    {
        var events = context.Events.Include(e => e.Parameters).Include(e => e.Tags).ToList();

        var eventDtos = new List<EventDto>();
        foreach (var eventToReturn in events)
        {
            var eventDto = eventToReturn.ToDto();

            eventDtos.Add(eventDto);
        }

        return eventDtos;
    }

    private string CreateSlug(EventDto eventDto)
    {
        var slug = eventDto.Name.ToLower().Replace(" ", "-");
        return slug;
    }

}