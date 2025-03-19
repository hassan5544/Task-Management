

using Domain.Entities;

namespace Domain.Repositories;

public interface ITaskRepository
{
    public Task<TaskItem> GetTaskByIdAsync(Guid id);
    
    public Task<List<TaskItem>> GetTasksAsync();
    
    public Task AddTaskAsync(TaskItem task , CancellationToken cancellationToken);
    
    public Task UpdateTaskAsync(TaskItem task , CancellationToken cancellationToken);
    
    public Task DeleteTaskAsync(TaskItem task , CancellationToken cancellationToken);
    
    public Task AssignTaskAsync(TaskItem task, User user , CancellationToken cancellationToken);
    
    public Task<List<TaskItem>> GetPendingTasksAsync();
    
}