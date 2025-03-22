using Domain.Entities;
using FluentAssertions;
using TaskManagement.Tests.Mocks;

namespace TaskManagement.Tests.Domain;

public class NotificationTests
{
    [Fact]
    public void Create_ShouldInitializeNotificationCorrectly()
    {
        // Arrange
        var user = User.Create("JohnDoe", "john@example.com", "SecurePass", new MockPasswordHasher());
        var message = "New notification";

        // Act
        var notification = Notification.Create(message, user);

        // Assert
        notification.Message.Should().Be(message);
        notification.User.Should().Be(user);
        notification.UserId.Should().Be(user.Id);
        notification.IsRead.Should().BeFalse();
    }
}