using Microsoft.AspNetCore.Mvc;

namespace ReasnAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    [HttpGet]
    [Route("{username}")]
    public IActionResult GetUserByUsername(string username)
    {
        throw new NotImplementedException();
    }
}