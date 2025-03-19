using Application.DTOs;
using Domain.Entities;
using MediatR;

namespace Application.Queries.GetPendingTasks;

public record GetPendingTasksQuery() : IRequest<List<TaskDto>>;
