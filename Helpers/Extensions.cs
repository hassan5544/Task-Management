using Helpers.Implementations;
using Helpers.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Helpers;

public static class Extensions
{
    public static IServiceCollection AddHelpers(this IServiceCollection services)
    {
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        return services;
    }
}