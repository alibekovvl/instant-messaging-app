using InstantMessagingApp.Application.Interfaces;
using InstantMessagingApp.Domain.Entities;
using InstantMessagingApp.Infrastructure.Data;

namespace InstantMessagingApp.Infrastructure.Repositories;

public class UserRepository(AppDbContext dbContext) : IUserRepository
{
    private readonly AppDbContext _dbContext = dbContext;
    public void Add(User account)
    {
        _dbContext.Users.Add(account);
        _dbContext.SaveChanges();
    }
    public User? GetByUsername(string username)
    {
        return _dbContext.Users.FirstOrDefault(u => u.Username == username);
    }
    
}