namespace ApiGateway.Exceptions;

public class UnauthorizedGatewayException : Exception
{
    public UnauthorizedGatewayException(string message) : base(message) { }
}