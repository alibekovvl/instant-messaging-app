using InstantMessagingApp.Application.Interfaces;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace InstantMessagingApp.Infrastructure.Services;

public class TelegramNotificationService: ITelegramNotificationService
{
    private readonly TelegramBotClient _botClient;
    private readonly ILogger<TelegramNotificationService> _logger;
    private readonly string _botUsername;

    public TelegramNotificationService(string botToken, ILogger<TelegramNotificationService> logger)
    {
        _botClient = new TelegramBotClient(botToken);
        _logger = logger;
        
        var botInfo = _botClient.GetMeAsync().Result;
        _botUsername = botInfo.Username;
    }

    public async Task SendNotificationAsync(string telegramChatId, string telegramMessage)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(telegramMessage))
            {
                _logger.LogWarning("TelegramChatId is null or empty");
                return;
            }

            var chatId = new ChatId(long.Parse(telegramChatId));
            await _botClient.SendTextMessageAsync(
                chatId: chatId,
                text: telegramMessage,
                parseMode:
                ParseMode.Html
            );
            _logger.LogInformation("Telegram notification sent to {ChatId}: {Message}", telegramChatId,
                telegramMessage);
        
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error sending telegram notification to {ChatId}", telegramChatId);
        }
    }
    public async Task SendMessageNotificationAsync(string receiverUsername, string senderUsername, string messageContent)
    {
        var notificationMessage = $"�� <b>Новое сообщение</b>\n\n" +
                                  $"От: <b>{senderUsername}</b>\n" +
                                  $"Сообщение: {messageContent}\n\n" +
                                  $"Время: {DateTime.Now:HH:mm}";

        await SendNotificationAsync(receiverUsername, notificationMessage);
    }
    public async Task SendWelcomeMessageAsync(string telegramChatId, string username)
    {
        var welcomeMessage = $"👋 <b>Добро пожаловать в систему уведомлений!</b>\n\n" +
                             $"Пользователь: <b>{username}</b>\n" +
                             $"Теперь вы будете получать уведомления о новых сообщениях, когда вы офлайн.\n\n" +
                             $"Бот: @{_botUsername}";

        await SendNotificationAsync(telegramChatId, welcomeMessage);
    }
    public async Task SendUnbindMessageAsync(string telegramChatId, string username)
    {
        var unbindMessage = $"🔗 <b>Отвязка от Telegram</b>\n\n" +
                            $"Пользователь: <b>{username}</b>\n" +
                            $"Уведомления о новых сообщениях отключены.";

        await SendNotificationAsync(telegramChatId, unbindMessage);
    }
    public string GetBotUsername() => _botUsername;
}