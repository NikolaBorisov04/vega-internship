using Events.Api.Security;
using Events.Api.Services;

namespace Events.Api.Extensions;

public static class SecurityServicesExtensions
{
    public static IServiceCollection AddSecurityServices(this IServiceCollection services)
    {
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<ITokenService, TokenService>();

        return services;
    }
}