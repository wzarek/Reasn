using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReasnAPI.Models.Database;

namespace ReasnAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class EventsController : ControllerBase
{
    private readonly ReasnContext _context;

    public EventsController(ReasnContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetEvents()
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    [Authorize(Roles = "Admin, Organizer")]
    public IActionResult CreateEvent()
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    [Route("{slug}")]
    public IActionResult GetEventBySlug(string slug)
    {
        throw new NotImplementedException();
    }

    [HttpPut]
    [Authorize(Roles = "Admin, Organizer")]
    [Route("{slug}")]
    public IActionResult UpdateEvent(string slug)
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    [Route("requests")]
    public IActionResult GetEventsRequests(string slug)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [Route("{slug}/approve")]
    public IActionResult ApproveEventRequest(string slug)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    [Authorize(Roles = "Admin, Organizer")]
    [Route("{slug}/images")]
    public IActionResult AddEventImage(string slug)
    {
        throw new NotImplementedException();
    }

    [HttpPut]
    [Authorize(Roles = "Admin, Organizer")]
    [Route("{slug}/images/{imageId:int}")]
    public IActionResult UpdateEventImage(string slug, int imageId)
    {
        throw new NotImplementedException();
    }

    [HttpDelete]
    [Authorize(Roles = "Admin, Organizer")]
    [Route("{slug}/images/{imageId:int}")]
    public IActionResult DeleteEventImage(string slug, int imageId)
    {
        throw new NotImplementedException();
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

    [HttpGet]
    [Authorize(Roles = "Admin, Organizer")]
    [Route("parameters")]
    public IActionResult GetEventsParameters(string slug)
    {
        throw new NotImplementedException();
    }

    [HttpDelete]
    [Authorize(Roles = "Admin")]
    [Route("parameters/{parameterId:int}")]
    public IActionResult DeleteEventsParameter(int parameterId)
    {
        throw new NotImplementedException();
    }
}