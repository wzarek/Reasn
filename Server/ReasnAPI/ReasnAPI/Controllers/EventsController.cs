using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReasnAPI.Models.Database;
using ReasnAPI.Models.DTOs;
using ReasnAPI.Models.Enums;
using ReasnAPI.Services;
using ReasnAPI.Exceptions;
using FluentValidation;
using ReasnAPI.Mappers;
using ReasnAPI.Models.API;

namespace ReasnAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class EventsController(
    ReasnContext context,
    EventService eventService,
    UserService userService,
    ImageService imageService,
    TagService tagService)
    : ControllerBase
{

    [HttpGet]
    [ProducesResponseType<IEnumerable<EventResponse>>(StatusCodes.Status200OK)]
    public IActionResult GetEvents()
    {
        var events = eventService.GetAllEvents();
        var eventsDtos = new List<EventResponse>();

        foreach (var thisEvent in events)
        {
            var participating = eventService.GetEventParticipantsCountBySlugAndStatus(thisEvent.Slug, ParticipantStatus.Participating);
            var interested = eventService.GetEventParticipantsCountBySlugAndStatus(thisEvent.Slug, ParticipantStatus.Interested);
            var username = userService.GetUserById(eventService.GetEventBySlug(thisEvent.Slug).OrganizerId).Username;
            var eventResponse = thisEvent.ToResponse(participating, interested, username, $"image/{thisEvent.OrganizerId}");
            eventsDtos.Add(eventResponse);
        }
   
        return Ok(eventsDtos);
    }

    [HttpPost]
    [Authorize(Roles = "Admin, Organizer")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult CreateEvent([FromBody] EventCreateRequest eventRequest, [FromServices] IValidator<EventDto> validator)
    {
        var user = userService.GetCurrentUser();
        var eventDto = eventRequest.ToDto(user.Id);
        validator.ValidateAndThrow(eventDto);

        eventService.CreateEvent(eventDto);
        return Created();
    }

    [HttpGet]
    [Route("{slug}")]
    [ProducesResponseType<EventResponse>(StatusCodes.Status200OK)]
    public IActionResult GetEventBySlug([FromRoute] string slug)
    {
        var eventDto = eventService.GetEventBySlug(slug).ToDto();
        var participating = eventService.GetEventParticipantsCountBySlugAndStatus(slug, ParticipantStatus.Participating);
        var interested = eventService.GetEventParticipantsCountBySlugAndStatus(slug, ParticipantStatus.Interested);
        var username = userService.GetUserById(eventService.GetEventBySlug(slug).OrganizerId).Username;
        var eventResponse = eventDto.ToResponse(participating, interested, username, $"image/{eventDto.OrganizerId}");

        return Ok(eventResponse);
    }

    [HttpPut]
    [Authorize(Roles = "Admin, Organizer")]
    [Route("{slug}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult UpdateEvent([FromRoute] string slug, [FromBody] EventUpdateRequest eventUpdateRequest, [FromServices] IValidator<EventDto> validator)
    {
        var eventDto = eventUpdateRequest.ToDto();
        validator.ValidateAndThrow(eventDto);
        var existingEvent = eventService.GetEventBySlug(slug);
        var user = userService.GetCurrentUser();

        if (existingEvent.OrganizerId != user.Id && user.Role != UserRole.Admin)
        {
            return Forbid();
        }

        if (existingEvent.Status != eventUpdateRequest.Status && user.Role != UserRole.Admin && eventUpdateRequest.Status != EventStatus.Cancelled)
        {
            return Forbid();
        }

        eventService.UpdateEvent(existingEvent.Id, eventDto);

        return Ok();
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    [Route("requests")]
    [ProducesResponseType<List<EventResponse>>(StatusCodes.Status200OK)]
    public IActionResult GetEventsRequests([FromRoute] string slug)
    {
        var events = eventService.GetEventsByFilter(e => e.Status == EventStatus.PendingApproval);
        var eventsDtos = new List<EventResponse>();
        foreach (var thisEvent in events)
        {
            var participating = eventService.GetEventParticipantsCountBySlugAndStatus(thisEvent.Slug, ParticipantStatus.Participating);
            var interested = eventService.GetEventParticipantsCountBySlugAndStatus(thisEvent.Slug, ParticipantStatus.Interested);
            var username = userService.GetUserById(eventService.GetEventBySlug(thisEvent.Slug).OrganizerId).Username;
            var eventResponse = thisEvent.ToResponse(participating, interested, username, $"image/{thisEvent.OrganizerId}");
            eventsDtos.Add(eventResponse);
        }
        return Ok(eventsDtos); 
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [Route("{slug}/approve")]
    [ProducesResponseType<EventDto>(StatusCodes.Status200OK)]
    public IActionResult ApproveEventRequest([FromRoute] string slug)
    {
        var eventToApprove = eventService.GetEventBySlug(slug);

        eventToApprove.Status = EventStatus.Approved;
        eventService.UpdateEvent(eventToApprove.Id, eventToApprove.ToDto());
        return Ok();
    }

    [HttpPost]
    [Authorize(Roles = "Admin, Organizer")]
    [Route("{slug}/images")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> AddEventImage([FromRoute] string slug, [FromForm] List<IFormFile> images)
    {
        var @event = eventService.GetEventBySlug(slug);
        var user = userService.GetCurrentUser();

        if (@event.OrganizerId != user.Id && user.Role != UserRole.Admin)
        {
            return Forbid();
        }

        var imageDtos = new List<ImageDto>();

        foreach (var formFile in images.Where(formFile => formFile.Length > 0))
        {
            using var ms = new MemoryStream();
            await formFile.CopyToAsync(ms);
            var fileBytes = ms.ToArray();

            imageDtos.Add(new ImageDto
            {
                ObjectId = @event.Id,
                ObjectType = ObjectType.Event,
                ImageData = fileBytes
            });
        }

        imageService.CreateImages(imageDtos);
        return Ok();
    }

    [HttpPut]
    [Authorize(Roles = "Admin, Organizer")]
    [Route("{slug}/images")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateEventImage([FromRoute] string slug, [FromForm] List<IFormFile> images)
    {
        var user = userService.GetCurrentUser();
        var @event = eventService.GetEventBySlug(slug);

        if (@event.OrganizerId != user.Id && user.Role != UserRole.Admin)
        {
            return Forbid();
        }

        var imageDtos = new List<ImageDto>();

        foreach (var formFile in images.Where(formFile => formFile.Length > 0))
        {
            using var ms = new MemoryStream();
            await formFile.CopyToAsync(ms);
            var fileBytes = ms.ToArray();

            imageDtos.Add(new ImageDto
            {
                ObjectId = @event.Id,
                ObjectType = ObjectType.Event,
                ImageData = fileBytes
            });
        }

        if (@event.Id != imageDtos[0].ObjectId)
        {
            return NotFound();
        }

        imageService.UpdateImagesForEvent(@event.Id, imageDtos);
        return Ok();
    }

    [HttpGet]
    [Route("{slug}/images")]
    [ProducesResponseType<List<string>>(StatusCodes.Status200OK)]
    public IActionResult GetEventImages([FromRoute] string slug)
    {
        var @event = eventService.GetEventBySlug(slug);
        var images = imageService.GetImagesByEventId(@event.Id);
        var count = images.Count();
        var stringList = new List<string>();
        for (int i = 0; i < count; i++)
        {
            stringList.Add($"{slug}/image/{i}");
        }
        return Ok(stringList);
    }

    [HttpGet]
    [Route("{slug}/image/{id:int}")]
    [ProducesResponseType<FileContentResult>(StatusCodes.Status200OK)]
    public IActionResult GetEventImage([FromRoute] string slug, [FromRoute] int id)
    {
        var @event = eventService.GetEventBySlug(slug);
        var images = imageService.GetImagesByEventId(@event.Id).ToList();

        if (images.Count <= id)
        {
            return NotFound();
        }

        var image = images[id];

        return File(image.ImageData, $"image/jpeg");
    }

    [HttpGet]
    [Route("{slug}/participants")]
    [ProducesResponseType<ParticipantsResponse>(StatusCodes.Status200OK)]
    public IActionResult GetEventParticipants([FromRoute] string slug)
    {
        var interestedDto = eventService.GetEventParticipantsBySlugAndStatus(slug, ParticipantStatus.Interested).ToList();
        var participatingDto = eventService.GetEventParticipantsBySlugAndStatus(slug, ParticipantStatus.Participating).ToList();

        var participantsResponse = new ParticipantsResponse
        {
            Interested = interestedDto,
            Participating = participatingDto
        };

        return Ok(participantsResponse);
    }

    [HttpGet]
    [Route("{slug}/comments")]
    [ProducesResponseType<List<CommentDto>>(StatusCodes.Status200OK)]
    public IActionResult GetEventComments([FromRoute] string slug)
    {
        var commentsDto = eventService.GetEventCommentsBySlug(slug); 
        return Ok(commentsDto);
    }

    [HttpPost]
    [Authorize]
    [Route("{slug}/comments")]
    public IActionResult AddEventComment([FromRoute] string slug, [FromBody]CommentRequest commentRequest, [FromServices] IValidator<CommentDto> validator)
    {
        var user = userService.GetCurrentUser();

        var commentDto = commentRequest.ToDtoFromRequest(user.Id);
        validator.ValidateAndThrow(commentDto);
        
        eventService.AddEventComment(commentDto,slug);
        return Ok();
    }

    [HttpGet]
    [Authorize(Roles = "Admin, Organizer")]
    [Route("parameters")]
    [ProducesResponseType<List<ParameterDto>>(StatusCodes.Status200OK)]
    public IActionResult GetEventsParameters([FromRoute] string slug)
    {
        var eventDto = eventService.GetEventBySlug(slug).ToDto();
        if (eventDto == null)
        {
            return NotFound();
        }

        var parameters = eventDto.Parameters;
        return Ok(parameters);
    }

    [HttpGet]
    [Authorize(Roles = "Admin, Organizer")]
    [Route("{slug}/tags")]
    [ProducesResponseType<List<TagDto>>(StatusCodes.Status200OK)]
    public IActionResult GetEventsTags([FromRoute] string slug)
    {
        var user = userService.GetCurrentUser();
        var @event = eventService.GetEventBySlug(slug);

        if (user.Role != UserRole.Admin && @event.OrganizerId != user.Id)
        {
            Forbid();
        }

        var tags = @event.Tags;
        return Ok(tags);
    }

    [HttpDelete]
    [Authorize(Roles = "Admin")]
    [Route("tags/{tagId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult DeleteEventsTag([FromRoute] int tagId)
    {
        tagService.DeleteTag(tagId);
        return NoContent(); 
    }
}