namespace InstantMessagingApp.Application.Interfaces;

public interface IUserService
{
    void Register(string username, string password);
    void Login(string username, string password);
}