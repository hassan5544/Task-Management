using Application.DTOs;
using Domain.Repositories;
using MediatR;

namespace Application.Queries.GetPendingTasks;

public class GetPendingTasksQueryHandler :  IRequestHandler<GetPendingTasksQuery, List<TaskDto>>
{
    private readonly ITaskRepository _taskRepository;
    
    public GetPendingTasksQueryHandler(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }
    
    public async Task<List<TaskDto>> Handle(GetPendingTasksQuery request, CancellationToken cancellationToken)
    {
        var tasks = await _taskRepository.GetPendingTasksAsync();
        return tasks.Select(t => new TaskDto
        {
            Id = t.Id,
            Title = t.Title,
            Description = t.Description,
            Status = t.Status.ToString(),
            AssignedTo = t.AssignedUser != null ? t.AssignedUser?.Username.ToString() : t.AssignedUser?.Username,
        }).ToList();
    }

}