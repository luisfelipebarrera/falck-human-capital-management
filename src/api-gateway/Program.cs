using ApiGateway.Controllers;
using ApiGateway.Middleware;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

string scalarTitle = builder.Configuration["Scalar:Title"] ?? "API Documentation";
string apiSecurityUrl = builder.Configuration["Microservices:ApiSecurityUrl"] ?? "http://localhost:8081";

builder.Services.AddControllers();

builder.Services.AddHttpClient<IAuthenticationController, ApiGateway.Services.AuthenticationService>(client =>
{
    client.BaseAddress = new Uri(apiSecurityUrl);
});

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

// Enabled native driver routing because there are controllers injected by NSwag interfaces
app.MapControllers();

if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("Staging"))
{
    app.MapGet("/apis/open-api.yml", async (HttpContext context) =>
    {
        var filePath = Path.Combine(AppContext.BaseDirectory, "apis", "open-api.yml");

        if (!File.Exists(filePath))
        {
            filePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "apis", "open-api.yml");
        }

        if (!File.Exists(filePath))
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            context.Response.ContentType = "text/plain; charset=utf-8";
            await context.Response.WriteAsync($"Error: The open-api.yml file was not found in the external root of the repository.");
            return;
        }

        context.Response.ContentType = "application/x-yaml";
        await context.Response.WriteAsync(await File.ReadAllTextAsync(filePath));
    });

    app.MapScalarApiReference(options =>
    {
        options.WithOpenApiRoutePattern("/apis/open-api.yml");
        options.OpenApiRoutePattern = "/apis/open-api.yml";
        options.WithTitle(scalarTitle);
    });
}

app.MapGet("/", () => "API Gateway is running...");
app.Run();