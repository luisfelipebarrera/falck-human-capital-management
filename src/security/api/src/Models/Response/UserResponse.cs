namespace Contracts.Responses;

public class UserResponse
{
    public Guid Id { get; init; }

    public string Username { get; init; } = string.Empty;

    public IReadOnlyCollection<string> Roles { get; init; } = [];
}