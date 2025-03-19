using Application.DTOs;
using MediatR;

namespace Application.Commands.TaskCommands.CreateTask;

public class CreateTaskCommand: IRequest<TaskDto>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public Guid CreatedBy { get; set; }
}