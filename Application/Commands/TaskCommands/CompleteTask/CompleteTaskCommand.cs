using MediatR;

namespace Application.Commands.TaskCommands.CompleteTask;

public record CompleteTaskCommand(Guid TaskId) : IRequest;
