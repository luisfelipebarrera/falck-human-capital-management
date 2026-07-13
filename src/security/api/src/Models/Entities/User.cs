using Security.Constants;

namespace ApiSecurity.Models.Entities;

public class User
{
    public Guid Id { get; init; }

    public string Username { get; init; } = string.Empty;

    public string PasswordHash { get; private set; } = string.Empty;

    public string Role { get; init; } = Roles.EmployeeRead;

    public User(Guid id, string username, string passwordHash, string role)
    {
        Id = id;
        Username = username;
        PasswordHash = passwordHash;
        Role = role;
    }

    public bool VerifyPassword(string password)
    {
        return BCrypt.Net.BCrypt.Verify(password, PasswordHash);
    }
}