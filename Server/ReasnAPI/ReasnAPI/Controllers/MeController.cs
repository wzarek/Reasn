using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReasnAPI.Mappers;
using ReasnAPI.Models.DTOs;
using ReasnAPI.Services;

namespace ReasnAPI.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class MeController : ControllerBase
{
    private readonly UserService _userService;

    public MeController(UserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    [ProducesResponseType<UserDto>(StatusCodes.Status200OK)]
    public IActionResult GetCurrentUser()
    {
        var user = _userService.GetCurrentUser();
        return Ok(user.ToDto());
    }

    [HttpPut]
    public IActionResult UpdateCurrentUser()
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    [Route("image")]
    public IActionResult AddCurrentUserImage()
    {
        throw new NotImplementedException();
    }

    [HttpPut]
    [Route("image")]
    public IActionResult UpdateCurrentUserImage()
    {
        throw new NotImplementedException();
    }

    [HttpDelete]
    [Route("image")]
    public IActionResult DeleteCurrentUserImage()
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    [Route("interests")]
    public IActionResult GetCurrentUserInterests()
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    [Route("interests")]
    public IActionResult AddCurrentUserInterest()
    {
        throw new NotImplementedException();
    }

    [HttpDelete]
    [Route("interests/{interestId:int}")]
    public IActionResult DeleteCurrentUserInterest(int interestId)
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    [Route("events")]
    public IActionResult GetCurrentUserEvents()
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    [Route("events/{slug}/enroll")]
    public IActionResult EnrollCurrentUserInEvent(string slug)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    [Route("events/{slug}/confirm")]
    public IActionResult ConfirmCurrentUserEventAttendance(string slug)
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    [Route("events/recommendations")]
    public IActionResult GetCurrentUserEventRecommendations()
    {
        throw new NotImplementedException();
    }
}