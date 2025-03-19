using Application.DTOs;
using Domain.Entities;
using Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Commands.TaskCommands.CreateTask;

public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, TaskDto>
{
    private readonly ITaskRepository _taskRepository;
    private readonly ILogger<CreateTaskCommandHandler> _logger;
    
    public CreateTaskCommandHandler(ITaskRepository taskRepository , ILogger<CreateTaskCommandHandler> logger)
    {
        _taskRepository = taskRepository;
        _logger = logger;
        
    }
    public async Task<TaskDto> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        var task = TaskItem.Create(request.Title, request.Description);
        await _taskRepository.AddTaskAsync(task , cancellationToken);
        return new TaskDto
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            Status = task.Status.ToString(),
            CreatedBy = request.CreatedBy.ToString(),
            CreatedAt = task.InsertDate
        };
        
    }
}