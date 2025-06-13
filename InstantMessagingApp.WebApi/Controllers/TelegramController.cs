using InstantMessagingApp.Application.DTOs;
using InstantMessagingApp.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InstantMessagingApp.Controllers;

public class TelegramController(IUserRepository userRepository) :ControllerBase
{
    [HttpPost("bind")]

    public async Task<IActionResult> BindTelegram([FromBody] BindTelegramRequest request)
    {
        var user = await userRepository.GetByUsernameAsync(request.Username);
        if (user == null) return NotFound();
        
        await userRepository.SetTelegramChatIdAsync(request.Username, request.TelegramChatId);
        return Ok();
    }
}