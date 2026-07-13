namespace ApiSecurity.Models.Responses;

public class UserResponse
{
    public Guid Id { get; init; }

    public string Username { get; init; } = string.Empty;

    public string Role { get; init; } = string.Empty;
}