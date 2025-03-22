using Application.DTOs;
using Domain.Entities;
using MediatR;

namespace Application.Queries.GetPendingTasks;

public record GetPendingTasksQuery(int pageNumber, int pageSize) : IRequest<List<TaskDto>>;
