using ReasnAPI.Models.Database;
using ReasnAPI.Models.DTOs;
using System.Linq;
using System.Linq.Expressions;
using System.Transactions;
using Microsoft.EntityFrameworkCore;

namespace ReasnAPI.Services;
public class TagService (ReasnContext context)
{
    public TagDto? CreateTag(TagDto tagDto)
    {
        var tag = context.Tags.FirstOrDefault(r => r.Name == tagDto.Name);
        if (tag is not null)
        {
            return null;
        }

        var newTag = new Tag
        {
            Name = tagDto.Name
        };
        context.Tags.Add(newTag);
        context.SaveChanges();
        return tagDto;
    }

    public TagDto? UpdateTag(int tagId, TagDto tagDto, int eventId)
    {
        using (var scope = new TransactionScope())
        {
            var tag = context.Tags.FirstOrDefault(r => r.Id == tagId);

            if (tag is null)
            {
                return null;
            }

            var eventsWithTags = context.Events.Include(e => e.Tags).ToList();

            var eventTags = eventsWithTags.Where(e => e.Tags.Any(t => t.Id == tagId) && e.Id != eventId).ToList();
            if (eventTags.Any()) // if tag is associated with more than one event
            {
                // Create new tag, associate it with the event, and remove the old association
                var newTag = new Tag
                {
                    Name = tagDto.Name
                };
                context.Tags.Add(newTag);
                context.SaveChanges();

                var eventToUpdate = eventsWithTags.FirstOrDefault(e => e.Id == eventId);
                if (eventToUpdate != null)
                {
                    eventToUpdate.Tags.Remove(tag);
                    eventToUpdate.Tags.Add(newTag);
                }
            }
            else if (eventTags.Count == 1 && eventTags[0].Id == eventId) // if tag is associated only with the same event
            {
                tag.Name = tagDto.Name;
                context.Tags.Update(tag);
            }
            else
            {
                return null;
            }

            context.SaveChanges();
            scope.Complete();
            return tagDto;
        }
    }

    public bool DeleteTag(int tagId)
    {
        var tag = context.Tags.FirstOrDefault(r => r.Id == tagId);

        if (tag == null)
        {
            return false;
        }

        var eventsWithTags = context.Events.Include(e => e.Tags).ToList();

        var isTagAssociatedWithEvent = eventsWithTags.Any(e => e.Tags.Any(t => t.Id == tagId));

        if (isTagAssociatedWithEvent) 
        {
            return false;
        }

        context.Tags.Remove(tag);
        context.SaveChanges();

        return true;
    }

    public TagDto? GetTagById(int tagId)
    {
        var tag = context.Tags.Find(tagId);
        if(tag is null)
        {
            return null;
        }

        return new TagDto
        {
            Name = tag.Name
        };
    }

    public IEnumerable<TagDto> GetAllTags()
    {
        var tags = context.Tags.ToList();
        return tags.Select(tag => new TagDto { Name = tag.Name }).AsEnumerable();
    }

    public IEnumerable<TagDto> GetTagsByFilter(Expression<Func<Tag, bool>> filter)
    {
        var tags = context.Tags.Where(filter).ToList();
        return tags.Select(tag => new TagDto { Name = tag.Name }).AsEnumerable();
    }

}