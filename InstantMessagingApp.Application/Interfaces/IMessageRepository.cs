using InstantMessagingApp.Domain.Entities;

namespace InstantMessagingApp.Application.Interfaces;

public interface IMessageRepository
{
    Task AddAsync(Message message);
    Task<IEnumerable<Message>> GetContentAsync(string user1, string user2);
    Task<List<Message>> GetMessagesBetweenUsersAsync(string user1, string user2);
}