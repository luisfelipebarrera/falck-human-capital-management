# Implementing solution

Commands to generate the archetype.

```bash

# 1. Createte solution
dotnet new sln -n solution

# 2. Create class library
dotnet new classlib -o src/domain
dotnet new classlib -o src/application
dotnet new classlib -o src/infrastructure

rm -r -force src/domain/obj
rm -r -force src/application/obj
rm -r -force src/infrastructure/obj

rm src/domain/Class1.cs
rm src/application/Class1.cs
rm src/infrastructure/Class1.cs

# 3. Create Web APIs
dotnet new webapi -o src/api-core
dotnet new webapi -o src/api-security
dotnet new webapi -o src/api-gateway

# 4. Join components to the solution.
dotnet add src/application/application.csproj reference src/domain/domain.csproj
dotnet add src/infrastructure/infrastructure.csproj reference src/application/application.csproj
dotnet add src/api-core/api-core.csproj reference src/application/application.csproj src/infrastructure/infrastructure.csproj

# 5. Add projects to the soluciont
dotnet sln add src/api-core/api-core.csproj
# The other projects are added by transitivity
# dotnet sln add src/domain/domain.csproj
# dotnet sln add src/application/application.csproj
# dotnet sln add src/infrastructure/infrastructure.csproj

```
