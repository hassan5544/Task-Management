using Application.Interfaces;
using Domain.Repositories;
using Infrastructure.Repositories;
using Infrastructure.Services;

using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ITaskRepository, TaskRepository>();
        services.AddScoped<INotificationService , NotificationService>();
        services.AddScoped<INotificationRepository, NotificationRepository>();
        return services;
    }
}