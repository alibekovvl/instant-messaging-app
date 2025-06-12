using InstantMessagingApp.Application.Interfaces;
using InstantMessagingApp.Infrastructure.Repositories;
using InstantMessagingApp.Infrastructure.Services;
using InstantMessagingApp.Infrastructure.Services.SignalR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;

namespace InstantMessagingApp.Infrastructure.Extentions;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        services
            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped<IMessageNotifier, MessageNotifier>()
            .AddScoped<IUserService, UserService>()
            .AddScoped<IMessageRepository, MessageRepository>()
            .AddScoped<IMessageService, MessageService>()
            .AddSingleton<IUserIdProvider, NameUserIdProvider>()
            .AddScoped<JwtService>();
        return services;
    }
}