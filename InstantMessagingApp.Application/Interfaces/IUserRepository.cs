using InstantMessagingApp.Domain.Entities;

namespace InstantMessagingApp.Application.Interfaces;

public interface IUserRepository
{
    Task<User> GetByUsernameAsync(string username);
    Task AddAsync(User? user);
}