using InstantMessagingApp.Application.DTOs;
using InstantMessagingApp.Application.Interfaces;
using InstantMessagingApp.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InstantMessagingApp.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class MessageController(IMessageService service) : ControllerBase
{
    [HttpPost("send")]
    public IActionResult SendMessage([FromBody] SendMessageRequest request)
    {
        var reciever = User.FindFirst("username")?.Value;
        if (request == null) return Unauthorized();
        
        service.SendMessage(reciever, request); 
        return Ok();
    }
}