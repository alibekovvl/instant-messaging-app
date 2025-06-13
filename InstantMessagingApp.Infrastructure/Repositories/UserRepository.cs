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
    
}