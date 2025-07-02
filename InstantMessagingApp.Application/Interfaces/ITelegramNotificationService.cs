namespace InstantMessagingApp.Application.Interfaces;

public interface ITelegramNotificationService
{
    Task SendNotificationAsync(string telegramChatId, string message);
    Task SendMessageNotificationAsync(string receiverUsername, string senderUsername, string messageContent);
    Task SendWelcomeMessageAsync(string telegramChatId, string username);
    Task SendUnbindMessageAsync(string telegramChatId, string username);
    string GetBotUsername();
}