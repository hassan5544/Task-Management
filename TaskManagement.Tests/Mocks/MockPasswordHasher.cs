using Helpers.Interfaces;

namespace TaskManagement.Tests.Mocks;

public class MockPasswordHasher : IPasswordHasher
{
    public string HashPassword(string password) => $"HASHED_{password}";

    public bool VerifyPassword(string hashedPassword, string providedPassword)
    {
        return hashedPassword == $"HASHED_{providedPassword}";
    }
}