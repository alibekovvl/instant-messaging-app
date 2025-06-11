using InstantMessagingApp.Application.Interfaces;
using InstantMessagingApp.Domain.Entities;
using InstantMessagingApp.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
    
namespace InstantMessagingApp.Infrastructure.Services;

public class UserService(
    IUserRepository userRepository,
    JwtService jwtService): IUserService
{
    public void Register(string userName, string password)
    {
        var account = new User()
        {
            Username = userName,
            Id = Guid.NewGuid()
        };

        var hashPassword = new PasswordHasher<User>().HashPassword(account, password);
        account.PasswordHash = hashPassword;    
        userRepository.Add(account);
    }
    public string Login(string username, string password)
    {
        var account = userRepository.GetByUsername(username);
        var result = new PasswordHasher<User>()
            .VerifyHashedPassword(account, account.PasswordHash, password);
        
        if (result == PasswordVerificationResult.Success)
        {
            return jwtService.GenerateToken(account);
            
        }
        else
        {
            throw new Exception("Unauthorised");
        }
    }
}