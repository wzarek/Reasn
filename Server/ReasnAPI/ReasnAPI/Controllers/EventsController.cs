using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

    public EventsController(ReasnContext context)
    {
        _context = context;
    }

    [HttpGet]
    [ProducesResponseType<IEnumerable<EventDto>>(StatusCodes.Status200OK)]
    public IActionResult GetEvents()
    {
        var events = _eventService.GetEvents();
        return Ok(events);
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
    [ProducesResponseType<EventDto>(StatusCodes.Status200OK)]
    public IActionResult GetEventBySlug(string slug)
    {
        try
        {
            var eventDto = _eventService.GetEventBySlug(slug);
            return Ok(eventDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPut]
    [Authorize(Roles = "Admin, Organizer")]
    [Route("{slug}")]
    [ProducesResponseType<EventDto>(StatusCodes.Status200OK)]
    public IActionResult UpdateEvent(string slug, [FromBody] EventDto eventDto)
    {
        try
        {
            var existingEvent = _eventService.GetEventBySlug(slug);
            if (existingEvent == null)
            {
                return NotFound();
            }

            var updatedEvent = _eventService.UpdateEvent(existingEvent.Id, eventDto);
            return Ok(updatedEvent);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    [Route("requests")]
    [ProducesResponseType<EventDto>(StatusCodes.Status200OK)]
    public IActionResult GetEventsRequests(string slug)
    {
        try
        {
            var events = _eventService.GetEventsByFilter(e => e.Status == "NotApproved");
            return Ok(events);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [Route("{slug}/approve")]
    [ProducesResponseType<EventDto>(StatusCodes.Status200OK)]
    public IActionResult ApproveEventRequest(string slug)
    {
        try
        {
            var eventToApprove = _eventService.GetEventBySlug(slug);

            eventToApprove.Status = "Approved";
            _eventService.UpdateEvent(eventToApprove.Id, eventToApprove);
            return Ok(eventToApprove);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost]
    [Authorize(Roles = "Admin, Organizer")]
    [Route("{slug}/images")]
    public IActionResult AddEventImage(string slug, [FromBody] List<ImageDto> imageDtos)
    {
        try
        {
            var @event = _eventService.GetEventBySlug(slug);
            if (@event.Id != imageDtos[0].ObjectId)
            {
                return NotFound(); // jakies exception tutaj
            }
            var image = _imageService.CreateImage(imageDtos, ObjectType.Event);
            return Ok(image);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
        
    }

    [HttpPut]
    [Authorize(Roles = "Admin, Organizer")]
    [Route("{slug}/images/{imageId:int}")]
    public IActionResult UpdateEventImage(string slug,[FromBody] List<ImageDto> imageDtos)
    {
        try
        {
            var @event = _eventService.GetEventBySlug(slug);
            if (@event.Id != imageDtos[0].ObjectId)
            {
                return NotFound(); // jakies exception tutaj
            }
            var image = _imageService.UpdateImages(imageDtos, ObjectType.Event);
            return Ok(image);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    [HttpDelete]
    [Authorize(Roles = "Admin, Organizer")]
    [Route("{slug}/images/{imageId:int}")]
    public IActionResult DeleteEventImage(string slug, int imageId)
    {
        throw new NotImplementedException(); // pytanie czy to potrzebne zadziala na updatcie XD
    }

    [HttpGet]
    [Route("{slug}/users")]
    public IActionResult GetEventUsers(string slug)
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    [Route("{slug}/comments")]
    public IActionResult GetEventComments(string slug)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    [Authorize]
    [Route("{slug}/comments")]
    public IActionResult AddEventComment(string slug)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    [Authorize(Roles = "Admin, Organizer")]
    [Route("{slug}/parameters")]
    public IActionResult AddEventParameter(string slug)
    {
        throw new NotImplementedException();
    }

    [HttpDelete]
    [Authorize(Roles = "Admin, Organizer")]
    [Route("{slug}/parameters/{parameterId:int}")]
    public IActionResult DeleteEventParameter(string slug, int parameterId)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    [Authorize(Roles = "Admin, Organizer")]
    [Route("{slug}/tags")]
    public IActionResult AddEventTag(string slug)
    {
        throw new NotImplementedException();
    }

    [HttpDelete]
    [Authorize(Roles = "Admin, Organizer")]
    [Route("{slug}/tags/{tagId:int}")]
    public IActionResult DeleteEventTag(string slug, int tagId)
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    [Authorize(Roles = "Admin, Organizer")]
    [Route("parameters")]
    public IActionResult GetEventsParameters(string slug)
    {
        try
        {
            var eventDto = _eventService.GetEventBySlug(slug);
            if (eventDto == null)
            {
                return NotFound();
            }

            var parameters = eventDto.Parameters;
            return Ok(parameters);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    [HttpDelete]
    [Authorize(Roles = "Admin")]
    [Route("parameters/{parameterId:int}")]
    public IActionResult DeleteEventsParameter(int parameterId)
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    [Authorize(Roles = "Admin, Organizer")]
    [Route("{slug}/tags")]
    public IActionResult GetEventsTags(string slug)
    {
        try
        {
            var eventDto = _eventService.GetEventBySlug(slug);
            if (eventDto == null)
            {
                return NotFound();
            }

            var tags = eventDto.Tags;
            return Ok(tags);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    [HttpDelete]
    [Authorize(Roles = "Admin")]
    [Route("tags/{tagId:int}")]
    public IActionResult DeleteEventsTag(int tagId)
    {
        try
        {
            var tag = _tagService.DeleteTag(tagId);
            return Ok(tag);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
        catch (BadRequestException)
        {
            return BadRequest();
        }
    }
}