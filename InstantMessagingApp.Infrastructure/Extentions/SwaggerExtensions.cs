using Microsoft.Extensions.DependencyInjection;

namespace InstantMessagingApp.Infrastructure.Extentions;

public static class SwaggerExtensions
{
    public static IServiceCollection AddAppSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        return services;
    }
}