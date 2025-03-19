namespace Application.DTOs;

public class TaskDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Status { get; set; }
    public string CreatedBy { get; set; }
    public string AssignedTo { get; set; }
    public DateTime CreatedAt { get; set; }
}