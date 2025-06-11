using InstantMessagingApp.Domain.Entities;

namespace InstantMessagingApp.Application.Interfaces;

public interface IUserRepository
{
    User GetByUsername(string username);
    void Add(User user);
}