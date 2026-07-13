
using System.Collections.Concurrent;
using ApiSecurity.Models.Entities;
using Security.Constants;

namespace ApiSecurity.Services;

public class INMemoryUserService
{
    private readonly ConcurrentDictionary<string, User> _users;

    public INMemoryUserService()
    {
        _users = new ConcurrentDictionary<string, User>();

        _users.TryAdd(
            "admin",
            new User(
                Guid.NewGuid(),
                "admin",
                BCrypt.Net.BCrypt.HashPassword("SuperPassword123"),
                Roles.Admin));

        _users.TryAdd(
            "user",
            new User(
                Guid.NewGuid(),
                "user",
                BCrypt.Net.BCrypt.HashPassword("UserPassword123"),
                Roles.EmployeeRead));
    }
}

