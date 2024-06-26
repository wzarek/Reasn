﻿using ReasnAPI.Models.Database;
using ReasnAPI.Models.DTOs;
using System.Linq.Expressions;
using System.Transactions;
using Microsoft.EntityFrameworkCore;
using ReasnAPI.Exceptions;
using ReasnAPI.Mappers;

namespace ReasnAPI.Services;
public class TagService(ReasnContext context)
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

    public void AttatchTagsToEvent(List<Tag> tagsToAdd, Event eventToUpdate)
    {
        var tagNamesToAdd = tagsToAdd.Select(t => t.Name).ToList();

        var tagsFromDb = context.Tags.Where(tag => tagNamesToAdd.Contains(tag.Name)).ToList();

        tagsFromDb.ForEach(eventToUpdate.Tags.Add);

        var newTagsToAdd = tagsToAdd.Where(t => tagsFromDb.All(dbTag => dbTag.Name != t.Name)).ToList();

        newTagsToAdd.ForEach(eventToUpdate.Tags.Add);
        context.SaveChanges();
    }

    public void DeleteTag(int tagId)
    {
        var tag = context.Tags.FirstOrDefault(r => r.Id == tagId);

        if (tag is null)
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

    public void ForceDeleteTag(TagDto tagDto)
    {
        if (tagDto is null)
        {
            throw new ArgumentException("No tag provided");
        }

        var tag = context.Tags.FirstOrDefault(r => r.Name == tagDto.Name);
        if (tag is null)
        {
            throw new NotFoundException("Tag not found");
        }

        var eventsWithTags = context.Events
            .Where(e => e.Tags.Any(p => p.Name == tagDto.Name))
            .Include(e => e.Tags)
            .ToList();

        foreach (var eventWithTag in eventsWithTags)
        {
            eventWithTag.Tags.Remove(tag);
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
        if (tag is null)
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

    public IEnumerable<string> GetAllTagsNames()
    {
        return context.Tags
            .Select(t => t.Name)
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