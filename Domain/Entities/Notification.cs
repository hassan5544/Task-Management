using Domain.Shared;

namespace Domain.Entities;

public class Notification : BaseEntity
{
    public string Message { get; private set; }
    public bool IsRead { get; private set; }
    public Guid UserId { get; private set; }
    public User User { get; private set; }

    private Notification()
    {
        // Required by EF
    }
    private Notification(string message, User user)
    {
        Message = message;
        User = user;
        UserId = user.Id;
        IsRead = false;
    }
    
    public static Notification Create(string message, User user)
    {
        return new Notification(message, user);
    }

}