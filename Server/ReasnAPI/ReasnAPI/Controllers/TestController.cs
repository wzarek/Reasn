using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReasnAPI.Models.DTOs;
using ReasnAPI.Services;

namespace ReasnAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : Controller
    {
        
        private readonly TagService _tagService;

        public TestController(TagService tagService)
        {
            _tagService = tagService;
        }

        [HttpGet]
        public IActionResult Add()
        {

            var tagDto = new TagDto
            {
                Name = "TestTagasd"
            };

            var result = _tagService.CreateTag(tagDto);

            return Ok(result);
        }
    }
}
