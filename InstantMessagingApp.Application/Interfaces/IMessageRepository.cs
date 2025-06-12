using InstantMessagingApp.Domain.Entities;

namespace InstantMessagingApp.Application.Interfaces;

public interface IMessageRepository
{
    void Add(Message message);
    IEnumerable<Message> GetContent(string user1, string user2);
}