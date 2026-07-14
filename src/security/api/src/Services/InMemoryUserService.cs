using ApiSecurity.Models.Entities;
using ApiSecurity.Models.Requests;
using Security.Constants;

namespace ApiSecurity.Services;

public sealed class INMemoryUserService : IUserService
{
    private readonly List<User> _users;

    public INMemoryUserService()
    {
        _users =
        [
            new User(
                Guid.NewGuid(),
                "admin",
                BCrypt.Net.BCrypt.HashPassword("SuperPassword123"),
                Roles.Admin),
            new User(
                Guid.NewGuid(),
                "user",
                BCrypt.Net.BCrypt.HashPassword("UserPassword123"),
                Roles.EmployeeRead),
        ];
    }

    public User? Authenticate(string username, string password)
    {
        return _users.FirstOrDefault(u =>
            u.Username == username &&
            u.VerifyPassword(password));
    }

    public User Create(CreateUserRequest request)
    {
        if (_users.Any(u =>
            u.Username.Equals(
                request.Username,
                StringComparison.OrdinalIgnoreCase)))
        {
            throw new InvalidOperationException(
                "Username already exists.");
        }

        var user = new User(
            Guid.NewGuid(),
            request.Username,
            BCrypt.Net.BCrypt.HashPassword(request.Password),
            request.Role);

        _users.Add(user);

        return user;
    }

    public IReadOnlyCollection<User> GetAll()
    {
        return _users.AsReadOnly();
    }
}

