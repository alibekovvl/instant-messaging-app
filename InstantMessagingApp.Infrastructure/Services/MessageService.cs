using InstantMessagingApp.Application.DTOs;
using InstantMessagingApp.Application.Interfaces;
using InstantMessagingApp.Domain.Entities;

namespace InstantMessagingApp.Infrastructure.Services;

public class MessageService(IMessageRepository messageRepository,IUserRepository userRepository, ITelegramNotificationService service): IMessageService
{
    public async Task SendMessageAsync(string sender, SendMessageRequest request)
    {
        var message = new Message()
        {
            Id = Guid.NewGuid(),
            Sender = sender,
            Receiver = request.Reciever,
            Content = request.Content,
            SentAt = DateTime.UtcNow,
        };
        await messageRepository.AddAsync(message);
        
        var reciever = await userRepository.GetByUsernameAsync(request.Reciever);
        if (reciever != null && !reciever.IsOnline && !string.IsNullOrEmpty(reciever.TelegramChatId))
        {
            service.SendNotificationAsync(reciever.TelegramChatId, $"У вас новое сообщение от {sender}");
        }
    }

    public async Task<IEnumerable<MessageDto>> GetContentAsync(string user1, string user2)
    {
        return messageRepository.GetContentAsync(user1, user2)
            .Select(m => new MessageDto
            {
                Sender = m.Sender,
                Receiver = m.Receiver,
                Content = m.Content,
                SentAt = m.SentAt
            });
    }

    public async Task<List<Message>> GetMessageHistoryAsync(string user1, string user2)
    {
        return await messageRepository.GetMessagesBetweenUsersAsync(user1, user2);
    }
}