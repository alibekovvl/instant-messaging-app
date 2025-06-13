namespace InstantMessagingApp.Application.Interfaces;

public interface ITelegramNotificationService
{
    Task SendNotificationAsync(string telegramChatId, string telegramMessage);
}