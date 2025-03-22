using Application.Commands.TaskCommands.AssignTask;
using Application.Commands.TaskCommands.CompleteTask;
using Application.Commands.TaskCommands.CreateTask;
using Application.Queries.GetPendingTasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[ApiController]
[Route("api/tasks")]
[Authorize]
public class TasksController : ControllerBase
{
    private readonly IMediator _mediator;

    public TasksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTask([FromBody] CreateTaskCommand command)
    {
        var taskId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetPendingTasks), new { id = taskId });
    }

    [HttpPut("{id}/assign")]
    public async Task<IActionResult> AssignTask(Guid id, [FromBody] AssignTaskCommand command)
    {
        if (id != command.TaskId) return BadRequest("Task ID mismatch");

        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPut("{id}/complete")]
    public async Task<IActionResult> CompleteTask(Guid id)
    {
        await _mediator.Send(new CompleteTaskCommand(id));
        return NoContent();
    }

    [HttpGet("pending")]
    public async Task<IActionResult> GetPendingTasks(int pageNumber , int pageSize)
    {
        var tasks = await _mediator.Send(new GetPendingTasksQuery(pageNumber, pageSize));
        return Ok(tasks);
    }
}