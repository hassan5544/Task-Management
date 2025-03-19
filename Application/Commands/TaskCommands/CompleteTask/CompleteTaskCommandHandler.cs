using Application.Commands.TaskCommands.CreateTask;
using Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Commands.TaskCommands.CompleteTask;

public class CompleteTaskCommandHandler : IRequestHandler<CompleteTaskCommand>
{
    private readonly ITaskRepository _taskRepository;
    private readonly ILogger<CompleteTaskCommandHandler> _logger; //

    public CompleteTaskCommandHandler(ITaskRepository taskRepository ,ILogger<CompleteTaskCommandHandler> logger )
    {
        _taskRepository = taskRepository;
        _logger = logger;

    }
    public async Task Handle(CompleteTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetTaskByIdAsync(request.TaskId);
        if (task == null)
        {
            _logger.LogWarning("Task {TaskId} not found", request.TaskId);
            throw new Exception("Task not found");
        }
        task.MarkAsCompleted();
        await _taskRepository.UpdateTaskAsync(task, cancellationToken);
    }
}