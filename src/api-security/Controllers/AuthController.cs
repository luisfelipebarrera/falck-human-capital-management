using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using ApiSecurity.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ApiSecurity.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public AuthController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        var mockUsers = _configuration.GetSection("MockUsers").Get<List<MockUser>>();

        if (mockUsers == null || !mockUsers.Any())
        {
            return StatusCode(500, "The list of simulated users is not configured.");
        }

        var user = mockUsers.FirstOrDefault(u => u.Username == request.Username && u.Password == request.Password);

        if (user == null)
        {
            return Unauthorized("Incorrect credentials.");
        }

        var privateKeyBase64 = _configuration["Jwt:PrivateKey"];

        if (string.IsNullOrEmpty(privateKeyBase64))
        {
            return StatusCode(500, "Internal server configuration error. No valid private key is configured.");
        }

        var rsaPrivateKey = RSA.Create();
        byte[] privateKeyBytes = Convert.FromBase64String(privateKeyBase64);

        try
        {
            rsaPrivateKey.ImportPkcs8PrivateKey(privateKeyBytes, out _);
        }
        catch (System.Formats.Asn1.AsnContentException)
        {
            rsaPrivateKey.ImportRSAPrivateKey(privateKeyBytes, out _);
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = new RsaSecurityKey(rsaPrivateKey);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
            [
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            ]),
            Expires = DateTime.UtcNow.AddHours(2),
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"],
            SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.RsaSha256)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);

        return Ok(new { Token = tokenString });
    }
}