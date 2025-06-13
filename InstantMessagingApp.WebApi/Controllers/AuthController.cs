using InstantMessagingApp.Application.DTOs;
using InstantMessagingApp.Application.Interfaces;
using InstantMessagingApp.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace InstantMessagingApp.Controllers;

[ApiController]
[Route("[controller]")]

public class AuthController(IUserService userService) : ControllerBase
{
    [HttpPost("register")]
    public IActionResult Register([FromBody] RegisterUserRequest request)
    {
        userService.RegisterAsync(request.Username, request.Password);
        return NoContent();
    }
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginUserRequest request)
    {
        try
        {
            var token = userService.LoginAsync(request.Username, request.Password);
            return Ok(token);
        }
        catch 
        {
            return Unauthorized();
        }
    }
}