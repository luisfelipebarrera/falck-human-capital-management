## falck-human-capital-management

Solution for management human capital in a Falck Group.

This solution uses several architectures, which are described in the document [architecture.md](docs/architecture.md). However, we primarily use hexagonal architecture, clean code, API-first design, and Domain-Driven Design (DDD). All of this is to provide greater robustness and scalability to the product. The development process is fully documented in [development.md](docs/development.md).

Repository stucture

**falck-human-capital-management**

```code

docs/
    api-first.md
    architecture.md
    development.md
    references.md

apis/
    open-api.yml

src/
    api-core
        Api
        Application
        Domain
            Aggregates
            Entities
            ValueObjects
            DomainServices

        Infrastructure
            Persistence
                Configurations
                Repositories
                Migrations
                Seed

tests/

docker/

README.md

```

## Microservices

### Open API

[Open API Document](http://localhost:5101/scalar/) - Open or Download Open API definition.

**/api-security**

To run the project, duplicate the **.env.example** file, rename it to **.env**, and dotnet run.

```bash

# This is only an example, you should use a valid authorizer
# Run OpenSSL to generate an RSA key pair.
openssl genrsa -out falck_private.key 4096
openssl rsa -in falck_private.key -pubout -out falck_public.key
# Export base64
grep -v '^-' falck_private.key | tr -d '\r\n '
grep -v '^-' falck_public.key | tr -d '\r\n '
```

**.env**

```bash
Jwt__Issuer=https://falk-poc-api.com
Jwt__Audience=https://falk-poc.com
Jwt__PublicKey=MIICIjANBgkqhkiG...
Jwt__PrivateKey=MIIJQwIBADANBgk...
```

### Dependency chain

Dependency chain to reach from the controller to the Employee aggregate

```bash
Controller --> Application --> Factory --> Strategy --> Employee
```

### Running environment

Generate controller from Open API (API First)

```bash
nswag openapi2cscontroller /input:"apis/open-api.yml" /output:"src/gateway/api/src/Controllers/ApiEmployeeHcm.cs" /className:"{controller}" /namespace:"ApiGateway.Controllers" /controllerBaseClass:"Microsoft.AspNetCore.Mvc.ControllerBase" /jsonLibrary:"SystemTextJson" /operationGenerationMode:"MultipleClientsFromFirstTagAndOperationId"
```

```bash
dotnet clean src/gateway/api/api.csproj
dotnet build src/gateway/api/api.csproj
dotnet run --project src/gateway/api/api.csproj --launch-profile Development

dotnet clean src/security/api/api.csproj
dotnet build src/security/api/api.csproj
dotnet run --project src/security/api/api.csproj --launch-profile Development
```

Users

| Username | Password         | Role         |
| :------- | :--------------- | :----------- |
| admin    | SuperPassword123 | Admin        |
| user     | UserPassword123  | EmployeeRead |
