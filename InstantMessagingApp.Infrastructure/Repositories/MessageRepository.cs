using InstantMessagingApp.Application.Interfaces;
using InstantMessagingApp.Domain.Entities;
using InstantMessagingApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InstantMessagingApp.Infrastructure.Repositories;

public class MessageRepository(AppDbContext dbContext) : IMessageRepository
{
    private readonly AppDbContext _dbContext = dbContext;
    public async Task AddAsync(Message message)
    {
        await _dbContext.AddAsync(message);    
        await _dbContext.SaveChangesAsync();
    }
    public async Task<IEnumerable<Message>> GetContentAsync(string user1, string user2)
    {
        return await _dbContext.Messages  
            .Where(m => 
                (m.Sender == user1 && m.Receiver == user2) ||
                (m.Sender == user2 && m.Receiver == user1))
            .OrderBy(m => m.SentAt)
            .ToListAsync();
    }

    public async Task<List<Message>> GetMessagesBetweenUsersAsync(string user1, string user2)
    {
        return await _dbContext.Messages
            .Where(m =>
                (m.Sender == user1 && m.Receiver == user2) ||
                (m.Sender == user2 && m.Receiver == user1))
            .OrderBy(m => m.SentAt)
            .ToListAsync();
    }
}