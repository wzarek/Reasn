using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReasnAPI.Helpers;
using ReasnAPI.Models.Database;
using ReasnAPI.Models.DTOs;
using ReasnAPI.Models.Enums;
using ReasnAPI.Services;
using ReasnAPI.Services.Exceptions;
using FluentValidation;
using ReasnAPI.Mappers;

namespace ReasnAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class EventsController : ControllerBase
{
    private readonly ReasnContext _context;
    private readonly EventService _eventService;
    private readonly UserService _userService;
    private readonly ImageService _imageService;
    private readonly TagService _tagService;
    private readonly ParticipantService _paticipantService;

    public EventsController(ReasnContext context)
    {
        _context = context;
    }

    [HttpGet]
    [ProducesResponseType<IEnumerable<EventResponse>>(StatusCodes.Status200OK)]
    public IActionResult GetEvents()
    {
        var events = _eventService.GetEvents();
        var eventsDtos = new List<EventResponse>();

        foreach (var thisEvent in events)
        {
            var participating = _paticipantService.GetParticipantsByEventSlug(thisEvent.Slug, ParticipantStatus.Participating);
            var interested = _paticipantService.GetParticipantsByEventSlug(thisEvent.Slug, ParticipantStatus.Interested);
            var eventResponse = thisEvent.ToResponse(participating, interested);
            eventsDtos.Add(eventResponse);
        }
   
        return Ok(eventsDtos);
    }

    [HttpPost]
    [Authorize(Roles = "Admin, Organizer")]
    [ProducesResponseType<EventDto>(StatusCodes.Status201Created)]
    public IActionResult CreateEvent([FromBody] EventCreateRequest eventRequest, [FromServices] IValidator<EventDto> validator)
    {
        var user = _userService.GetCurrentUser();
        var eventDto = eventRequest.ToDtoFromRequest(user.Id);
        validator.ValidateAndThrow(eventDto);

        _eventService.CreateEvent(eventDto);
        return Created();
    
    }

    [HttpGet]
    [Route("{slug}")]
    [ProducesResponseType<EventResponse>(StatusCodes.Status200OK)]
    public IActionResult GetEventBySlug([FromRoute] string slug)
    {
        var eventDto = _eventService.GetEventBySlug(slug);
        var participating = _paticipantService.GetParticipantsByEventSlug(slug, ParticipantStatus.Participating);
        var interested = _paticipantService.GetParticipantsByEventSlug(slug, ParticipantStatus.Interested);
        var eventResponse = eventDto.ToResponse(participating, interested);

        return Ok(eventResponse);
    
    }

