using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReasnAPI.Mappers;
using ReasnAPI.Models.API;
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
        var username = _userService.GetCurrentUser().Username;
        var user = _userService.GetUserByUsername(username);

        return Ok(user);
    }

    [HttpPut]
    [ProducesResponseType<UserDto>(StatusCodes.Status200OK)]
    public IActionResult UpdateCurrentUser(
        [FromBody] UserDto userDto,
        [FromServices] IValidator<UserDto> validator)
    {
        validator.ValidateAndThrow(userDto);

        var user = _userService.GetCurrentUser();

        // Users cant change their role in this endpoint
        if (user.Role != userDto.Role)
        {
            return Forbid();
        }

        var updatedUser = _userService.UpdateUser(user.Username, userDto);

        return Ok(updatedUser);
    }

    [HttpGet]
    [Route("image")]
    [ProducesResponseType<ImageDto>(StatusCodes.Status200OK)]
    public IActionResult GetCurrentUserImage()
    {
        var user = _userService.GetCurrentUser();
        var image = _imageService.GetImageByUserId(user.Id);

        if (image is null)
        {
            return NotFound();
        }

        return File(image.ImageData, "image/jpeg");
    }

    [HttpPost]
    [Route("image")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> AddCurrentUserImage([FromForm] List<IFormFile> images)
    {
        var userId = _userService.GetCurrentUser().Id;

        foreach (var image in images.Where(image => image.Length > 0))
        {
            using var ms = new MemoryStream();
            await image.CopyToAsync(ms);
            var fileBytes = ms.ToArray();

            var imageDto = new ImageDto
            {
                ObjectId = userId,
                ObjectType = ObjectType.User,
                ImageData = fileBytes
            };

            _imageService.CreateImages([imageDto]);
        }

        return Ok();
    }

    [HttpPut]
    [Route("image")]
    [ProducesResponseType<ImageDto>(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateCurrentUserImage([FromForm] List<IFormFile> images)
    {
        var userId = _userService.GetCurrentUser().Id;

        foreach (var image in images.Where(image => image.Length > 0))
        {
            using var ms = new MemoryStream();
            await image.CopyToAsync(ms);
            var fileBytes = ms.ToArray();

            var imageDto = new ImageDto
            {
                ObjectId = userId,
                ObjectType = ObjectType.User,
                ImageData = fileBytes
            };

            _imageService.UpdateImageForUser(userId, imageDto);
        }

        return Ok();
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
    [ProducesResponseType<IEnumerable<EventResponse>>(StatusCodes.Status200OK)]
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
        var user = _userService.GetCurrentUser();

        var participant = _participantService.CreateOrUpdateParticipant(new ParticipantDto { EventSlug = slug, Username = user.Username, Status = ParticipantStatus.Interested });

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
        var user = _userService.GetCurrentUser();
        var participant = _participantService.CreateOrUpdateParticipant(new ParticipantDto { EventSlug = slug, Username = user.Username, Status = ParticipantStatus.Participating });

        return Ok(participant);
    }

    [HttpDelete]
    [Route("events/{slug}/cancel")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult CancelCurrentUserEventAttendance([FromRoute] string slug)
    {
        var userId = _userService.GetCurrentUser().Id;
        _participantService.DeleteParticipant(userId, slug);

        return NoContent();
    }

    [HttpGet]
    [Route("events/recommendations")]
    public IActionResult GetCurrentUserEventRecommendations()
    {
        throw new NotImplementedException();
    }
}