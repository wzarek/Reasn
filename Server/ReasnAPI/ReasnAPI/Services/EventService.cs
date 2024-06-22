using Microsoft.EntityFrameworkCore;
using ReasnAPI.Exceptions;
using ReasnAPI.Models.Database;
using ReasnAPI.Models.DTOs;
using System.Linq.Expressions;
using System.Transactions;
using System.Text.RegularExpressions;
using ReasnAPI.Mappers;
using ReasnAPI.Models.API;
using ReasnAPI.Models.Enums;

namespace ReasnAPI.Services;
public class EventService(ReasnContext context, ParameterService parameterService, TagService tagService, CommentService commentService, AddressService addressService, ImageService imageService)
{

    public EventDto CreateEvent(EventDto eventDto, AddressDto addressDto)
    {
        using (var scope = new TransactionScope())
        {
            var createdTime = DateTime.UtcNow;
            var newEvent = eventDto.ToEntity();
            newEvent.CreatedAt = createdTime;
            newEvent.UpdatedAt = createdTime;
            newEvent.Slug = CreateSlug(eventDto);

            addressService.CreateAddress(addressDto);
            newEvent.Address = addressDto.ToEntity();
            context.SaveChanges();
            context.Events.Add(newEvent);


            context.SaveChanges();
            if (eventDto.Tags != null)
            {
                var newTags = eventDto.Tags.ToEntityList();
                tagService.AttatchTagsToEvent(newTags, newEvent);
            }

            if (eventDto.Parameters != null)
            {
                var newParameters = eventDto.Parameters.ToEntityList();
                parameterService.AttachParametersToEvent(newParameters, newEvent);
            }
            
            context.SaveChanges();
            scope.Complete();
            eventDto.Slug = newEvent.Slug;
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
            if (eventToUpdate.Name != eventDto.Name)
            {
                eventToUpdate.Slug = CreateSlug(eventDto);
            }

            eventToUpdate.Name = eventDto.Name;
            eventToUpdate.Description = eventDto.Description;
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
                tagService.AttatchTagsToEvent(newTags, eventToUpdate);
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
                parameterService.AttachParametersToEvent(newParameters, eventToUpdate);
            }

            context.Events.Update(eventToUpdate);

            context.SaveChanges();
            scope.Complete();
        }

