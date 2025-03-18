namespace Domain.Repositories;

public interface IUnitOfWork
{
    Task CommitAsync(CancellationToken cancellationToken );
    Task RollbackAsync(CancellationToken cancellationToken );
}