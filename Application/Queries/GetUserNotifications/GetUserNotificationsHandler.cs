using Application.DTOs;
using Domain.Repositories;
using MediatR;

namespace Application.Queries.GetUserNotifications;

public class GetUserNotificationsHandler: IRequestHandler<GetUserNotificationsQuery, List<NotificationDto>>
{
    private readonly INotificationRepository _notificationRepository;

    public GetUserNotificationsHandler(INotificationRepository notificationRepository)
    {
        _notificationRepository = notificationRepository;
    }

    public async Task<List<NotificationDto>> Handle(GetUserNotificationsQuery request, CancellationToken cancellationToken)
    {
        var notifications = await _notificationRepository.GetNotificationsByUserIdAsync(request.UserId);
        return notifications.Select(n => new NotificationDto
        {
            CreatedAt = n.InsertDate,
            Message = n.Message,
        }).ToList();
    }
}