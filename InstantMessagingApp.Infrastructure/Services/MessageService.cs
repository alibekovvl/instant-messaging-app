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
        
        var receiver = await userRepository.GetByUsernameAsync(request.Reciever);
        if (receiver != null && !receiver.IsOnline && !string.IsNullOrEmpty(receiver.TelegramChatId))
        {
            await service.SendMessageNotificationAsync(
                receiver.TelegramChatId, 
                sender, 
                request.Content
            ); 
        }
    }

    public async Task<IEnumerable<MessageDto>> GetContentAsync(string user1, string user2)
    {
        var messages = await messageRepository.GetContentAsync(user1, user2);
        return messages.Select(m => new MessageDto
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