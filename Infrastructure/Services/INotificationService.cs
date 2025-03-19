namespace Infrastructure.Services;

public interface INotificationService
{
    Task SendNotificationAsync(Guid userId, string message , CancellationToken cancellationToken);
}