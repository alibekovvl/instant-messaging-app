using InstantMessagingApp.Application.Interfaces;
using InstantMessagingApp.Domain.Entities;
using InstantMessagingApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InstantMessagingApp.Infrastructure.Repositories;

public class UserRepository(AppDbContext dbContext) : IUserRepository
{
    private readonly AppDbContext _dbContext = dbContext;
    public async Task AddAsync(User account)
    {
        await _dbContext.Users.AddAsync(account);
        await _dbContext.SaveChangesAsync();
    }
    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task  UpdateAsync(User? user)
    {
        _dbContext.Users.Update(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task SetTelegramChatIdAsync(string username, string telegramChatId)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == username);
        if (user != null)
        {
            user.TelegramChatId = telegramChatId;
            await _dbContext.SaveChangesAsync();
        }
    }
}