using Security.Exceptions;
using Security.Options;

namespace Security.Validation;

public static class JwtOptionsValidator
{
    public static void ValidatePublicKey(JwtOptions options)
    {
        ValidateCommon(options);

        if (string.IsNullOrWhiteSpace(options.PublicKey))
        {
            throw new JwtConfigurationException("JWT public key is not configured.");
        }
    }

    public static void ValidatePrivateKey(JwtOptions options)
    {
        ValidateCommon(options);

        if (string.IsNullOrWhiteSpace(options.PrivateKey))
        {
            throw new JwtConfigurationException("JWT private key is not configured.");
        }
    }

    private static void ValidateCommon(JwtOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);

        if (string.IsNullOrWhiteSpace(options.Issuer))
        {
            throw new JwtConfigurationException("JWT issuer is not configured.");
        }

        if (string.IsNullOrWhiteSpace(options.Audience))
        {
            throw new JwtConfigurationException("JWT audience is not configured.");
        }

        if (options.ExpirationHours <= 0)
        {
            throw new JwtConfigurationException("JWT expiration time must be greater than zero.");
        }
    }
}