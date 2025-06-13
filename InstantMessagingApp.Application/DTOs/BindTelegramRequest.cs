namespace InstantMessagingApp.Application.DTOs;

public class BindTelegramRequest
{
    public string Username{ get; set; }
    public string TelegramChatId { get; set; } = null!;
}