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
    public async Task<IActionResult> SendMessage([FromBody] SendMessageRequest request)
    {
        var reciever = User.FindFirst("userName")?.Value;
        if (request == null) return Unauthorized();
        
        await service.SendMessageAsync(reciever, request); 
        return Ok();
    }

    [HttpGet("{username}")]
    public async Task<IActionResult> GetMessages(string username)
    {
        var currentUsername = User.FindFirst("userName")?.Value;
        if (currentUsername == null) return Unauthorized();
        var messages = await service.GetContentAsync(currentUsername, username);
        return Ok(messages);
    }

    [HttpGet("history")]
    public async Task<IActionResult> GetMessagesHistory([FromQuery] string user1, [FromQuery] string user2)
    {
        var messages = await service.GetMessageHistoryAsync(user1, user2);
        return Ok(messages);
    }
}