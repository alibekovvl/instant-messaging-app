using InstantMessagingApp.Application.Interfaces;
using InstantMessagingApp.Domain.Entities;
using InstantMessagingApp.Infrastructure.Data;

namespace InstantMessagingApp.Infrastructure.Repositories;

public class MessageRepository(AppDbContext dbContext) : IMessageRepository
{
    private readonly AppDbContext _dbContext = dbContext;
    public void Add(Message message)
    {
        _dbContext.Add(message);    
        _dbContext.SaveChanges();
    }
    public IEnumerable<Message> GetContent(string user1, string user2)
    {
        return _dbContext.Messages  
            .Where(m => 
                (m.Sender == user1 && m.Receiver == user2) ||
                (m.Sender == user2 && m.Receiver == user1))
            .OrderBy(m => m.SentAt)
            .ToList();
    }
    
}