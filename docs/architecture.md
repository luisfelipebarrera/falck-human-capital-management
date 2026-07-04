# Architecture Pattern

## 1. Clean Architecture Overview (Hexagonal)

Hexagonal Architecture, also known as Ports and Adapters, aims to create loosely coupled application components. It makes the system independent of databases, frameworks, UI, and any external technology or device.

### Core Layers

1. Domain LayerThe heart of the application. It represents the business logic and state. It is pure code and has absolutely no dependencies on other layers.Entities: Rich domain objects that encapsulate state and behavior.Value Objects: Immutable objects that describe characteristics or attributes.Domain Services: Business logic that doesn't fit within a single entity.Repository Interfaces: Interfaces defining the operations (save, get, delete) the domain requires to persist data.

2. Application LayerThe use-case layer. It orchestrates the flow of data to and from the domain, translating user intents into domain operations. It depends on the Domain layer but knows nothing about the outside world (UI or Database).

- Use Cases (Interactors): Application-specific business rules (e.g., "Register User", "Process Payment").
- Input Ports: Interfaces defining what the application can do (used by primary adapters).
- Output Ports: Interfaces defining what the application needs from external services (implemented by secondary adapters).

3. Infrastructure LayerThe technical details. This layer handles interactions with the outside world, such as databases, APIs, message queues, and UI frameworks. It depends on both the Domain and Application layers.

- Adapters: Implementations of the ports defined in the Application layer.
  - Driving (Primary/Input) Adapters: Trigger application use cases (e.g., REST API Controllers, CLI commands, Web UI).
  - Driven (Secondary/Output) Adapters: Fulfill technical needs defined by output ports (e.g., Database Repositories, Third-party API clients, Email services).

### The Flow of Dependencies

Dependencies always point inward toward the Domain layer:

The Flow of DependenciesDependencies always point inward toward the Domain layer:

[ Infrastructure ] ---> [ Application ] ---> [ Domain ]

- Infrastructure depends on Application and Domain.
- Application depends on Domain.
- Domain depends on nothing.

### Benefits

- Testability: Domain logic can be easily unit-tested without mocking a database or server.
- Flexibility: You can swap frameworks or databases (e.g., changing from MySQL to PostgreSQL) by simply creating a new Infrastructure adapter.
- Maintainability: Clear separation of concerns makes the codebase easier to understand and evolve.
