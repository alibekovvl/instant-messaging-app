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

    public IEnumerable<Message> GetContent(string user1, string user2)
    {
        return messages
            .Where(m => 
                (m.Sender == user1 && m.Sender == user2) ||
                (m.Sender == user2 && m.Sender == user1))
            .OrderBy(m => m.SentAt);
    }
    
}