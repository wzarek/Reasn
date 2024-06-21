using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReasnAPI.Models.DTOs;
using ReasnAPI.Models.Enums;
using ReasnAPI.Services;

namespace ReasnAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController(UserService userService, InterestService interestService) : ControllerBase
{
    private readonly UserService _userService = userService;
    private readonly InterestService _interestService = interestService;

    [HttpGet]
    [Authorize(Roles = "User")]
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
        var user = _userService.GetUserByUsername(username);

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
        var userToUpdate = _userService.GetUserByUsername(username);

        // Users can only update their own profile, unless they are an admin
        if (currentUser.Role != UserRole.Admin && currentUser.Username != userToUpdate.Username)
        {
            return Forbid();
        }

        // Non-admin users can't update their role to admin
        if (currentUser.Role != UserRole.Admin && userDto.Role == UserRole.Admin)
        {
            return Forbid();
        }

        var updatedUser = _userService.UpdateUser(userToUpdate.Username, userDto);

        return Ok(updatedUser);
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