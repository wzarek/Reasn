using Microsoft.EntityFrameworkCore;
using ReasnAPI.Models.Database;
using ReasnAPI.Models.DTOs;
using System.Linq.Expressions;
using System.Linq;
using System.Transactions;

namespace ReasnAPI.Services;
public class EventService(ReasnContext context)
{
    public EventDto CreateEvent(EventDto eventDto)
    {
        using (var scope = new TransactionScope())
        {
            eventDto.Slug = CreateSlug(eventDto);

            var newEvent = new Event
            {
                Name = eventDto.Name,
                AddressId = eventDto.AddressId,
                Description = eventDto.Description,
                OrganizerId = eventDto.OrganizerId,
                StartAt = eventDto.StartAt,
                EndAt = eventDto.EndAt,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Slug = eventDto.Slug,
                StatusId = eventDto.StatusId,
            };

            context.Events.Add(newEvent);
            context.SaveChanges();

            var eventId = newEvent.Id;
            if (eventDto.Tags == null)
            {
                return eventDto;
            }

            foreach (var tag in eventDto.Tags)
            {
                var newTag = context.Tags.FirstOrDefault(r => r.Name == tag.Name);
                if (newTag == null)
                {
                    var tagToAdd = new Tag { Name = tag.Name };
                    context.Tags.Add(tagToAdd);
                    context.SaveChanges();

                    var eventTag = new EventTag
                    {
                        EventId = eventId,
                        TagId = tagToAdd.Id
                    };
                    context.EventTags.Add(eventTag);
                    context.SaveChanges();
                }
                else
                {
                    var eventTag = new EventTag
                    {
                        EventId = eventId,
                        TagId = newTag.Id
                    };
                    context.EventTags.Add(eventTag);
                    context.SaveChanges();
                }
            }
        }

        return eventDto;
    }

    public EventDto UpdateEvent(int eventId, EventDto eventDto)
    {
        using (var scope = new TransactionScope())
        {
            var eventToUpdate = context.Events.FirstOrDefault(r => r.Id == eventId);
            if (eventToUpdate == null)
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
            eventToUpdate.StatusId = eventDto.StatusId;

            context.Events.Update(eventToUpdate);
            context.SaveChanges();

            var existingTags = context.EventTags.Where(r => r.EventId == eventId).Include(eventTag => eventTag.Tag).ToList();

            if (eventDto.Tags == null)
            {
                return eventDto;
            }

            var newTags = eventDto.Tags
                .Select(tagDto => new Tag
                {
                    Name = tagDto.Name,
                }).ToList();

            var tagsToRemove = from existingTag in existingTags
                               where newTags.All(r => r.Name != existingTag.Tag.Name)
                               select existingTag;

            foreach (var existingTag in tagsToRemove)
            {
                context.EventTags.Remove(existingTag);
            }

            var tagsToAdd = from newTag in newTags
                            where existingTags.All(r => r.Tag.Name != newTag.Name)
                            select newTag;

            foreach (var newTag in tagsToAdd)
            {
                var existingTag = context.Tags.FirstOrDefault(t => t.Name == newTag.Name);
                if (existingTag == null)
                {
                    existingTag = new Tag { Name = newTag.Name };
                    context.Tags.Add(existingTag);
                    context.SaveChanges();
                }

                context.EventTags.Add(new EventTag { EventId = eventId, TagId = existingTag.Id });
            }

            context.SaveChanges();
        }

        return eventDto;
    }

    public bool DeleteEvent(int eventId)
    {
        using (var scope = new TransactionScope())
        {
            var eventToDelete = context.Events.FirstOrDefault(r => r.Id == eventId);

            if (eventToDelete == null)
            {
                return false;
            }

            context.EventTags.RemoveRange(context.EventTags.Where(r => r.EventId == eventId));
            context.Events.Remove(eventToDelete);

            context.SaveChanges();
        }

        return true;
    }

    public EventDto GetEventById(int eventId)
    {
        var eventToReturn = context.Events.FirstOrDefault(r => r.Id == eventId);
        if (eventToReturn == null)
        {
            return null;
        }

        var tagIds = context.EventTags
            .Where(r => r.EventId == eventId)
            .Select(r => r.TagId)
            .ToList();

        var tags = context.Tags
            .Where(t => tagIds.Contains(t.Id))
            .Select(t => new TagDto { Name = t.Name })
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
            StatusId = eventToReturn.StatusId,
            Tags = tags
        };

        return eventDto;
    }

    public IEnumerable<EventDto> GetEventsByFilter(Expression<Func<Event, bool>> filter)
    {
        var events = context.Events.Where(filter).ToList();
        var eventDtos = new List<EventDto>();
        foreach (var eventToReturn in events)
        {
            var tagIds = context.EventTags
                .Where(r => r.EventId == eventToReturn.Id)
                .Select(r => r.TagId)
                .ToList();

            var tags = context.Tags
                .Where(t => tagIds.Contains(t.Id))
                .Select(t => new TagDto { Name = t.Name })
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
                StatusId = eventToReturn.StatusId,
                Tags = tags
            };

            eventDtos.Add(eventDto);
        }

        return eventDtos;
    }

    public IEnumerable<EventDto> GetAllEvents()
    {
        var events = context.Events.ToList();
        var eventDtos = new List<EventDto>();
        foreach (var eventToReturn in events)
        {
            var tagIds = context.EventTags
                .Where(r => r.EventId == eventToReturn.Id)
                .Select(r => r.TagId)
                .ToList();

            var tags = context.Tags
                .Where(t => tagIds.Contains(t.Id))
                .Select(t => new TagDto { Name = t.Name })
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
                StatusId = eventToReturn.StatusId,
                Tags = tags
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