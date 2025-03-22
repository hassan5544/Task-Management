using Domain.Entities;
using Domain.Enums;
using Domain.Repositories;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TaskRepository : ITaskRepository
{
    
    private readonly ApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;

    public TaskRepository(ApplicationDbContext context , IUnitOfWork unitOfWork)
    {
        _context = context;
        _unitOfWork = unitOfWork;
        
    }
    public async Task<TaskItem> GetTaskByIdAsync(Guid id)
    {
        var result = await _context.Tasks.FindAsync(id);
        if(result == null)
        {
            throw new Exception("Task not found");
        }
        return result;
    }

    public async Task<List<TaskItem>> GetTasksAsync()
    {
        return await _context.Tasks.ToListAsync();
    }

    public async Task AddTaskAsync(TaskItem task , CancellationToken cancellationToken)
    {
        await _context.Tasks.AddAsync(task);
        await _unitOfWork.CommitAsync(cancellationToken);
        
    }

    public async Task UpdateTaskAsync(TaskItem task , CancellationToken cancellationToken)
    {
        _context.Tasks.Update(task);
        await _unitOfWork.CommitAsync(cancellationToken);
        
    }

    public async Task DeleteTaskAsync(TaskItem task , CancellationToken cancellationToken)
    {
        _context.Tasks.Remove(task);
        await _unitOfWork.CommitAsync(cancellationToken);
        
    }

    public async Task AssignTaskAsync(TaskItem task, User user  , CancellationToken cancellationToken)
    {
        task.AssignTo(user);
        _context.Tasks.Attach(task);
        await _unitOfWork.CommitAsync(cancellationToken);    
    }
    
    public  async Task<List<TaskItem>> GetPendingTasksAsync(int pageNumber, int pageSize)
    {
        var result = await _context.Tasks
            .Where(t => t.Status == TaskStatusEnum.Pending)
            .OrderBy(t => t.InsertDate)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking() // Disable EF tracking for better performance
            .ToListAsync();
        
        if(result == null)
        {
            throw new Exception("Task not found");
        }

        return result;
    }
}