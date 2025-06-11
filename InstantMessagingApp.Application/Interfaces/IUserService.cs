namespace InstantMessagingApp.Application.Interfaces;

public interface IUserService
{
    void Register(string username, string password);
    string Login(string username, string password);
}