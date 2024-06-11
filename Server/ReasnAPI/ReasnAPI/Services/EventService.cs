﻿using Microsoft.EntityFrameworkCore;
using ReasnAPI.Exceptions;
using ReasnAPI.Models.Database;
using ReasnAPI.Models.DTOs;
using System.Linq.Expressions;
using System.Transactions;
using System.Text.RegularExpressions;
using ReasnAPI.Mappers;

namespace ReasnAPI.Services;
public class EventService(ReasnContext context)
{
    private readonly ParameterService _parameterService = new ParameterService(context);
    private readonly TagService _tagService = new TagService(context);

    public EventDto CreateEvent(EventDto eventDto)
    {
        using (var scope = new TransactionScope())
        {
            var createdTime = DateTime.UtcNow;
            var newEvent = eventDto.ToEntity();
            newEvent.CreatedAt = createdTime;
            newEvent.UpdatedAt = createdTime;
            newEvent.Slug = CreateSlug(eventDto, createdTime);

            context.Events.Add(newEvent);

            context.SaveChanges();
            if (eventDto.Tags != null)
            {
                var newTags = eventDto.Tags.ToEntityList();
                AttatchTagsToEvent(newTags, newEvent);
            }

            if (eventDto.Parameters != null)
            {
                var newParameters = eventDto.Parameters.ToEntityList();
                AttachParametersToEvent(newParameters, newEvent);
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
            var eventToUpdate = context.Events.Include(e => e.Tags).Include(e => e.Parameters).FirstOrDefault(e => e.Id == eventId);

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

            if (eventDto.Tags == null)
            {
                DetachTagsFromEvent(eventToUpdate.Tags.ToList(), eventToUpdate);
            }
            else
            {
                var newTags = eventDto.Tags.ToEntityList();
                var tagsToRemove = eventToUpdate.Tags
                    .Where(existingTag => newTags.All(newTag => newTag.Name != existingTag.Name))
                    .ToList();
                DetachTagsFromEvent(tagsToRemove, eventToUpdate);
                AttatchTagsToEvent(newTags, eventToUpdate);
            }

            if (eventDto.Parameters == null)
            {
                DetachParametersFromEvent(eventToUpdate.Parameters.ToList(), eventToUpdate);
            }
            else
            {
                var newParameters = eventDto.Parameters.ToEntityList();
                var paramsToRemove = eventToUpdate.Parameters
                    .Where(existingParam => newParameters.All(newParam => newParam.Key != existingParam.Key || newParam.Value != existingParam.Value))
                    .ToList();
                DetachParametersFromEvent(paramsToRemove, eventToUpdate);
                AttachParametersToEvent(newParameters, eventToUpdate);
            }

            context.Events.Update(eventToUpdate);

            context.SaveChanges();
            scope.Complete();
        }

        return eventDto;
    }

    private void AttatchTagsToEvent(List<Tag> tagsToAdd, Event eventToUpdate)
    {
        _tagService.AddTagsFromList(tagsToAdd);

        var tagsToAttach = tagsToAdd
            .Where(newTag => eventToUpdate.Tags.All(existingTag => existingTag.Name != newTag.Name))
            .ToList();

        tagsToAttach.ForEach(eventToUpdate.Tags.Add);

        context.SaveChanges();
    }

    private void DetachTagsFromEvent(List<Tag> tagsToRemove, Event eventToUpdate)
    {
        tagsToRemove.ForEach(tag => eventToUpdate.Tags.Remove(tag));
        context.SaveChanges();

        _tagService.RemoveTagsNotInAnyEvent();
    }

    private void AttachParametersToEvent(List<Parameter> parametersToAdd, Event eventToUpdate)
    {
        _parameterService.AddParametersFromList(parametersToAdd);

        var paramsToAttach = parametersToAdd
            .Where(newParam => eventToUpdate.Parameters.All(existingParam => existingParam.Key != newParam.Key || existingParam.Value != newParam.Value))
            .ToList();

        paramsToAttach.ForEach(eventToUpdate.Parameters.Add);

        context.SaveChanges();
    }

    private void DetachParametersFromEvent(List<Parameter> parametersToRemove, Event eventToUpdate)
    {
        parametersToRemove.ForEach(param => eventToUpdate.Parameters.Remove(param));
        context.SaveChanges();

        _parameterService.RemoveParametersNotInAnyEvent();
    }

    public void DeleteEvent(int eventId)
    {
        using (var scope = new TransactionScope())
        {
            var eventToDelete = context.Events.Include(e => e.Tags).Include(e => e.Parameters).Include(e => e.Participants).Include(e => e.Comments).FirstOrDefault(e => e.Id == eventId);

            if (eventToDelete is null)
            {
                throw new NotFoundException("Event not found");
            }

            RemoveTags(eventToDelete, eventId);
            RemoveParametersFromEventCollection(eventToDelete, eventId);
            RemoveCommentsFromEventCollection(eventToDelete);
            RemoveParticipantsFromEventCollection(eventToDelete);

            context.Events.Remove(eventToDelete);
            context.SaveChanges();
            scope.Complete();
        }
    }

    private void RemoveTags(Event eventToDelete, int eventId)
    {
        foreach (var tag in eventToDelete.Tags.ToList())
        {
            eventToDelete.Tags.Remove(tag); // remove the association first
            // Check if the tag is associated with any other event
            if (!context.Events.Any(e => e.Tags.Any(t => t.Name == tag.Name) && e.Id != eventId) && !context.Events.Any(e => e.Tags.Contains(tag)))
            {
                context.Tags.Remove(tag); // then remove the tag
            }
        }
    }

    private void RemoveParametersFromEventCollection(Event eventToDelete, int eventId)
    {
        foreach (var parameter in eventToDelete.Parameters.ToList())
        {
            eventToDelete.Parameters.Remove(parameter); // remove the association first
            // Check if the parameter is associated with any other event
            if (!context.Events.Any(e => e.Parameters.Any(p => p.Key == parameter.Key && p.Value == parameter.Value) && e.Id != eventId) && !context.Events.Any(e => e.Parameters.Contains(parameter)))
            {
                context.Parameters.Remove(parameter); // then remove the parameter
            }
        }
    }

    private void RemoveCommentsFromEventCollection(Event eventToDelete)
    {
        context.Comments.RemoveRange(eventToDelete.Comments);
    }

    private void RemoveParticipantsFromEventCollection(Event eventToDelete)
    {
        context.Participants.RemoveRange(eventToDelete.Participants);
    }

    public EventDto GetEventById(int eventId)
    {
        var eventToReturn = context.Events.Include(e => e.Tags).Include(e => e.Parameters).FirstOrDefault(e => e.Id == eventId);
        if (eventToReturn is null)
        {
            throw new NotFoundException("Event not found");
        }

        var eventDto = eventToReturn.ToDto();

        return eventDto;
    }

    public EventDto GetEventBySlug(string slug)
    {
        var eventToReturn = context.Events.Include(e => e.Tags).Include(e => e.Parameters).FirstOrDefault(e => e.Slug == slug);
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
        var eventDtos = events.ToDtoList();
        return eventDtos;
    }

    public IEnumerable<EventDto> GetAllEvents()
    {
        var events = context.Events.Include(e => e.Parameters).Include(e => e.Tags).ToList();

        var eventDtos = events.ToDtoList();
        return eventDtos;
    }
    public IEnumerable<EventDto> GetUserEvents(string username)
    {
        var user = context.Users.FirstOrDefault(u => u.Username == username);

        if (user is null)
        {
            throw new NotFoundException("User not found");
        }

        var userEvents = context.Participants.Include(p => p.Event).Where(p => p.UserId == user.Id).Select(p => p.Event);
        return userEvents.ToDtoList().AsEnumerable();
    }

    private string CreateSlug(EventDto eventDto, DateTime createdTime)
    {
        var slug = eventDto.Name.ToLower();
        slug = slug.Trim();
        slug = Regex.Replace(slug, @"\s+", " ");
        slug = Regex.Replace(slug, " ", "-");
        slug = Regex.Replace(slug, @"[^a-z0-9-]", "");
        slug = Regex.Replace(slug, "-+", "-");

        var timestamp = createdTime.Ticks.ToString();
        var maxSlugLength = 128 - timestamp.Length - 1; // -1 for the dash between slug and timestamp

        if (slug.Length > maxSlugLength)
        {
            slug = slug.Substring(0, maxSlugLength);
        }

        return $"{slug}-{timestamp}";
    }

}