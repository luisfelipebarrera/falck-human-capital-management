using ApiSecurity.Models.Entities;

namespace ApiSecurity.Security;

public interface IJwtTokenGenerator
{
    string Generate(User user);
}