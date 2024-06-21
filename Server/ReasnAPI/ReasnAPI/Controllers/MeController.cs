using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReasnAPI.Mappers;
using ReasnAPI.Models.DTOs;
using ReasnAPI.Models.Enums;
using ReasnAPI.Services;

namespace ReasnAPI.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class MeController(UserService userService, EventService eventService, ParticipantService participantService, ImageService imageService) : ControllerBase
{
    private readonly UserService _userService = userService;
    private readonly EventService _eventService = eventService;
    private readonly ParticipantService _participantService = participantService;
    private readonly ImageService _imageService = imageService;

    [HttpGet]
    [ProducesResponseType<UserDto>(StatusCodes.Status200OK)]
    public IActionResult GetCurrentUser()
    {
        var user = _userService.GetCurrentUser();
        return Ok(user.ToDto());
    }

    [HttpPut]
    [ProducesResponseType<UserDto>(StatusCodes.Status200OK)]
    public IActionResult UpdateCurrentUser(
        [FromBody] UserDto userDto,
        [FromServices] IValidator<UserDto> validator)
    {
        validator.ValidateAndThrow(userDto);

        var user = _userService.GetCurrentUser();

        // Non-admin users can't update their role to admin
        if (user.Role != UserRole.Admin && userDto.Role == UserRole.Admin)
        {
            return Forbid();
        }

        var updatedUser = _userService.UpdateUser(user.Username, userDto);

        return Ok(updatedUser);
    }

    [HttpPost]
    [Route("image")]
    [ProducesResponseType<ImageDto>(StatusCodes.Status201Created)]
    public IActionResult AddCurrentUserImage([FromBody] ImageDto imageDto)
    {
        var image = _imageService.CreateImages([imageDto]);
        return Ok(image);
    }

    [HttpPut]
    [Route("image")]
    [ProducesResponseType<ImageDto>(StatusCodes.Status200OK)]
    public IActionResult UpdateCurrentUserImage([FromBody] ImageDto imageDto)
    {
        var userId = _userService.GetCurrentUser().Id;
        _imageService.UpdateImageForUser(userId, imageDto);

        return Ok(imageDto);
    }

    [HttpDelete]
    [Route("image")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult DeleteCurrentUserImage()
    {
        var userId = _userService.GetCurrentUser().Id;
        _imageService.DeleteImageByObjectIdAndType(userId, ObjectType.User);

        return NoContent();
    }

    [HttpGet]
    [Route("events")]
    [ProducesResponseType<IEnumerable<EventDto>>(StatusCodes.Status200OK)]
    public IActionResult GetCurrentUserEvents()
    {
        var user = _userService.GetCurrentUser();
        var events = _eventService.GetUserEvents(user.Username);

        if (user.Role == UserRole.Organizer)
        {
            var organizerEvents = _eventService.GetEventsByFilter(e => e.OrganizerId == user.Id);
            events = events.Concat(organizerEvents);
        }

        return Ok(events);
    }

    [HttpPost]
    [Route("events/{slug}/enroll")]
    [ProducesResponseType<ParticipantDto>(StatusCodes.Status201Created)]
    public IActionResult EnrollCurrentUserInEvent([FromRoute] string slug)
    {
        var eventId = _eventService.GetEventBySlug(slug).Id;
        var userId = _userService.GetCurrentUser().Id;

        var participant = _participantService.CreateParticipant(new ParticipantDto { EventId = eventId, UserId = userId, Status = ParticipantStatus.Interested });

        var location = Url.Action(
            action: nameof(GetCurrentUserEvents),
            controller: "Me");

        return Created(location, participant);
    }

    [HttpPost]
    [Route("events/{slug}/confirm")]
    [ProducesResponseType<ParticipantDto>(StatusCodes.Status200OK)]
    public IActionResult ConfirmCurrentUserEventAttendance([FromRoute] string slug)
    {
        var eventId = _eventService.GetEventBySlug(slug).Id;
        var userId = _userService.GetCurrentUser().Id;
        var participant = _participantService.GetParticipantsByFilter(p => p.EventId == eventId && p.UserId == userId).First();

        participant = _participantService.UpdateParticipant(participant.UserId, new ParticipantDto { Status = ParticipantStatus.Participating });

        return Ok(participant);
    }

    [HttpPost]
    [Route("events/{slug}/cancel")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult CancelCurrentUserEventAttendance([FromRoute] string slug)
    {
        var eventId = _eventService.GetEventBySlug(slug).Id;
        var userId = _userService.GetCurrentUser().Id;
        var participant = _participantService.GetParticipantsByFilter(p => p.EventId == eventId && p.UserId == userId).First();

        _participantService.DeleteParticipant(participant.UserId);

        return NoContent();
    }

    [HttpGet]
    [Route("events/recommendations")]
    public IActionResult GetCurrentUserEventRecommendations()
    {
        throw new NotImplementedException();
    }
}