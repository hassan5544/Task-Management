using Domain.Entities;

namespace Domain.Repositories;

public interface IUserRepository
{
    Task<User> GetByEmailAsync(string email);
    System.Threading.Tasks.Task AddUserAsync(User user);
    Task<bool> EmailExistsAsync(string email);
}