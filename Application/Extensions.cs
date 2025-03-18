using System.Reflection;
using Application.Commands.UserCommands.RegisterUser;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly(), typeof(RegisterUserCommandHandler).Assembly));
        return services;
    }
}