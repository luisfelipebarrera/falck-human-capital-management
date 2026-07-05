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
