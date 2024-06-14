using Microsoft.EntityFrameworkCore;
using ReasnAPI.Exceptions;
using ReasnAPI.Models.Database;
using ReasnAPI.Models.DTOs;
using System.Linq.Expressions;
using System.Transactions;
using System.Text.RegularExpressions;
using ReasnAPI.Mappers;

namespace ReasnAPI.Services;
public class EventService(ReasnContext context, ParameterService parameterService, TagService tagService, CommentService commentService)
{

    public EventDto CreateEvent(EventDto eventDto)
    {
        using (var scope = new TransactionScope())
        {
            var createdTime = DateTime.UtcNow;
            var newEvent = eventDto.ToEntity();
            newEvent.CreatedAt = createdTime;
            newEvent.UpdatedAt = createdTime;
            newEvent.Slug = CreateSlug(eventDto);

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
            eventToUpdate.UpdatedAt = DateTime.UtcNow;
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
        tagService.AddTagsFromList(tagsToAdd);

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

        tagService.RemoveTagsNotInAnyEvent();
    }

    private void AttachParametersToEvent(List<Parameter> parametersToAdd, Event eventToUpdate)
    {
        parameterService.AddParametersFromList(parametersToAdd);

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

        parameterService.RemoveParametersNotInAnyEvent();
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

            var parametersToRemove = eventToDelete.Parameters.ToList();
            var tagsToRemove = eventToDelete.Tags.ToList();

            DetachParametersFromEvent(parametersToRemove, eventToDelete);
            DetachTagsFromEvent(tagsToRemove, eventToDelete);

            RemoveCommentsFromEventCollection(eventToDelete);
            RemoveParticipantsFromEventCollection(eventToDelete);

            context.Events.Remove(eventToDelete);
            context.SaveChanges();
            scope.Complete();
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

    public Event GetEventBySlug(string slug)
    {
        var eventToReturn = context.Events.Include(e => e.Tags).Include(e => e.Parameters).FirstOrDefault(e => e.Slug == slug);
        if (eventToReturn is null)
        {
            throw new NotFoundException("Event not found");
        }


        return eventToReturn;
    }

    public IEnumerable<ParticipantDto> GetEventParticipantsBySlug(string slug)
    {
        var eventToReturn = context.Events.Include(e => e.Participants).FirstOrDefault(e => e.Slug == slug);
        if (eventToReturn is null)
        {
            throw new NotFoundException("Event not found");
        }

        var participantsDto = eventToReturn.Participants.Select(p => p.ToDto());

        return participantsDto;
    }

    public IEnumerable<CommentDto> GetEventCommentsBySlug(string slug)
    {
        var eventToReturn = context.Events.Include(e => e.Comments)
            .FirstOrDefault(e => e.Slug == slug);
        if (eventToReturn is null)
        {
            throw new NotFoundException("Event not found");
        }

        var commentDtos = eventToReturn.Comments.Select(p => p.ToDto());

        return commentDtos;
    }

    public void AddEventComment(CommentDto commentDto, string slug)
    {
        commentDto = commentService.CreateComment(commentDto);
        var relatedEvent = GetEventBySlug(slug);
        relatedEvent.Comments.Add(commentDto.ToEntity());
        context.SaveChanges();
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

    private string CreateSlug(EventDto eventDto)
    {
        var baseSlug = eventDto.Name.ToLower();
        baseSlug = baseSlug.Trim();
        baseSlug = Regex.Replace(baseSlug, @"\s+", " ");
        baseSlug = Regex.Replace(baseSlug, " ", "-");
        baseSlug = Regex.Replace(baseSlug, @"[^a-z0-9-]", "");
        baseSlug = Regex.Replace(baseSlug, "-+", "-");

        var existingSlugs = context.Events
            .Where(e => e.Slug.StartsWith(baseSlug))
            .Select(e => e.Slug)
            .ToList();

        var counter = 1;
        if (existingSlugs.Any())
        {
            var regex = new Regex($"^{Regex.Escape(baseSlug)}-(\\d+)$");
            foreach (var slug in existingSlugs)
            {
                var match = regex.Match(slug);
                if (match.Success)
                {
                    var currentCounter = int.Parse(match.Groups[1].Value);
                    counter = Math.Max(counter, currentCounter + 1);
                }
            }
        }

        var countLength = counter.ToString().Length;
        var maxBaseSlugLength = 128 - countLength - 1;
        if (baseSlug.Length > maxBaseSlugLength)
        {
            baseSlug = baseSlug.Substring(0, maxBaseSlugLength);
        }

        var finalSlug = $"{baseSlug}-{counter}";

        return finalSlug;
    }

}