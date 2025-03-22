using Application.DTOs;
using MediatR;

namespace Application.Queries.GetUserNotifications;

public record GetUserNotificationsQuery(Guid UserId) : IRequest<List<NotificationDto>>;
