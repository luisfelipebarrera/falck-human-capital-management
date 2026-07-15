using ApiGateway.Exceptions;

namespace ApiGateway.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (UnauthorizedGatewayException ex)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = "application/json";

            var errorResponse = new { error = ex.Message };
            await context.Response.WriteAsJsonAsync(errorResponse);
        }
        // For Productive environments, you might want to log the exception and return a generic error message instead of exposing internal details.
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);

            context.Response.StatusCode = 500;

            await context.Response.WriteAsJsonAsync(new
            {
                message = ex.Message,
                exception = ex.GetType().Name,
                stackTrace = ex.StackTrace
            });
        }
        //catch (Exception ex)
        //{
        //    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        //    context.Response.ContentType = "application/json";

        //    var errorResponse = new { error = "An unexpected error occurred." };
        //    await context.Response.WriteAsJsonAsync(errorResponse);
        //}
    }
}