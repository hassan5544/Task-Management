namespace Domain.Shared;

public class BaseEntity
{
    public Guid Id { get; protected set; } = Guid.NewGuid();
    public DateTime InsertDate { get; protected set; } = DateTime.UtcNow;
    public DateTime UpdateDate { get; protected set; } = DateTime.UtcNow;
    public bool IsDeleted { get; protected set; } = false;
    public DateTimeOffset? DeletedAt { get; protected set; } = null;
}