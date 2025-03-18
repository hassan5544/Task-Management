using Domain.Repositories;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories;

public class UnitOfWork: IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task CommitAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }

    public Task RollbackAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}