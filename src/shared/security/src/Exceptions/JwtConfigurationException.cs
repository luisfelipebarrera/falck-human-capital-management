namespace Security.Exceptions;

public sealed class JwtConfigurationException : Exception
{
    public JwtConfigurationException(string message)
        : base(message)
    {
    }
}