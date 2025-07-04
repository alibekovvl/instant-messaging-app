﻿namespace InstantMessagingApp.Application.Interfaces;

public interface IUserService
{
    Task RegisterAsync(string username, string password);
    Task<string> LoginAsync(string username, string password);
}