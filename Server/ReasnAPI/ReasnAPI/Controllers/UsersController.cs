using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReasnAPI.Models.DTOs;
using ReasnAPI.Models.Enums;
using ReasnAPI.Services;

namespace ReasnAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController(UserService userService) : ControllerBase
{
    private readonly UserService _userService = userService;

    [HttpGet]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType<IEnumerable<UserDto>>(StatusCodes.Status200OK)]
    public IActionResult GetUsers()
    {
        var users = _userService.GetAllUsers();
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
        if (currentUser.Role != UserRole.Admin && currentUser.Id != userToUpdate.Id)
        {
            return Forbid();
        }

        // Non-admin users can't update their role to admin
        if (currentUser.Role != UserRole.Admin && userDto.Role == UserRole.Admin)
        {
            return Forbid();
        }

        var updatedUser = _userService.UpdateUser(userToUpdate.Id, userDto);

        var location = Url.Action(
                            action: nameof(GetUserByUsername),
                            controller: "Users",
                            values: new { username = updatedUser.Username });

        return Ok(location, updatedUser);
    }

    [HttpGet]
    [Authorize]
    [Route("interests")]
    public IActionResult GetUsersInterests()
    {
        var interests = _userService.GetAllInterests();
        return Ok(interests);
    }

    [HttpDelete]
    [Authorize(Roles = "Admin")]
    [Route("interests/{interestId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult DeleteUserInterest([FromRoute] int interestId)
    {
        _userService.DeleteInterest(interestId);
        return NoContent();
    }
}