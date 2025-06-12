using InstantMessagingApp.Application.Interfaces;
using InstantMessagingApp.Domain.Entities;

namespace InstantMessagingApp.Infrastructure.Repositories;

public class MessageRepository: IMessageRepository
{
    private static readonly List<Message> messages = new();
    
    public void Add(Message message)
    {
        messages.Add(message);    
    }
    
}