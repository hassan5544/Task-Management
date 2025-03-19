using Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace Application.Commands.TaskCommands.AssignTask;

public class AssignTaskCommandHandler
{
    private readonly ITaskRepository _taskRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILogger<AssignTaskCommandHandler> _logger;
    
    public AssignTaskCommandHandler(ITaskRepository taskRepository, IUserRepository userRepository , ILogger<AssignTaskCommandHandler> logger)
    {
        _taskRepository = taskRepository;
        _userRepository = userRepository;
        _logger = logger;
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
    }
}