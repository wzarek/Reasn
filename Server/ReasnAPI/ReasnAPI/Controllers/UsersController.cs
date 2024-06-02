using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ReasnAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public IActionResult GetUsers()
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    [Route("{username}")]
    public IActionResult GetUserByUsername(string username)
    {
        throw new NotImplementedException();
    }

    [HttpPut]
    [Authorize]
    [Route("{username}")]
    public IActionResult UpdateUser(string username)
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    [Authorize]
    [Route("interests")]
    public IActionResult GetUsersInterests(string username)
    {
        throw new NotImplementedException();
    }

    [HttpDelete]
    [Authorize(Roles = "Admin")]
    [Route("interests/{interestId:int}")]
    public IActionResult DeleteUserInterest(int interestId)
    {
        throw new NotImplementedException();
    }
}