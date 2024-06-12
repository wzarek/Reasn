using ReasnAPI.Models.Database;
using ReasnAPI.Models.DTOs;
using System.Linq.Expressions;
using System.Transactions;
using Microsoft.EntityFrameworkCore;
using ReasnAPI.Exceptions;
using ReasnAPI.Mappers;

namespace ReasnAPI.Services;
public class TagService (ReasnContext context)
{
    public TagDto CreateTag(TagDto tagDto)
    {
        var tag = context.Tags.FirstOrDefault(r => r.Name == tagDto.Name);
        if (tag is not null)
        {
            throw new BadRequestException("Tag already exists");
        }

        context.Tags.Add(tagDto.ToEntity());
        context.SaveChanges();
        return tagDto;
    }

    public TagDto UpdateTag(int tagId, TagDto tagDto)
    {
        var tag = context.Tags.FirstOrDefault(r => r.Id == tagId);

        if (tag is null)
        {
            throw new NotFoundException("Tag not found");
        }

        tag.Name = tagDto.Name;
        context.Tags.Update(tag);
        context.SaveChanges();

        return tagDto;
    }

    public void AddTagsFromList(List<Tag> tagsToAdd)
    {
        var existingTagsInDb = context.Tags
            .Where(tag => tagsToAdd.Any(newTag => newTag.Name == tag.Name))
            .ToList();

        var tagsToAddToDb = tagsToAdd
            .Where(newTag => existingTagsInDb.All(existingTag => existingTag.Name != newTag.Name))
            .ToList();

        context.Tags.AddRange(tagsToAddToDb);
        context.SaveChanges();
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
            throw new BadRequestException("Tag is associated with an event");
        }

        context.Tags.Remove(tag);
        context.SaveChanges();
    }

    public void ForceDeleteTag(int tagId)
    {
        var tag = context.Tags.FirstOrDefault(r => r.Id == tagId);
        if (tag is null)
        {
            throw new NotFoundException("Tag not found");
        }

        var eventsWithTags = context.Events.Include(e => e.Tags).ToList();
        foreach (var eventWithTags in eventsWithTags)
        {
            eventWithTags.Tags.Remove(tag);
        }

        context.Tags.Remove(tag);
        context.SaveChanges();
    }

    public void RemoveTagsNotInAnyEvent()
    {
        var tagsNotInAnyEvent = context.Tags
            .Where(t => !context.Events.Any(e => e.Tags.Contains(t)))
            .ToList();

        context.Tags.RemoveRange(tagsNotInAnyEvent);
        context.SaveChanges();
    }

    public TagDto GetTagById(int tagId)
    {
        var tag = context.Tags.Find(tagId);
        if(tag is null)
        {
            throw new NotFoundException("Tag not found");
        }

        return tag.ToDto();
    }

    public IEnumerable<TagDto> GetAllTags()
    {
        return context.Tags
            .ToDtoList()
            .AsEnumerable();
    }

    public IEnumerable<TagDto> GetTagsByFilter(Expression<Func<Tag, bool>> filter)
    {
        return context.Tags
            .Where(filter)
            .ToDtoList()
            .AsEnumerable();
    }
}