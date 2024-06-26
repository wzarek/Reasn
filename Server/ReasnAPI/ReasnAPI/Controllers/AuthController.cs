using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ReasnAPI.Mappers;
using ReasnAPI.Models.Authentication;
using ReasnAPI.Models.DTOs;
using ReasnAPI.Services.Authentication;

namespace ReasnAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;
    private readonly TokenService _tokenService;

    public AuthController(AuthService authService, TokenService tokenService)
    {
        _authService = authService;
        _tokenService = tokenService;
    }

    [HttpPost("login")]
    [ProducesResponseType<TokenPayload>(StatusCodes.Status200OK)]
    public IActionResult Login(
        [FromBody] LoginRequest request,
        [FromServices] IValidator<LoginRequest> validator)
    {
        validator.ValidateAndThrow(request);
        var user = _authService.Login(request);

        var tokenPayload = _tokenService.GenerateToken(user);
        return Ok(tokenPayload);
    }

    [HttpPost("register")]
    [ProducesResponseType<UserDto>(StatusCodes.Status201Created)]
    public IActionResult Register(
        [FromBody] RegisterRequest request,
        [FromServices] IValidator<RegisterRequest> validator)
    {
        validator.ValidateAndThrow(request);
        var user = _authService.Register(request);

        var location = Url.Action(
            action: nameof(UsersController.GetUserByUsername),
            controller: "Users",
            values: new { username = user.Username });

        return Created(location, user.ToDto());
    }
}