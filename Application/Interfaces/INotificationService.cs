﻿namespace Application.Interfaces;

public interface INotificationService
{
    Task SendNotificationAsync(Guid userId, string message , CancellationToken cancellationToken);
}