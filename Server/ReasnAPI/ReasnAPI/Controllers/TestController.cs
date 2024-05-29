using Microsoft.AspNetCore.Mvc;
using ReasnAPI.Models.Database;
using ReasnAPI.Models.DTOs;
using ReasnAPI.Services;

namespace ReasnAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class TestController(AddressService addressService, CommentService commentService, ParticipantService participantService, UserService userService) : Controller
    {

        /**************************************************************
            ADDRESS SERVICES
         **************************************************************/

        [HttpGet]
        [Route("addresses")]
        public IActionResult GetAllAddresses()
        {
            return Ok(addressService.GetAllAddresses());
        }

        [HttpGet]
        [Route("addresses/{addressId}")]
        public IActionResult GetAddressById(int addressId)
        {
            return Ok(addressService.GetAddressById(addressId));
        }

        [HttpPost]
        [Route("addresses/create")]
        public IActionResult CreateAddress(AddressDto addressDto)
        {
            return Ok(addressService.CreateAddress(addressDto));
        }

        [HttpPut]
        [Route("addresses/update/{addressId}")]
        public IActionResult UpdateAddress(int addressId, AddressDto addressDto)
        {
            return Ok(addressService.UpdateAddress(addressId, addressDto));
        }

        [HttpDelete]
        [Route("addresses/delete/{addressId}")]
        public IActionResult DeleteAddress(int addressId)
        {
            addressService.DeleteAddress(addressId);
            return Ok();
        }

        /**************************************************************
            COMMENT SERVICES
         **************************************************************/

        [HttpGet]
        [Route("comments")]
        public IActionResult GetAllComments()
        {
            return Ok(commentService.GetAllComments());
        }

        [HttpGet]
        [Route("comments/{commentId}")]
        public IActionResult GetCommentById(int commentId)
        {
            return Ok(commentService.GetCommentById(commentId));
        }

        [HttpPost]
        [Route("comments/create")]
        public IActionResult CreateComment(CommentDto commentDto)
        {
            return Ok(commentService.CreateComment(commentDto));
        }

        [HttpPut]
        [Route("comments/update/{commentId}")]
        public IActionResult UpdateComment(int commentId, CommentDto commentDto)
        {
            return Ok(commentService.UpdateComment(commentId, commentDto));
        }

        [HttpDelete]
        [Route("comments/delete/{commentId}")]
        public IActionResult DeleteComment(int commentId)
        {
            commentService.DeleteComment(commentId);
            return Ok();
        }


        /**************************************************************
            PARTICIPANT SERVICES
         **************************************************************/

        [HttpGet]
        [Route("participants")]
        public IActionResult GetAllParticipants()
        {
            return Ok(participantService.GetAllParticipants());
        }

        [HttpGet]
        [Route("participants/{participantId}")]
        public IActionResult GetParticipantById(int participantId)
        {
            return Ok(participantService.GetParticipantById(participantId));
        }

        [HttpPost]
        [Route("participants/create")]
        public IActionResult CreateParticipant(ParticipantDto participantDto)
        {
            return Ok(participantService.CreateParticipant(participantDto));
        }

        [HttpPut]
        [Route("participants/update/{participantId}")]
        public IActionResult UpdateParticipant(int participantId, ParticipantDto participantDto)
        {
            return Ok(participantService.UpdateParticipant(participantId, participantDto));
        }

        [HttpDelete]
        [Route("participants/delete/{participantId}")]
        public IActionResult DeleteParticipant(int participantId)
        {
            participantService.DeleteParticipant(participantId);
            return Ok();
        }

        /**************************************************************
            USER SERVICES
         *************************************************************/

        [HttpGet]
        [Route("users")]
        public IActionResult GetAllUsers()
        {
            return Ok(userService.GetAllUsers());
        }

        [HttpGet]
        [Route("users/{userId}")]
        public IActionResult GetUserById(int userId)
        {
            return Ok(userService.GetUserById(userId));
        }

        [HttpPost]
        [Route("users/create")]
        public IActionResult CreateUser(UserDto userDto)
        {
            return Ok(userService.CreateUser(userDto));
        }

        [HttpPut]
        [Route("users/update/{userId}")]
        public IActionResult UpdateUser(int userId, UserDto userDto)
        {
            return Ok(userService.UpdateUser(userId, userDto));
        }

        [HttpDelete]
        [Route("users/delete/{userId}")]
        public IActionResult DeleteUser(int userId)
        {
            userService.DeleteUser(userId);
            return Ok();
        }
    }
}
