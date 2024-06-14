using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReasnAPI.Helpers;
using ReasnAPI.Models.Database;
using ReasnAPI.Models.DTOs;
using ReasnAPI.Models.Enums;
using ReasnAPI.Services;
using ReasnAPI.Services.Exceptions;

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
    public IActionResult CreateEvent([FromBody] EventDto eventDto)
    {
        if (eventDto.OrganizerId != _userService.GetCurrentUser().Id)
        {
            return Unauthorized();
        }
        
        var createdEvent = _eventService.CreateEvent(eventDto);
        return Created();
    
    }

    [HttpGet]
    [Route("{slug}")]
    [ProducesResponseType<EventResponse>(StatusCodes.Status200OK)]
    public IActionResult GetEventBySlug(string slug)
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
    public IActionResult UpdateEvent(string slug, [FromBody] EventDto eventDto)
    {
        var existingEvent = _eventService.GetEventBySlug(slug);
        if (existingEvent == null)
        {
            return NotFound();
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
    public IActionResult GetEventsRequests(string slug)
    {
        var events = _eventService.GetEventsByFilter(e => e.Status == "NotApproved");
        return Ok(events);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [Route("{slug}/approve")]
    [ProducesResponseType<EventDto>(StatusCodes.Status200OK)]
    public IActionResult ApproveEventRequest(string slug)
    {
        var eventToApprove = _eventService.GetEventBySlug(slug);

        eventToApprove.Status = "Approved";
        _eventService.UpdateEvent(eventToApprove.Id, eventToApprove);
        return Ok(eventToApprove);
    }

    [HttpPost]
    [Authorize(Roles = "Admin, Organizer")]
    [Route("{slug}/images")]
    public IActionResult AddEventImage(string slug, [FromBody] List<ImageDto> imageDtos)
    {
   
        var @event = _eventService.GetEventBySlug(slug);
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
    public IActionResult UpdateEventImage(string slug,[FromBody] List<ImageDto> imageDtos)
    {
        var @event = _eventService.GetEventBySlug(slug);
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
    public IActionResult DeleteEventImage(string slug, int imageId)
    {

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
    public IActionResult GetEventComments(string slug)
    {
       var commentsDto = _eventService.GetEventCommentsBySlug(slug);

       return Ok(commentsDto);
    }

    [HttpPost]
    [Authorize]
    [Route("{slug}/comments")]
    public IActionResult AddEventComment(string slug, [FromBody]CommentDto commentDto)
    {
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
    public IActionResult GetEventsParameters(string slug)
    {
        var eventDto = _eventService.GetEventBySlug(slug);
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
    public IActionResult GetEventsTags(string slug)
    {
        var eventDto = _eventService.GetEventBySlug(slug);
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
    public IActionResult DeleteEventsTag(int tagId)
    {
        var tag = _tagService.DeleteTag(tagId);
        return Ok(tag);
    }
}