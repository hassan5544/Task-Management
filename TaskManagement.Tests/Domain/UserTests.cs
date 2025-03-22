using Domain.Entities;
using FluentAssertions;
using TaskManagement.Tests.Mocks;

namespace TaskManagement.Tests.Domain;

public class UserTests
{
    [Fact]
    public void Create_ShouldInitializeUserWithCorrectValues()
    {
        // Arrange
        var username = "JohnDoe";
        var email = "john@example.com";
        var password = "SecurePass";
        var hasher = new MockPasswordHasher();

        // Act
        var user = User.Create(username, email, password, hasher);

        // Assert
        user.Username.Should().Be(username);
        user.Email.Should().Be(email);
        user.VerifyPassword(password, hasher).Should().BeTrue();
    }

    [Theory]
    [InlineData("", "email@example.com", "password")]
    [InlineData("Username", "", "password")]
    [InlineData("Username", "email@example.com", "")]
    public void Create_ShouldThrowArgumentException_WhenInvalidDataProvided(string username, string email, string password)
    {
        // Arrange
        var hasher = new MockPasswordHasher();

        // Act
        Action act = () => User.Create(username, email, password, hasher);

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void VerifyPassword_ShouldReturnTrue_WhenPasswordMatches()
    {
        // Arrange
        var hasher = new MockPasswordHasher();
        var user = User.Create("JohnDoe", "john@example.com", "SecurePass", hasher);

        // Act
        var result = user.VerifyPassword("SecurePass", hasher);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void VerifyPassword_ShouldReturnFalse_WhenPasswordDoesNotMatch()
    {
        // Arrange
        var hasher = new MockPasswordHasher();
        var user = User.Create("JohnDoe", "john@example.com", "SecurePass", hasher);

        // Act
        var result = user.VerifyPassword("WrongPass", hasher);

        // Assert
        result.Should().BeFalse();
    }
}