using InstantMessagingApp.Application.DTOs;
using InstantMessagingApp.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InstantMessagingApp.Controllers;
[ApiController]
[Route("api/[controller]")]
public class TelegramController :ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly ITelegramNotificationService _telegramService;
    private readonly ILogger<TelegramController> _logger;

    public TelegramController(
        IUserRepository userRepository,
        ITelegramNotificationService telegramNotificationService,
        ILogger<TelegramController> logger)
    {
        _userRepository = userRepository;
        _telegramService = telegramNotificationService;
        _logger = logger;
    }


    [HttpPost("bind")]
    [Authorize]
    public async Task<IActionResult> BindTelegram([FromBody] BindTelegramRequest request)
    {

        try
        {
            var user = await _userRepository.GetByUsernameAsync(request.Username);
            if (user == null)
            {
                return NotFound("User not found");
            }
            var currentUsername = User.Identity?.Name;
            if (currentUsername != user.Username)
            {
                return Forbid();
            }
            await _userRepository.SetTelegramChatIdAsync(request.Username, request.TelegramChatId);
            await _telegramService.SendWelcomeMessageAsync(request.TelegramChatId, request.Username);
            _logger.LogInformation("User {Username} bound Telegram chat ID {ChatId}", request.Username, request.TelegramChatId);

            return Ok(new { 
                message = "Telegram успешно привязан",
                botUsername = _telegramService.GetBotUsername()
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error binding Telegram for user {Username}", request.Username);
            return StatusCode(500, new { message = "Ошибка при привязке Telegram" });
        }
        
        return Ok();
    }
    
    [HttpPost("unbind")]
    [Authorize]
    public async Task<IActionResult> UnbindTelegram([FromBody] UnbindTelegramRequest request)
    {
        try
        {
            var user = await _userRepository.GetByUsernameAsync(request.Username);
            if (user == null)
            {
                return NotFound(new { message = "Пользователь не найден" });
            }

            // Проверяем, что пользователь отвязывает свой аккаунт
            var currentUsername = User.Identity?.Name;
            if (currentUsername != request.Username)
            {
                return Forbid();
            }

            var oldChatId = user.TelegramChatId;
            await _userRepository.SetTelegramChatIdAsync(request.Username, null);

            // Отправляем сообщение об отвязке
            if (!string.IsNullOrEmpty(oldChatId))
            {
                await _telegramService.SendUnbindMessageAsync(oldChatId, request.Username);
            }

            _logger.LogInformation("User {Username} unbound Telegram", request.Username);
            
            return Ok(new { message = "Telegram успешно отвязан" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error unbinding Telegram for user {Username}", request.Username);
            return StatusCode(500, new { message = "Ошибка при отвязке Telegram" });
        }
    }
}