using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ReasnAPI.Models.Database;
using ReasnAPI.Models.DTOs;
using ReasnAPI.Models.Enums;
using ReasnAPI.Services;

namespace ReasnAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController(UserService userService, InterestService interestService, ImageService imageService, RecomendationService recomendationService) : ControllerBase
{
    private readonly UserService _userService = userService;
    private readonly ImageService _imageService = imageService;
    private readonly InterestService _interestService = interestService;
    private readonly RecomendationService _recomendationService;

    [HttpGet]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType<IEnumerable<UserDto>>(StatusCodes.Status200OK)]
    public IActionResult GetUsers()
    {
        var users = _userService.GetUsersByFilter(u => u.IsActive && u.Role != UserRole.Admin);
        return Ok(users);
    }

    [HttpGet]
    [Route("{username}")]
    [ProducesResponseType<UserDto>(StatusCodes.Status200OK)]
    public IActionResult GetUserByUsername([FromRoute] string username)
    {
        var user =_userService.GetUserByUsername(username);

        if (user.Role == UserRole.Admin)
        {
            return Forbid();
        }

        return Ok(user);
    }

    [HttpPut]
    [Authorize]
    [Route("{username}")]
    [ProducesResponseType<UserDto>(StatusCodes.Status200OK)]
    public IActionResult UpdateUser(
        [FromBody] UserDto userDto,
        [FromRoute] string username,
        [FromServices] IValidator<UserDto> validator)
    {
        validator.ValidateAndThrow(userDto);

        var currentUser = _userService.GetCurrentUser();

        // Only admins can update other users from this endpoint
        if (currentUser.Role != UserRole.Admin)
        {
            return Forbid();
        }

        var updatedUser = _userService.UpdateUser(username, userDto);

        return Ok(updatedUser);
    }

    [HttpGet]
    [Route("image/{username}")]
    public IActionResult GetImageByUsername(string username)
    {
        var userId = userService.GetUserIdByUsername(username);
        var image = _imageService.GetImageByUserId(userId);

        return File(image.ImageData, $"image/jpeg");
    }

    [HttpGet]
    [Authorize]
    [Route("interests")]
    [ProducesResponseType<IEnumerable<InterestDto>>(StatusCodes.Status200OK)]
    public IActionResult GetUsersInterests()
    {
        var interests = _interestService.GetAllInterests();
        return Ok(interests);
    }

    [HttpDelete]
    [Authorize(Roles = "Admin")]
    [Route("interests/{interestId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult DeleteUserInterest([FromRoute] int interestId)
    {
        _interestService.DeleteInterest(interestId);
        return NoContent();
    }

}