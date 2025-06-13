using InstantMessagingApp.Application.Interfaces;
using InstantMessagingApp.Domain.Entities;
using InstantMessagingApp.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
    
namespace InstantMessagingApp.Infrastructure.Services;

public class UserService(
    IUserRepository userRepository,
    JwtService jwtService): IUserService
{
    public async Task RegisterAsync(string userName, string password)
    {
        var account = new User()
        {
            Username = userName,
            Id = Guid.NewGuid(),
            IsOnline = false
        };

        var hashPassword =  new PasswordHasher<User>().HashPassword(account, password);
        account.PasswordHash = hashPassword;    
        await userRepository.AddAsync(account);
    }
    public async Task<string> LoginAsync(string username, string password)
    {
        var account = await userRepository.GetByUsernameAsync(username);
        var result = new PasswordHasher<User>()
            .VerifyHashedPassword(account, account.PasswordHash, password);
        
        if (result == PasswordVerificationResult.Success)
        {
            account.IsOnline = true;
            await userRepository.UpdateAsync(account);
            return jwtService.GenerateToken(account);
        }
        else
        {
            throw new Exception("Unauthorised");
        }
    }
}