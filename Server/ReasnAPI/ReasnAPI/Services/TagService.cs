using ReasnAPI.Models.Database;
using ReasnAPI.Models.DTOs;
using System.Linq;
using System.Linq.Expressions;
using System.Transactions;
using Microsoft.EntityFrameworkCore;
using ReasnAPI.Services.Exceptions;

namespace ReasnAPI.Services;
public class TagService (ReasnContext context)
{
    public TagDto CreateTag(TagDto tagDto)
    {
        var tag = context.Tags.FirstOrDefault(r => r.Name == tagDto.Name);
        if (tag is not null)
        {
            throw new ObjectExistsException("Tag already exists");
        }

        var newTag = MapTagFromTagDto(tagDto);
        context.Tags.Add(newTag);
        context.SaveChanges();
        return tagDto;
    }

    public TagDto UpdateTag(int tagId, TagDto tagDto, int eventId)
    {
        using (var scope = new TransactionScope())
        {
            var tag = context.Tags.FirstOrDefault(r => r.Id == tagId);

            if (tag is null)
            {
                throw new NotFoundException("Tag not found");
            }

            var eventsWithTags = context.Events.Include(e => e.Tags).ToList();

            var eventTags = eventsWithTags.Where(e => e.Tags.Any(t => t.Id == tagId)).ToList();
            var eventToUpdate = eventsWithTags.FirstOrDefault(e => e.Id == eventId);

            if (eventTags.Count > 1 || (eventTags.Count == 1 && eventTags[0].Id != eventId))
            {
                // Create new tag, associate it with the event, and remove the old association
                var newTag = MapTagFromTagDto(tagDto);
                context.Tags.Add(newTag);
                context.SaveChanges();

                if (eventToUpdate != null)
                {
                    eventToUpdate.Tags.Remove(tag);
                    eventToUpdate.Tags.Add(newTag);
                    context.Events.Update(eventToUpdate);
                }
            }
            else if (eventTags.Count == 1 && eventTags[0].Id == eventId)
            {
                tag.Name = tagDto.Name;
                context.Tags.Update(tag);
            }
            else
            {
                throw new NotFoundException("Tag not found");
            }

            context.SaveChanges();
            scope.Complete();
            return tagDto;
        }
    }

    public void DeleteTag(int tagId)
    {
        var tag = context.Tags.FirstOrDefault(r => r.Id == tagId);

        if (tag == null)
        {
            throw new NotFoundException("Tag not found");
        }

        var eventsWithTags = context.Events.Include(e => e.Tags).ToList();

        var isTagAssociatedWithEvent = eventsWithTags.Any(e => e.Tags.Any(t => t.Id == tagId));

        if (isTagAssociatedWithEvent) 
        {
            throw new ObjectInUseException("Tag is associated with an event");
        }

        context.Tags.Remove(tag);
        context.SaveChanges();
    }

    public TagDto GetTagById(int tagId)
    {
        var tag = context.Tags.Find(tagId);
        if(tag is null)
        {
            throw new NotFoundException("Tag not found");
        }

        return MapTagDtoFromTag(tag);
    }

    public IEnumerable<TagDto> GetAllTags()
    {
        var tags = context.Tags.ToList();
        return tags.Select(tag => MapTagDtoFromTag(tag)).AsEnumerable();
    }

    public IEnumerable<TagDto> GetTagsByFilter(Expression<Func<Tag, bool>> filter)
    {
        var tags = context.Tags.Where(filter).ToList();
        return tags.Select(tag => MapTagDtoFromTag(tag)).AsEnumerable();
    }

    private TagDto MapTagDtoFromTag(Tag tag)
    {
        return new TagDto
        {
            Name = tag.Name
        };
    }

    private Tag MapTagFromTagDto(TagDto tagDto)
    {
        return new Tag
        {
            Name = tagDto.Name
        };
    }

}