using InstantMessagingApp.Application.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace InstantMessagingApp.Infrastructure.Services.SignalR;

public class MessageNotifier(IHubContext<ChatHub> hubContext) : IMessageNotifier
{
    private readonly IHubContext<ChatHub> _hubContext = hubContext;

    public async Task NotifyAsync(string sender, string reciever, string message)
    {
        await _hubContext.Clients.User(reciever)
            .SendAsync("ReceiveMessage", sender, message);
    }
}