    [HttpPut]
    [Authorize(Roles = "Admin, Organizer")]
    [Route("{slug}")]
    [ProducesResponseType<EventResponse>(StatusCodes.Status200OK)]
    public IActionResult UpdateEvent([FromRoute] string slug, [FromBody] EventUpdateRequest eventUpdateRequest, [FromServices] IValidator<EventDto> validator)
    {
        var eventDto = eventUpdateRequest.ToDtoFromRequest();
        validator.ValidateAndThrow(eventDto);
        var existingEvent = _eventService.GetEventBySlug(slug);
        var user = _userService.GetCurrentUser();
        if (existingEvent == null)
        {
            return NotFound();
        }

        if (existingEvent.OrgenizerId != user.Id && user.Role != UserRole.Admin)
        {
            return Forbid();
        }

        var updatedEvent = _eventService.UpdateEvent(existingEvent.Id, eventDto);
        var participating = _paticipantService.GetParticipantsByEventSlug(slug, ParticipantStatus.Participating);
        var interested = _paticipantService.GetParticipantsByEventSlug(slug, ParticipantStatus.Interested);
        var eventResponse = updatedEvent.ToResponse(participating, interested);

        return Ok(eventResponse);
       
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    [Route("requests")]
    [ProducesResponseType<EventDto>(StatusCodes.Status200OK)]
    public IActionResult GetEventsRequests([FromRoute] string slug)
    {
        var user = _userService.GetCurrentUser();

        var events = _eventService.GetEventsByFilter(e => e.Status == EventStatus.WaitingForApproval);
        return Ok(events); // tutaj chyab nie potrzeba eventresponsa ze wzgledu na to ze pobieramy eventy
                           // ktore nie sa jeszcze zatwierdzone wiec nie powinny miec uczestnikow
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [Route("{slug}/approve")]
    [ProducesResponseType<EventDto>(StatusCodes.Status200OK)]
    public IActionResult ApproveEventRequest([FromRoute] string slug)
    {
        var eventToApprove = _eventService.GetEventBySlug(slug);

        eventToApprove.Status = EventStatus.Approved;
        _eventService.UpdateEvent(eventToApprove.Id, eventToApprove.ToDto());
        return Ok(eventToApprove);
    }

    [HttpPost]
    [Authorize(Roles = "Admin, Organizer")]
    [Route("{slug}/images")]
    public IActionResult AddEventImage([FromRoute] string slug, [FromBody] List<ImageDto> imageDtos)
    {
        var @event = _eventService.GetEventBySlug(slug);
        var user = _userService.GetCurrentUser();

        if (@event.OrgenizerId != user.Id && user.Role != UserRole.Admin)
        {
            return Forbid();
        }
        
        if (@event.Id != imageDtos[0].ObjectId)
        {
            return NotFound(); 
        }

        var image = _imageService.CreateImage(imageDtos, ObjectType.Event);
        return Ok(image);
    
    }

    [HttpPut]
    [Authorize(Roles = "Admin, Organizer")]
    [Route("{slug}/images/{imageId:int}")]
    public IActionResult UpdateEventImage([FromRoute] string slug,[FromBody] List<ImageDto> imageDtos)
    {
        var user = _userService.GetCurrentUser();
        var @event = _eventService.GetEventBySlug(slug);
        
        if (@event.OrgenizerId != user.Id && user.Role != UserRole.Admin)
        {
            return Forbid();
        }

        if (@event.Id != imageDtos[0].ObjectId)
        {
            return NotFound(); 
        }
        var image = _imageService.UpdateImages(imageDtos, ObjectType.Event);
        return Ok(image);
    
    }

    [HttpDelete]
    [Authorize(Roles = "Admin, Organizer")]
    [Route("{slug}/images/{imageId:int}")]
    public IActionResult DeleteEventImage([FromRoute] string slug, [FromRoute] int imageId)
    {
        var user = _userService.GetCurrentUser();
        var @event = _eventService.GetEventBySlug(slug);

        if (@event.OrgenizerId == user.Id)
        {
            return Forbid();
        }

        throw new NotImplementedException(); // pytanie czy to potrzebne zadziala na updatcie XD
    }

    //[HttpGet]
    //[Route("{slug}/users")]
    //public IActionResult GetEventUsers(string slug)
    //{
    //    var participantsDto = _eventService.getEventParticipantsBySlug(slug); // podzielic tu logike na getintrested i getparticipants

    //    var participating = participantsDto.Where(p => p.Status == ParticipantStatus.Participating);
    //    var interested = participantsDto.Where(p => p.Status == ParticipantStatus.Interested);
    //    var result = new {participating, interested};
    //    return Ok(result);
    //}

    [HttpGet]
    [Route("{slug}/comments")]
    [ProducesResponseType<List<CommentDto>>(StatusCodes.Status200OK)]
    public IActionResult GetEventComments([FromRoute] string slug)
    {
        var commentsDto = _eventService.GetEventCommentsBySlug(slug); 
        return Ok(commentsDto);
    }

    [HttpPost]
    [Authorize]
    [Route("{slug}/comments")]
    public IActionResult AddEventComment([FromRoute] string slug, [FromBody]CommentRequest commentRequest)
    {
        var user = _userService.GetCurrentUser();

        var commentDto = commentRequest.ToDtoFromRequest(user.Id);

        _eventService.AddEventCommentBySlug(commentDto,slug);
        return Ok();
    }

    //[HttpPost]
    //[Authorize(Roles = "Admin, Organizer")]
    //[Route("{slug}/parameters")]
    //public IActionResult AddEventParameter(string slug)
    //{
    //    throw new NotImplementedException();
    //}

    //[HttpDelete]
    //[Authorize(Roles = "Admin, Organizer")]
    //[Route("{slug}/parameters/{parameterId:int}")]
    //public IActionResult DeleteEventParameter(string slug, int parameterId)
    //{
    //    throw new NotImplementedException();
    //}

    //[HttpPost]
    //[Authorize(Roles = "Admin, Organizer")]
    //[Route("{slug}/tags")]
    //public IActionResult AddEventTag(string slug)
    //{
    //    throw new NotImplementedException();
    //}

    //[HttpDelete]
    //[Authorize(Roles = "Admin, Organizer")]
    //[Route("{slug}/tags/{tagId:int}")]
    //public IActionResult DeleteEventTag(string slug, int tagId)
    //{
    //    throw new NotImplementedException();
    //}

    [HttpGet]
    [Authorize(Roles = "Admin, Organizer")]
    [Route("parameters")]
    [ProducesResponseType<List<ParameterDto>>(StatusCodes.Status200OK)]
    public IActionResult GetEventsParameters([FromRoute] string slug)
    {
        var eventDto = _eventService.GetEventBySlug(slug).ToDto();
        if (eventDto == null)
        {
            return NotFound();
        }

        var parameters = eventDto.Parameters;
        return Ok(parameters);
    }

    //[HttpDelete]
    //[Authorize(Roles = "Admin")]
    //[Route("parameters/{parameterId:int}")]
    //public IActionResult DeleteEventsParameter(int parameterId)
    //{
    //    throw new NotImplementedException();
    //}

    [HttpGet]
    [Authorize(Roles = "Admin, Organizer")]
    [Route("{slug}/tags")]
    [ProducesResponseType<List<TagDto>>(StatusCodes.Status200OK)]
    public IActionResult GetEventsTags([FromRoute] string slug)
    {
        var eventDto = _eventService.GetEventBySlug(slug).ToDto();
        if (eventDto == null)
        {
            return NotFound();
        }

        var tags = eventDto.Tags;
        return Ok(tags);
    }

    [HttpDelete]
    [Authorize(Roles = "Admin")]
    [Route("tags/{tagId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult DeleteEventsTag([FromRoute] int tagId)
    {
        var tag = _tagService.DeleteTag(tagId);
        return NoContent(); 
    }
}