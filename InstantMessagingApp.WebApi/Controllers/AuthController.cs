using InstantMessagingApp.Application.DTOs;
using InstantMessagingApp.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace InstantMessagingApp.Controllers;

[ApiController]
[Route("[controller]")]

public class AuthController(UserService userService) : ControllerBase
{
    [HttpPost("register")]
    public IActionResult Register([FromBody] RegisterUserRequest request)
    {
        userService.Register(request.Username, request.Password);
        return NoContent();
    }

    [HttpPost("login")]

    public IActionResult Login([FromBody] LoginUserRequest request)
    {
        try
        {
            userService.Login(request.Username, request.Password);
            return Ok();
        }
        catch (Exception e)
        {
            return Unauthorized();
        }
    }
}