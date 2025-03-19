using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;
public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;

    public UserRepository(ApplicationDbContext context , IUnitOfWork unitOfWork)
    {
        _context = context;
        _unitOfWork = unitOfWork;
    }

    public async Task<User> GetByIdAsync(Guid id)
    {
        var result =  await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        if(result == null)
        {
            throw new Exception("User not found");
        }
        return result;
    }
    public async Task<User> GetByEmailAsync(string email)
    {
        var result = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        if(result == null)
        {
            throw new Exception("User not found");
        }

        return result;
    }

    public async Task AddUserAsync(User user , CancellationToken cancellationToken)
    {
        _context.Users.Add(user);
        await _unitOfWork.CommitAsync(cancellationToken);
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
        return await _context.Users.AnyAsync(u => u.Email == email);
    }
}