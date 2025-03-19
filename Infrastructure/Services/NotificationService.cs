using Domain.Entities;
using Domain.Repositories;
using Hangfire;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services;

public class NotificationService : INotificationService
{
    private readonly INotificationRepository _notificationRepository;
    private readonly ILogger<NotificationService> _logger;
    private readonly IUserRepository _userRepository;

    public NotificationService(INotificationRepository notificationRepository, ILogger<NotificationService> logger , IUserRepository userRepository)
    {
        _notificationRepository = notificationRepository;
        _logger = logger;
        _userRepository = userRepository;
    }

    public async Task SendNotificationAsync(Guid userId, string message , CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        var notification = Notification.Create(message, user);
    

        await _notificationRepository.AddNotificationAsync(notification ,cancellationToken);

        BackgroundJob.Enqueue(() => ProcessNotification(notification));
    }
    public async Task ProcessNotification(Notification notification)
    {
        _logger.LogInformation("Processing notification for User {UserId}: {Message}", notification.User.Id, notification.Message);
    }

}