namespace ApiSecurity.Models.Requests;

public class CreateUserRequest
{
    public string Username { get; init; } = string.Empty;

    public string Password { get; init; } = string.Empty;

    public required IReadOnlyCollection<string> Roles { get; init; }
}