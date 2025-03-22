using Domain.Entities;
using Domain.Enums;
using FluentAssertions;
using TaskManagement.Tests.Mocks;

namespace TaskManagement.Tests.Domain;

public class TaskItemTests
{
     [Fact]
    public void Create_ShouldInitializeTaskWithCorrectValues()
    {
        // Arrange
        var title = "Test Task";
        var description = "This is a test task.";

        // Act
        var task = TaskItem.Create(title, description);

        // Assert
        task.Title.Should().Be(title);
        task.Description.Should().Be(description);
        task.Status.Should().Be(TaskStatusEnum.Pending);
        task.AssignedUserId.Should().BeNull();
    }

    [Fact]
    public void AssignTo_ShouldSetAssignedUser_WhenUserIsNotAssigned()
    {
        // Arrange
        var task = TaskItem.Create("Task", "Description");
        var user = User.Create("JohnDoe", "john@example.com", "SecurePass", new MockPasswordHasher());

        // Act
        task.AssignTo(user);

        // Assert
        task.AssignedUserId.Should().Be(user.Id);
        task.AssignedUser.Should().Be(user);
    }

    [Fact]
    public void AssignTo_ShouldThrowException_WhenTaskIsAlreadyAssigned()
    {
        // Arrange
        var task = TaskItem.Create("Task", "Description");
        var user1 = User.Create("JohnDoe", "john@example.com", "SecurePass", new MockPasswordHasher());
        var user2 = User.Create("JaneDoe", "jane@example.com", "SecurePass", new MockPasswordHasher());

        task.AssignTo(user1);

        // Act
        Action act = () => task.AssignTo(user2);

        // Assert
        act.Should().Throw<InvalidOperationException>().WithMessage("Task is already assigned!");
    }

    [Fact]
    public void MarkAsCompleted_ShouldChangeStatusToCompleted()
    {
        // Arrange
        var task = TaskItem.Create("Task", "Description");

        // Act
        task.MarkAsCompleted();

        // Assert
        task.Status.Should().Be(TaskStatusEnum.Completed);
    }

    [Fact]
    public void MarkAsCompleted_ShouldThrowException_WhenTaskIsAlreadyCompleted()
    {
        // Arrange
        var task = TaskItem.Create("Task", "Description");
        task.MarkAsCompleted();

        // Act
        Action act = () => task.MarkAsCompleted();

        // Assert
        act.Should().Throw<InvalidOperationException>().WithMessage("Task is already completed!");
    }
}