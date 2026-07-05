using ApiSecurity.Models.Entities;
using ApiSecurity.Models.Requests;

namespace ApiSecurity.Services;

public interface IUserService
{
    User Authenticate(string username, string password);

    User Create(CreateUserRequest request);

    IReadOnlyCollection<User> GetAll();
}