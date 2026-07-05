# Implementing solution

## Commands to generate the archetype.

```bash

# 1. Createte solution
dotnet new sln -n solution

# 2. Create class library
dotnet new classlib -o src/core/domain
dotnet new classlib -o src/core/application
dotnet new classlib -o src/core/infrastructure

rm -r -force src/core/domain/obj
rm -r -force src/core/application/obj
rm -r -force src/core/infrastructure/obj

rm src/core/domain/Class1.cs
rm src/core/application/Class1.cs
rm src/core/infrastructure/Class1.cs

# 3. Create Web APIs
dotnet new webapi -o src/core/api
dotnet new webapi -o src/security/api
dotnet new webapi -o src/gateway/api

# 3.1. Create shared
dotnet new classlib -o src/security/security
rm -r -force src/security/security/obj
rm src/security/security/Class1.cs
dotnet sln add src/security/security/security.csproj
dotnet add src/security/api/api.csproj reference src/security/security/security.csproj
dotnet add src/security/security package Microsoft.AspNetCore.Authentication.JwtBearer
dotnet add src/security/security package Microsoft.Extensions.Options.ConfigurationExtensions

# 4. Join components to the solution.
dotnet add src/core/application/application.csproj reference src/core/domain/domain.csproj
dotnet add src/core/infrastructure/infrastructure.csproj reference src/core/application/application.csproj
dotnet add src/core/api/api.csproj reference src/core/application/application.csproj src/core/infrastructure/infrastructure.csproj

# 5. Add projects to the soluciont
dotnet sln add src/core/api/api.csproj
dotnet sln add src/gateway/api/api.csproj
dotnet sln add src/security/api/api.csproj
# The other projects are added by transitivity
# dotnet sln add src/domain/domain.csproj
# dotnet sln add src/application/application.csproj
# dotnet sln add src/infrastructure/infrastructure.csproj

```

## Configure API Gateway

```bash

# APIs documentation with scalar
dotnet add package Scalar.AspNetCore --project src/gateway/api/api.csproj
```

Running environment

```bash
dotnet clean src/gateway/api/api.csproj
dotnet build src/gateway/api/api.csproj
dotnet run --project src/gateway/api/api.csproj --launch-profile Development

dotnet clean src/security/api/api.csproj
dotnet build src/security/api/api.csproj
dotnet run --project src/security/api/api.csproj --launch-profile Development
```

## Install dependencies

```bash
dotnet add package DotNetEnv --project src/security/api/api.csproj
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer --project src/security/api/api.csproj
dotnet add package BCrypt.Net-Next --project src/security/api/api.csproj
```
