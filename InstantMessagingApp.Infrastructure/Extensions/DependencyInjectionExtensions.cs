using InstantMessagingApp.Application.Interfaces;
using InstantMessagingApp.Infrastructure.Repositories;
using InstantMessagingApp.Infrastructure.Services;
using InstantMessagingApp.Infrastructure.Services.SignalR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace InstantMessagingApp.Infrastructure.Extensions;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddAppServices(this IServiceCollection services,IConfiguration configuration)
    {
        var botToken = configuration["Telegram:BotToken"];

        services
            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped<IMessageNotifier, MessageNotifier>()
            .AddScoped<IUserService, UserService>()
            .AddScoped<IMessageRepository, MessageRepository>()
            .AddScoped<IMessageService, MessageService>()
            .AddSingleton<IUserIdProvider, NameUserIdProvider>()
            .AddScoped<JwtService>()
            .AddSingleton<ITelegramNotificationService>(provider => 
            new TelegramNotificationService(botToken, provider.GetRequiredService<ILogger<TelegramNotificationService>>()));

        return services;
    }
}