using InstantMessagingApp.Application.DTOs;
using InstantMessagingApp.Application.Interfaces;
using InstantMessagingApp.Domain.Entities;

namespace InstantMessagingApp.Infrastructure.Services;

public class MessageService(IMessageRepository repository): IMessageService
{

    public void SendMessage(string sender, SendMessageRequest request)
    {
        var message = new Message()
        {
            Id = Guid.NewGuid(),
            Sender = sender,
            Receiver = request.Reciever,
            Content = request.Content,
            SentAt = DateTime.UtcNow,
        };
        repository.Add(message);
    }

    public IEnumerable<MessageDto> GetContent(string user1, string user2)
    {
        return repository.GetContent(user1, user2)
            .Select(m => new MessageDto
            {
                Sender = m.Sender,
                Receiver = m.Receiver,
                Content = m.Content,
                SentAt = m.SentAt
            });
    }
}