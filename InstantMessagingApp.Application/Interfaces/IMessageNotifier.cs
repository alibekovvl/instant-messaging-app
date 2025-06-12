using InstantMessagingApp.Domain.Entities;

namespace InstantMessagingApp.Application.Interfaces;

public interface IMessageNotifier
{
    Task NotifyAsync(string sender, string reciever, string message);
}