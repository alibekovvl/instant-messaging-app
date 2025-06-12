    using Microsoft.Extensions.DependencyInjection;

    namespace InstantMessagingApp.Infrastructure.Extensions;

    public static class CorsExtensions
    {
        public static IServiceCollection AddAppCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()
                        .SetIsOriginAllowed(_ => true); 
                });
            });
            return services;
        }
    }