        return eventDto;
    }

    private void DetachTagsFromEvent(List<Tag> tagsToRemove, Event eventToUpdate)
    {
        tagsToRemove.ForEach(tag => eventToUpdate.Tags.Remove(tag));
        context.SaveChanges();
    }

    private void DetachParametersFromEvent(List<Parameter> parametersToRemove, Event eventToUpdate)
    {
        parametersToRemove.ForEach(param => eventToUpdate.Parameters.Remove(param));
        context.SaveChanges();
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
        var eventToReturn = context.Events.Include(e => e.Tags).Include(e => e.Parameters).Include(e => e.Address).Include(e => e.Organizer).FirstOrDefault(e => e.Slug == slug);
        if (eventToReturn is null)
        {
            throw new NotFoundException("Event not found");
        }


        return eventToReturn;
    }

    public int GetEventParticipantsCountBySlugAndStatus(string slug, ParticipantStatus status)
    {
        var eventToReturn = context.Events.Include(e => e.Participants).FirstOrDefault(e => e.Slug == slug);
        if (eventToReturn is null)
        {
            throw new NotFoundException("Event not found");
        }

        return eventToReturn.Participants.Count(p => p.Status == status);
    }

    public IEnumerable<ParticipantDto> GetEventParticipantsBySlugAndStatus(string slug, ParticipantStatus status)
    {
        var eventToReturn = context.Events.Include(e => e.Participants).FirstOrDefault(e => e.Slug == slug);
        if (eventToReturn is null)
        {
            throw new NotFoundException("Event not found");
        }

        var participantDtos = eventToReturn.Participants
            .Where(p => p.Status == status)
            .Select(p => p.ToDto());

        return participantDtos;
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

    public void AddEventComment(CommentDto commentDto)
    {
        commentService.CreateComment(commentDto);
        context.SaveChanges();
    }

    public IEnumerable<EventResponse> GetEventsByFilter(Expression<Func<Event, bool>> filter)
    {
        var events = context.Events.Include(e => e.Parameters).Include(e => e.Tags).Include(e => e.Address).Include(e => e.Organizer).Where(filter).ToList();

        var eventsResponses = new List<EventResponse>();
        foreach (var thisEvent in events)
        {
            var participating = GetEventParticipantsCountBySlugAndStatus(thisEvent.Slug, ParticipantStatus.Participating);
            var interested = GetEventParticipantsCountBySlugAndStatus(thisEvent.Slug, ParticipantStatus.Interested);
            var participants = new Participants(participating, interested);
            var username = thisEvent.Organizer.Username;
            var addressDto = thisEvent.Address.ToDto();
            var addressId = thisEvent.AddressId;

            var eventDto = thisEvent.ToDto();
            var eventResponse = eventDto.ToResponse(participants, username, $"/api/v1/Users/image/{username}", addressDto, addressId, GetEventImages(thisEvent.Slug));
            eventsResponses.Add(eventResponse);
        }

        return eventsResponses.AsEnumerable();
    }

    public List<string> GetEventImages(string slug)
    {
        var @event = GetEventBySlug(slug);
        var images = imageService.GetImagesByEventId(@event.Id);
        var count = images.Count();
        var stringList = new List<string>();
        for (int i = 0; i < count; i++)
        {
            stringList.Add($"/api/v1/Events/{slug}/image/{i}");
        }
        return stringList;
    }

    public IEnumerable<EventResponse> GetAllEvents()
    {
        var events = context.Events.Include(e => e.Parameters).Include(e => e.Tags).Include(e => e.Address).Include(e => e.Organizer).ToList();
        var eventsResponses = new List<EventResponse>();
        foreach (var thisEvent in events )
        {
            var participating = GetEventParticipantsCountBySlugAndStatus(thisEvent.Slug, ParticipantStatus.Participating);
            var interested = GetEventParticipantsCountBySlugAndStatus(thisEvent.Slug, ParticipantStatus.Interested);
            var participants = new Participants(participating, interested);
            var username = thisEvent.Organizer.Username;
            var addressDto = thisEvent.Address.ToDto();
            var addressId = thisEvent.AddressId;

            var eventDto = thisEvent.ToDto();
            var eventResponse = eventDto.ToResponse(participants, username, $"/api/v1/Users/image/{username}", addressDto, addressId, GetEventImages(thisEvent.Slug));
            eventsResponses.Add(eventResponse);
        }

        return eventsResponses.AsEnumerable();
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

    public void UpdateAddressForEvent(AddressDto addressDto, int addressId, string slug)
    {
        var relatedEvent = GetEventBySlug(slug);

        if (relatedEvent.AddressId != addressId)
        {
            throw new NotFoundException("Address is not related with this event");
        }

        if (addressDto == addressService.GetAddressById(addressId)) return;
        addressService.UpdateAddress(addressId, addressDto);

        context.SaveChanges();
    }

    public AddressDto GetRelatedAddress(string slug)
    {
        var relatedEvent = GetEventBySlug(slug);
        var address = context.Addresses.Find(relatedEvent.AddressId);

        if (address is null)
        {
            throw new NotFoundException("Address not found");
        }

        return address.ToDto();
    }

    private string CreateSlug(EventDto eventDto)
    {
        var baseSlug = Regex.Replace(eventDto.Name.ToLower().Trim(), @"[^a-z0-9\s-]", "")
            .Replace(" ", "-")
            .Replace("--", "-");

        var pattern = $"^{Regex.Escape(baseSlug)}(-\\d+)?$";

        var existingSlugs = context.Events
            .Select(e => e.Slug)
            .AsNoTracking()
            .AsEnumerable()
            .Where(slug => Regex.IsMatch(slug, pattern));

        var highestNumber = existingSlugs
            .Select(slug => Regex.Match(slug, $"^{Regex.Escape(baseSlug)}-(\\d+)$"))
            .Where(match => match.Success)
            .Select(match => int.Parse(match.Groups[1].Value))
            .DefaultIfEmpty(0)
            .Max();

        var counter = highestNumber + 1;
        var finalSlug = $"{baseSlug}-{counter}";

        if (finalSlug.Length > 128)
        {
            finalSlug = $"{baseSlug[..(128 - counter.ToString().Length - 1)]}-{counter}";
        }

        return finalSlug;

    }

}