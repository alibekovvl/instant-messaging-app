namespace InstantMessagingApp.Domain.Entities;

public class Message
{
    public Guid Id { get; set; }
    public string Sender { get; set; }
    public string Receiver { get; set; }
    public string Content { get; set; }
    public DateTime SentAt { get; set; }
}