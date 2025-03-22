using Domain.Shared;
using Helpers.Interfaces;

namespace Domain.Entities;

public class User : BaseEntity
{
    public string Username { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }

    private readonly List<TaskItem> _tasks = new();
    public IReadOnlyCollection<TaskItem> Tasks => _tasks.AsReadOnly();

    private readonly List<Notification> _notifications = new();
    public IReadOnlyCollection<Notification> Notifications => _notifications.AsReadOnly();

    private User()
    {
        // Required by EF
    } 

    private User(string username, string email, string password)
    {
        Username = username;
        Email = email;
        PasswordHash = password;
    }

    public static User Create(string username, string email, string password, IPasswordHasher passwordHasher)
    {
        if (string.IsNullOrWhiteSpace(username))
            throw new ArgumentException("UserName cannot be empty.", nameof(username));

        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be empty.", nameof(email));

        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentException("Password cannot be empty.", nameof(password));

        if (passwordHasher == null)
            throw new ArgumentNullException(nameof(passwordHasher));
        
        return new User(username, email, passwordHasher.HashPassword(password));
    }
    
    public bool VerifyPassword(string password, IPasswordHasher passwordHasher)
    {
        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentException("Password cannot be empty.", nameof(password));

        return passwordHasher.VerifyPassword(PasswordHash, password);
    }
}