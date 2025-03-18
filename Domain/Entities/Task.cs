using Domain.Enums;
using Domain.Shared;

namespace Domain.Entities;

public class Task : BaseEntity
{
    public string Title { get; private set; }
    public string Description { get; private set; }
    public TaskStatusEnum Status { get; private set; }
    public Guid? AssignedUserId { get; private set; }
    public User? AssignedUser { get; private set; }

    private Task()
    {
        // Required by EF

    } 

    private Task(string title, string description)
    {
        Title = title;
        Description = description;
        Status = TaskStatusEnum.Pending;
    }

    public static Task Create(string title, string description)
    {
        return new Task(title, description);
    }
    
    public void AssignTo(User user)
    {
        if (AssignedUserId != null)
            throw new InvalidOperationException("Task is already assigned!");

        AssignedUser = user;
        AssignedUserId = user.Id;
        user.AssignTask(this);
    }

    public void MarkAsCompleted()
    {
        if (Status == TaskStatusEnum.Completed)
            throw new InvalidOperationException("Task is already completed!");

        Status = TaskStatusEnum.Completed;
    }
}