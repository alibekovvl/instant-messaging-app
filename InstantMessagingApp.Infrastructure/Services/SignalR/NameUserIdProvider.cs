using System.Security.Claims;
using Microsoft.AspNetCore.SignalR;

namespace InstantMessagingApp.Infrastructure.Services.SignalR;

public class NameUserIdProvider : IUserIdProvider
{
    public string? GetUserId(HubConnectionContext connection)
    {
        return connection.User?.FindFirst("userName")?.Value;
    }
}