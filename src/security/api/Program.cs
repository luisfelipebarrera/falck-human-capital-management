using DotNetEnv;
using Security.Extensions;

var builder = WebApplication.CreateBuilder(args);

Env.Load();

builder.Configuration.AddEnvironmentVariables();

builder.Services.AddControllers();

builder.Services.AddJwtAuthentication(builder.Configuration);

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();