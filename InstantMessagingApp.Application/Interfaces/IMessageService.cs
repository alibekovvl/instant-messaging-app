using InstantMessagingApp.Application.DTOs;
using InstantMessagingApp.Domain.Entities;

namespace InstantMessagingApp.Application.Interfaces;

public interface IMessageService
{
    void SendMessage(string sender,SendMessageRequest request);
    IEnumerable<MessageDto> GetContent(string user1, string user2);
}