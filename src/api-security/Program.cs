using DotNetEnv;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// 1. Cargar archivo .env en las variables de entorno del sistema operativo
Env.Load();

// 2. Mapear las variables de entorno sobre la configuración en memoria
// Esto combina appsettings.json (MockUsers) con el .env (Jwt__)
builder.Configuration.AddEnvironmentVariables();

// 3. Configurar la autenticación JwtBearer utilizando la llave pública RSA
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var publicKeyBase64 = builder.Configuration["Jwt:PublicKey"];

        if (string.IsNullOrEmpty(publicKeyBase64))
        {
            throw new InvalidOperationException("La llave pública JWT no está configurada en el entorno.");
        }

        // Crear la instancia RSA e importar de forma segura
        var rsaPublicKey = RSA.Create();
        byte[] publicKeyBytes = Convert.FromBase64String(publicKeyBase64);

        rsaPublicKey.ImportSubjectPublicKeyInfo(publicKeyBytes, out _);

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new RsaSecurityKey(rsaPublicKey),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ValidateLifetime = true
        };
    });

// Agregar controladores al contenedor de dependencias
builder.Services.AddControllers();

var app = builder.Build();

// Habilitar el middleware de autenticación y autorización en el pipeline
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
