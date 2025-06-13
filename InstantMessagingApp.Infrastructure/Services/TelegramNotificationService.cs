using InstantMessagingApp.Application.Interfaces;
using Telegram.Bot;

namespace InstantMessagingApp.Infrastructure.Services;

public class TelegramNotificationService: ITelegramNotificationService
{
    private readonly TelegramBotClient _botClient;

    public TelegramNotificationService(string botToken)
    {
        _botClient = new TelegramBotClient(botToken);
    }

    public async Task SendNotificationAsync(string telegramChatId, string telegramMessage)
    {
        await _botClient.SendTextMessageAsync(chatId: telegramChatId, text: telegramMessage);
    }
}