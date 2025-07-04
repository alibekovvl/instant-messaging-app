﻿namespace InstantMessagingApp.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public string? TelegramChatId { get; set; }
    public bool IsOnline { get; set; }
}