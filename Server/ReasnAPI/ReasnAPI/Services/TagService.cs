using ReasnAPI.Models.Database;
using ReasnAPI.Models.DTOs;
using System.Linq;
using System.Linq.Expressions;
using System.Transactions;

namespace ReasnAPI.Services;
public class TagService (ReasnContext context)
{
    public TagDto CreateTag(TagDto tagDto)
    {
        var tag = context.Tags.FirstOrDefault(r => r.Name == tagDto.Name);
        if (tag != null)
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

    public TagDto UpdateTag(int tagId, TagDto tagDto, int eventId)
    {
        using (var scope = new TransactionScope())
        {
            var tag = context.Tags.FirstOrDefault(r => r.Id == tagId);

            if (tag == null)
            {
                return null;
            }

            var eventTags = context.EventTags.Where(r => r.TagId == tagId).ToList();
            if (eventTags.Any(et => et.EventId != eventId)) // if tag is associated with more than one event
            {
                // Create new tag and event tag, and remove the old one
                var newTag = new Tag
                {
                    Name = tagDto.Name
                };
                context.Tags.Add(newTag);
                context.SaveChanges();

                var newEventTag = new EventTag
                {
                    EventId = eventId,
                    TagId = newTag.Id
                };
                context.EventTags.Add(newEventTag);

                var tagsToRemove = context.EventTags.Where(r => r.EventId == eventId && r.TagId == tagId).ToList();
                context.EventTags.RemoveRange(tagsToRemove);
            }
            else if (eventTags.Count == 1 &&
                     eventTags[0].EventId == eventId) // if tag is associated only with the same event
            {
                tag.Name = tagDto.Name;
                context.Tags.Update(tag);
            }
            else
            {
                return null;
            }

            context.SaveChanges();
            return tagDto;
        }
    }

    public bool DeleteTag(int tagId)
    {
        var tag = context.Tags.FirstOrDefault(r => r.Id == tagId);

        var eventTag = context.EventTags.FirstOrDefault(r => r.TagId == tagId);
        if (eventTag != null) // if tag is associated with an event, it cannot be deleted
        {
            return false;
        }

        if (tag == null)
        {
            return false;
        }
        context.Tags.Remove(tag);
        context.SaveChanges();

        return true;
    }

    public TagDto GetTagById(int tagId)
    {
        var tag = context.Tags.FirstOrDefault(r => r.Id == tagId);
        if(tag == null)
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
        return tags.Select(tag => new TagDto { Name = tag.Name }).ToList();
    }

    public IEnumerable<TagDto> GetTagsByFilter(Expression<Func<Tag, bool>> filter)
    {
        var tags = context.Tags.Where(filter).ToList();
        return tags.Select(tag => new TagDto { Name = tag.Name }).ToList();
    }

}