using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using ApiSecurity.Models.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Security.Options;
using Security.Validation;

namespace ApiSecurity.Security;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly JwtOptions _options;

    public JwtTokenGenerator(IOptions<JwtOptions> options)
    {
        _options = options.Value;
    }

    public string Generate(User user)
    {
        JwtOptionsValidator.ValidatePrivateKey(_options);

        var rsa = CreatePrivateKey();

        var descriptor = new SecurityTokenDescriptor
        {
            Subject = CreateClaims(user),
            Expires = DateTime.UtcNow.AddHours(_options.ExpirationHours),
            Issuer = _options.Issuer,
            Audience = _options.Audience,
            SigningCredentials = new SigningCredentials(
                new RsaSecurityKey(rsa),
                SecurityAlgorithms.RsaSha256)
        };

        var handler = new JwtSecurityTokenHandler();

        var token = handler.CreateToken(descriptor);

        return handler.WriteToken(token);
    }

    private RSA CreatePrivateKey()
    {
        var rsa = RSA.Create();

        var privateKeyBytes =
            Convert.FromBase64String(_options.PrivateKey);

        try
        {
            rsa.ImportPkcs8PrivateKey(
                privateKeyBytes,
                out _);
        }
        catch (System.Formats.Asn1.AsnContentException)
        {
            rsa.ImportRSAPrivateKey(
                privateKeyBytes,
                out _);
        }

        return rsa;
    }

    private static ClaimsIdentity CreateClaims(User user)
    {
        return new ClaimsIdentity(
        [
            new Claim(
                ClaimTypes.NameIdentifier,
                user.Id.ToString()),

            new Claim(
                ClaimTypes.Name,
                user.Username),

            new Claim(
                ClaimTypes.Role,
                user.Role.ToString())
        ]);
    }
}