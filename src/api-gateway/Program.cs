using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("Staging"))
{
    app.MapGet("/apis/open-api.yml", async (HttpContext context) =>
    {
        // 1. Al usar <Link>, .NET creará la carpeta apis dentro del bin automáticamente
        var filePath = Path.Combine(AppContext.BaseDirectory, "apis", "open-api.yml");

        // 2. Si estás ejecutando en modo debug local y no se ha copiado, 
        // buscamos yendo dos niveles hacia atrás desde el directorio de trabajo actual
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

        string scalarTitle = builder.Configuration["Scalar:Title"] ?? "API Documentation";
        options.WithTitle(scalarTitle);
    });
}

app.MapGet("/", () => "API Gateway is running...");
app.Run();