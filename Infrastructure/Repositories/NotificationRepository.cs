using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class NotificationRepository : INotificationRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;
    
    public NotificationRepository(ApplicationDbContext context , IUnitOfWork unitOfWork)
    {
        _context = context;
        _unitOfWork = unitOfWork;
        
    }
    public async Task AddNotificationAsync(Notification notification , CancellationToken cancellationToken)
    {
        await _context.Notifications.AddAsync(notification);
        await _unitOfWork.CommitAsync(cancellationToken);
    }

    

    public Task<List<Notification>> GetNotificationsByUserIdAsync(Guid userId)
    {
        return _context.Notifications.Where(x => x.UserId == userId).ToListAsync();
        
    }
}