using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ReasnAPI.Models.Database;
using ReasnAPI.Models.DTOs;
using ReasnAPI.Services;

namespace ReasnAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : Controller
    {
        
        private readonly EventService _eventService;
        private readonly ImageService _imageService;
        private readonly InterestService _interestService;
        private readonly ParameterService _parameterService;
        private readonly TagService _tagService;

        public TestController(EventService eventService, ImageService imageService, InterestService interestService, ParameterService parameterService, TagService tagService)
        {
            _eventService = eventService;
            _imageService = imageService;
            _interestService = interestService;
            _parameterService = parameterService;
            _tagService = tagService;
        }

        [HttpGet]
        [Route("event/{eventId}")]
        public IActionResult GetEventById(int eventId)
        {
            return Ok(_eventService.GetEventById(eventId));
        }

        [HttpGet]
        [Route("event/filter")]
        public IActionResult GetEventsByFilter([FromBody] Expression<Func<Event, bool>> filter)
        {
            return Ok(_eventService.GetEventsByFilter(filter));
        }

        [HttpGet]
        [Route("event/all")]
        public IActionResult GetAllEvents()
        {
            return Ok(_eventService.GetAllEvents());
        }

        [HttpPost]
        [Route("event/create")]
        public IActionResult CreateEvent([FromBody] EventDto eventDto)
        {
            var @event = _eventService.CreateEvent(eventDto);
            if (@event is null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut]
        [Route("event/update/{eventId}")]
        public IActionResult UpdateEvent(int eventId, [FromBody] EventDto eventDto)
        {
            return Ok(_eventService.UpdateEvent(eventId, eventDto));
        }

        [HttpDelete]
        [Route("event/delete/{eventId}")]
        public IActionResult DeleteEvent(int eventId)
        {
            _eventService.DeleteEvent(eventId);
            return Ok();
        }

        [HttpPost]
        [Route("image/create")]
        public IActionResult CreateImage([FromBody] List<ImageDto> imageDtos)
        {
            var image = _imageService.CreateImages(imageDtos);
            if (image is null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut]
        [Route("image/update/{imageId}")]
        public IActionResult UpdateImage(int imageId, [FromBody] ImageDto imageDto)
        {
            return Ok(_imageService.UpdateImage(imageId, imageDto));
        }

        [HttpDelete]
        [Route("image/delete/{imageId}")]
        public IActionResult DeleteImage(int imageId)
        {
            _imageService.DeleteImage(imageId);
            return Ok();
        }

        [HttpGet]
        [Route("image/{imageId}")]
        public IActionResult GetImageById(int imageId)
        {
            return Ok(_imageService.GetImageById(imageId));
        }

        [HttpGet]
        [Route("image/filter")]
        public IActionResult GetImagesByFilter([FromBody] Expression<Func<Image, bool>> filter)
        {
            return Ok(_imageService.GetImagesByFilter(filter));
        }


        [HttpGet]
        [Route("image/all")]
        public IActionResult GetAllImages()
        {
            return Ok(_imageService.GetAllImages());
        }

        [HttpGet]
        [Route("interest/{interestId}")]
        public IActionResult GetInterestById(int interestId)
        {
            return Ok(_interestService.GetInterestById(interestId));
        }

        [HttpGet]
        [Route("interest/all")]
        public IActionResult GetAllInterests()
        {
            return Ok(_interestService.GetAllInterests());
        }

        [HttpGet]
        [Route("interest/filter")]
        public IActionResult GetInterestsByFilter([FromBody] Expression<Func<Interest, bool>> filter)
        {
            return Ok(_interestService.GetInterestsByFilter(filter));
        }

        [HttpPost]
        [Route("interest/create")]
        public IActionResult CreateInterest([FromBody] InterestDto interestDto)
        {
            var interest = _interestService.CreateInterest(interestDto);
            if (interest is null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut]
        [Route("interest/update/{interestId}")]
        public IActionResult UpdateInterest(int interestId, [FromBody] InterestDto interestDto)
        {
            return Ok(_interestService.UpdateInterest(interestId, interestDto));
        }

        [HttpDelete]
        [Route("interest/delete/{interestId}")]
        public IActionResult DeleteInterest(int interestId)
        {
            _interestService.DeleteInterest(interestId);
            return Ok();
        }

        [HttpGet]
        [Route("parameter/{parameterId}")]
        public IActionResult GetParameterById(int parameterId)
        {
            return Ok(_parameterService.GetParameterById(parameterId));
        }

        [HttpGet]
        [Route("parameter/all")]
        public IActionResult GetAllParameters()
        {
            return Ok(_parameterService.GetAllParameters());
        }

        [HttpGet]
        [Route("parameter/filter")]
        public IActionResult GetParametersByFilter([FromBody] Expression<Func<Parameter, bool>> filter)
        {
            return Ok(_parameterService.GetParametersByFilter(filter));
        }

        [HttpPost]
        [Route("parameter/create")]
        public IActionResult CreateParameter([FromBody] ParameterDto parameterDto)
        {
            var parameter = _parameterService.CreateParameter(parameterDto);
            if (parameter is null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut]
        [Route("parameter/update/{parameterId}")]
        public IActionResult UpdateParameter(int parameterId, [FromBody] ParameterDto parameterDto)
        {
            return Ok(_parameterService.UpdateParameter(parameterId, parameterDto));
        }

        [HttpDelete]
        [Route("parameter/delete/{parameterId}")]
        public IActionResult DeleteParameter(int parameterId)
        {
            _parameterService.DeleteParameter(parameterId);
            return Ok();
        }


        [HttpGet]
        [Route("tag/{tagId}")]
        public IActionResult GetTagById(int tagId)
        {
            return Ok(_tagService.GetTagById(tagId));
        }

        [HttpGet]
        [Route("tag/all")]
        public IActionResult GetAllTags()
        {
            return Ok(_tagService.GetAllTags());
        }

        [HttpPost]
        [Route("tag/create")]
        public IActionResult CreateTag([FromBody] TagDto tagDto)
        {
            var tag = _tagService.CreateTag(tagDto);
            if (tag is null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpGet]
        [Route("tag/filter")]
        public IActionResult GetTagsByFilter([FromBody] Expression<Func<Tag, bool>> filter)
        {
            return Ok(_tagService.GetTagsByFilter(filter));
        }

        [HttpPut]
        [Route("tag/update/{tagId}/{eventId}")]
        public IActionResult UpdateTag(int tagId, [FromBody] TagDto tagDto, int eventId)
        {
            return Ok(_tagService.UpdateTag(tagId, tagDto, eventId));
        }

        [HttpDelete]
        [Route("tag/delete/{tagId}")]
        public IActionResult DeleteTag(int tagId)
        {
            _tagService.DeleteTag(tagId);
            return Ok();
        }

    }
}
