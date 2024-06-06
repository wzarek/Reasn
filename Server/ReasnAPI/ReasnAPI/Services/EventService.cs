using Microsoft.EntityFrameworkCore;
using ReasnAPI.Models.Database;
using ReasnAPI.Models.DTOs;
using System.Linq.Expressions;
using System.Linq;
using System.Transactions;
using ReasnAPI.Models.Enums;

namespace ReasnAPI.Services;
public class EventService(ReasnContext context)
{
    public EventDto? CreateEvent(EventDto eventDto)
    {
        using (var scope = new TransactionScope())
        {
            eventDto.Slug = CreateSlug(eventDto);
            var nowTime = DateTime.UtcNow;
            var newEvent = new Event
            {
                Name = eventDto.Name,
                AddressId = eventDto.AddressId,
                Description = eventDto.Description,
                OrganizerId = eventDto.OrganizerId,
                StartAt = eventDto.StartAt,
                EndAt = eventDto.EndAt,
                CreatedAt = nowTime,
                UpdatedAt = nowTime,
                Slug = eventDto.Slug,
                Status = eventDto.Status
            };

            context.Events.Add(newEvent);
            context.SaveChanges();

            var eventId = newEvent.Id;

            var addedEvent = context.Events.Include(e => e.Tags).Include(e => e.Parameters)
                .FirstOrDefault(e => e.Id == eventId);

            if (addedEvent == null) return null;
            if (eventDto.Tags is not null && eventDto.Tags.Count > 0)
            {
                var newTags = eventDto.Tags
                    .Where(t => !context.Tags.Any(x => x.Name == t.Name))
                    .Select(t => new Tag { Name = t.Name })
                    .ToList();

                context.Tags.AddRange(newTags);
                addedEvent.Tags = newTags;
            }

            if (eventDto.Parameters is not null && eventDto.Parameters.Count > 0)
            {
                var newParameters = eventDto.Parameters
                    .Where(p => !context.Parameters.Any(x => x.Key == p.Key && x.Value == p.Value))
                    .Select(p => new Parameter { Key = p.Key, Value = p.Value })
                    .ToList();

                context.Parameters.AddRange(newParameters);

                addedEvent.Parameters = newParameters;
            }

            context.SaveChanges();
            scope.Complete();
        }

        return eventDto;
    }

