using MediatR;

namespace Application.Commands.TaskCommands.AssignTask;

public class AssignTaskCommand : IRequest
{
    public Guid TaskId { get; set; }
    public Guid UserId { get; set; }
}