using Application.Interfaces;
using Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Commands.TaskCommands.AssignTask;

public class AssignTaskCommandHandler : IRequestHandler<AssignTaskCommand>
{
    private readonly ITaskRepository _taskRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILogger<AssignTaskCommandHandler> _logger;
    private readonly INotificationService _notificationService;

    
    public AssignTaskCommandHandler(ITaskRepository taskRepository, IUserRepository userRepository , ILogger<AssignTaskCommandHandler> logger , INotificationService notificationService)
    {
        _taskRepository = taskRepository;
        _userRepository = userRepository;
        _logger = logger;
        _notificationService = notificationService;
    }
    
    public async Task Handle(AssignTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetTaskByIdAsync(request.TaskId);
        if (task == null)
        {
            _logger.LogWarning("Task {TaskId} not found", request.TaskId);

            throw new Exception("Task not found");
        }

        var user = await _userRepository.GetByIdAsync(request.UserId);
        if (user == null)
        {
            _logger.LogWarning("User {UserId} not found", request.UserId);

            throw new Exception("User not found");
        }

        await _taskRepository.AssignTaskAsync(task, user ,cancellationToken);
        await _notificationService.SendNotificationAsync(request.UserId, $"You have been assigned a new task: {task.Title}" , cancellationToken);

    }
}