    public EventDto? UpdateEvent(int eventId, EventDto eventDto)
    {
        using (var scope = new TransactionScope())
        {
            var eventToUpdate = context.Events.FirstOrDefault(r => r.Id == eventId);
            if (eventToUpdate is null)
            {
                return null;
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

            var existingEvent = context.Events
                .Include(e => e.Tags)
                .FirstOrDefault(e => e.Id == eventId);

            if (existingEvent is null)
            {
                return null;
            }

            if (eventDto.Tags is not null)
            {

                var newTags = eventDto.Tags
                    .Select(tagDto => new Tag
                    {
                        Name = tagDto.Name,
                    }).ToList();

                var tagsToRemove = existingEvent.Tags
                    .Where(existingTag => newTags.All(newTag => newTag.Name != existingTag.Name))
                    .ToList();

                foreach (var tagToRemove in tagsToRemove)
                {
                    existingEvent.Tags.Remove(tagToRemove);
                }

                var existingTagsInDb = context.Tags.ToList();
                var tagsToAdd = newTags
                    .Where(newTag => existingTagsInDb.All(existingTag => existingTag.Name != newTag.Name))
                    .ToList();

                context.Tags.AddRange(tagsToAdd);
                context.SaveChanges();

                var updatedTags = newTags
                    .Select(newTag => existingTagsInDb.FirstOrDefault(existingTag => existingTag.Name == newTag.Name) ??
                                      newTag)
                    .ToList();

                existingEvent.Tags = updatedTags;

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

                var paramsToRemove = existingEvent.Parameters
                    .Where(existingParam => newParameters.All(newParam => newParam.Key != existingParam.Key))
                    .ToList();

                foreach (var paramToRemove in paramsToRemove)
                {
                    existingEvent.Parameters.Remove(paramToRemove);
                }

                var existingParamsInDb = context.Parameters.ToList();
                var paramsToAdd = newParameters
                    .Where(newParam => existingParamsInDb.All(existingParam => existingParam.Key != newParam.Key))
                    .ToList();

                context.Parameters.AddRange(paramsToAdd);
                context.SaveChanges();

                var updatedParams = newParameters
                    .Select(newParam => existingParamsInDb.FirstOrDefault(existingParam => existingParam.Key == newParam.Key) ??
                                        newParam)
                    .ToList();

                existingEvent.Parameters = updatedParams;

                context.SaveChanges();
            }
            scope.Complete();
        }

        return eventDto;
    }

    public bool DeleteEvent(int eventId)
    {
        using (var scope = new TransactionScope())
        {
            var eventToDelete = context.Events.FirstOrDefault(r => r.Id == eventId);

            if (eventToDelete is null)
            {
                return false;
            }

            context.Events.Remove(eventToDelete);
            context.SaveChanges();
            scope.Complete();
        }

        return true;
    }

    public EventDto? GetEventById(int eventId)
    {
        var eventToReturn = context.Events.Include(e => e.Tags).Include(e => e.Parameters).FirstOrDefault(e => e.Id == eventId);
        if (eventToReturn is null)
        {
            return null;
        }

        var tags = eventToReturn.Tags
            .Select(t => new TagDto { Name = t.Name })
            .ToList();
        
        var parameters = eventToReturn.Parameters
            .Select(p => new ParameterDto { Key = p.Key, Value = p.Value })
            .ToList();

        var eventDto = new EventDto
        {
            Name = eventToReturn.Name,
            AddressId = eventToReturn.AddressId,
            Description = eventToReturn.Description,
            OrganizerId = eventToReturn.OrganizerId,
            StartAt = eventToReturn.StartAt,
            EndAt = eventToReturn.EndAt,
            CreatedAt = eventToReturn.CreatedAt,
            UpdatedAt = eventToReturn.UpdatedAt,
            Slug = eventToReturn.Slug,
            Status = eventToReturn.Status,
            Tags = tags,
            Parameters = parameters
        };

        return eventDto;
    }

    public IEnumerable<EventDto> GetEventsByFilter(Expression<Func<Event, bool>> filter)
    {
        var events = context.Events.Include(e => e.Parameters).Include(e => e.Tags).Where(filter).ToList();
        var eventDtos = new List<EventDto>();
        foreach (var eventToReturn in events)
        {
            var tags = eventToReturn.Tags
                .Select(t => new TagDto { Name = t.Name })
                .ToList();

            var parameters = eventToReturn.Parameters
                .Select(p => new ParameterDto { Key = p.Key, Value = p.Value })
                .ToList();

            var eventDto = new EventDto
            {
                Name = eventToReturn.Name,
                AddressId = eventToReturn.AddressId,
                Description = eventToReturn.Description,
                OrganizerId = eventToReturn.OrganizerId,
                StartAt = eventToReturn.StartAt,
                EndAt = eventToReturn.EndAt,
                CreatedAt = eventToReturn.CreatedAt,
                UpdatedAt = eventToReturn.UpdatedAt,
                Slug = eventToReturn.Slug,
                Status = eventToReturn.Status,
                Tags = tags,
                Parameters = parameters
            };

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
            var tags = eventToReturn.Tags
                .Select(t => new TagDto { Name = t.Name })
                .ToList();

            var parameters = eventToReturn.Parameters
                .Select(p => new ParameterDto { Key = p.Key, Value = p.Value })
                .ToList();

            var eventDto = new EventDto
            {
                Name = eventToReturn.Name,
                AddressId = eventToReturn.AddressId,
                Description = eventToReturn.Description,
                OrganizerId = eventToReturn.OrganizerId,
                StartAt = eventToReturn.StartAt,
                EndAt = eventToReturn.EndAt,
                CreatedAt = eventToReturn.CreatedAt,
                UpdatedAt = eventToReturn.UpdatedAt,
                Slug = eventToReturn.Slug,
                Status = eventToReturn.Status,
                Tags = tags,
                Parameters = parameters
            };

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