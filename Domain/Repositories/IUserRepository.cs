using Domain.Entities;

namespace Domain.Repositories;

public interface IUserRepository
{
    Task<User> GetByEmailAsync(string email);
    Task<User> GetByIdAsync(Guid id);
    Task AddUserAsync(User user , CancellationToken cancellationToken);
    Task<bool> EmailExistsAsync(string email);
}