using InstantMessagingApp.Application.DTOs;
using InstantMessagingApp.Domain.Entities;

namespace InstantMessagingApp.Application.Interfaces;

public interface IMessageService
{
    Task SendMessageAsync(string sender,SendMessageRequest request);
    Task<IEnumerable<MessageDto>> GetContentAsync(string user1, string user2);
    Task <List<Message>> GetMessageHistoryAsync(string user1, string user2);
}