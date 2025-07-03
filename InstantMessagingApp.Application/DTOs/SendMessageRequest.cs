namespace InstantMessagingApp.Application.DTOs;

public class SendMessageRequest
{
    public string Receiver { get; set; }
    public string Content { get; set; }
}