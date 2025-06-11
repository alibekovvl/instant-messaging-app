using InstantMessagingApp.Application.Interfaces;
using InstantMessagingApp.Domain.Entities;

namespace InstantMessagingApp.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private static IDictionary<string, User> Users = new Dictionary<string, User>();

    public void Add(User account)
    {
        Users[account.Username] = account;
    }
    public User? GetByUsername(string username)
    {
        return Users.TryGetValue(username, out var account) ? account : null;
    }
    
}