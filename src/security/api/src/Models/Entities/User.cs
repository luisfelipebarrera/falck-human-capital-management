using Security.Constants;

namespace ApiSecurity.Models.Entities;

public class User
{
    public Guid Id { get; init; }

    public string Username { get; init; } = string.Empty;

    public string PasswordHash { get; private set; } = string.Empty;

    public IReadOnlyCollection<string> Roles { get; init; } = [];

    public User(Guid id, string username, string passwordHash, IEnumerable<string> roles)
    {
        Id = id;
        Username = username;
        PasswordHash = passwordHash;
        Roles = roles.ToList().AsReadOnly();
    }

    public bool VerifyPassword(string password)
    {
        return BCrypt.Net.BCrypt.Verify(password, PasswordHash);
    }
}