using Domain.Entities;

namespace Domain.Repositories;

public interface INotificationRepository
{
    Task AddNotificationAsync(Notification notification , CancellationToken cancellationToken);
    Task<List<Notification>> GetNotificationsByUserIdAsync(Guid userId);
}