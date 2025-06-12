﻿namespace InstantMessagingApp.Application.DTOs;

public class MessageDto
{
    public string Sender { get; set; }
    public string Receiver { get; set; }
    public string Content { get; set; }
    public DateTime SentAt { get; set; }
}