namespace Security.Options;

public sealed class JwtOptions
{
    public const string SectionName = "Jwt";

    public string Issuer { get; init; } = string.Empty;

    public string Audience { get; init; } = string.Empty;

    public string PublicKey { get; init; } = string.Empty;

    public string PrivateKey { get; init; } = string.Empty;

    public int ExpirationHours { get; init; } = 2;
}