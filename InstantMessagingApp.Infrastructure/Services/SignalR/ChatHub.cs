using Microsoft.AspNetCore.SignalR;

namespace InstantMessagingApp.Infrastructure.Services.SignalR;

public class ChatHub : Hub
{
    public async Task SendMessageToUser(string receiver, string message)
    {
        await Clients.User(receiver)
            .SendAsync("ReceiveMessage", Context.User?.Identity?.Name ?? "Unknown", message);
    }
}