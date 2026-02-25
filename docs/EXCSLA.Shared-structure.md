# EXCSLA.Shared Project Structure and NuGet Package Usage


## Overview
EXCSLA.Shared is a modular .NET framework for building DDD (Domain-Driven Design) applications, following Clean Architecture principles. It provides abstractions and implementations for CQRS (Command/Query Responsibility Segregation), domain modeling, and infrastructure integrations.

### NuGet Packages (v5.4.0)
EXCSLA.Shared is distributed as a set of NuGet packages. To use the framework, add the required packages as `PackageReference` to your project(s):

- **EXCSLA.Shared.Domain**: Domain layer (aggregates, value objects, events, guard clauses, exceptions)
- **EXCSLA.Shared.Application**: Application layer (CQRS abstractions, dispatcher, handlers)
- **EXCSLA.Shared.Infrastructure.SendGridService**: SendGrid email service implementation
- **EXCSLA.Shared.Infrastructure.AzureBlobService**: Azure Blob Storage service implementation
- **EXCSLA.Shared.Infrastructure.AzureBlobService.Abstractions**: Azure Blob Storage service abstractions (interfaces only)

> All packages must be referenced at version **5.4.0** for compatibility.

**Example:**
```xml
<PackageReference Include="EXCSLA.Shared.Domain" Version="5.4.0" />
<PackageReference Include="EXCSLA.Shared.Application" Version="5.4.0" />
<PackageReference Include="EXCSLA.Shared.Infrastructure.SendGridService" Version="5.4.0" />
<PackageReference Include="EXCSLA.Shared.Infrastructure.AzureBlobService" Version="5.4.0" />
<PackageReference Include="EXCSLA.Shared.Infrastructure.AzureBlobService.Abstractions" Version="5.4.0" />
```

Add only the packages needed for each project. For example, a web API may reference Domain, Application, and Infrastructure packages, while a test project may only need Domain and Application.

---

## Top-Level Structure

- **EXCSLA.Shared.sln**: Solution file
- **src/**: Main source code
- **Tests/**: Unit and integration tests
- **examples/**: Example applications (e.g., SimpleOrderApi)
- **docs/**: Documentation, guides, migration plans
- **nupkg/**: NuGet package outputs

---

## Source Code Layout (`src/`)

### Application Layer (`src/Application/`)
- Implements CQRS abstractions:
  - `ICommand`, `ICommandHandler<T>`, `IQuery<TResult>`, `IQueryHandler<TQuery, TResult>`, `IDispatcher`
- `Dispatcher.cs`: Lightweight dispatcher using DI (no MediatR dependency)
- `ServiceCollectionExtensions.cs`: DI registration helpers
- README with usage examples

### Domain Layer (`src/Domain/`)
- Aggregates, Entities, Value Objects
- Domain Events, Guard Clauses, Specifications
- Custom Exceptions
- Modular, testable, and extensible
- README with DDD modeling guidance

### Infrastructure Layer (`src/Infrastructure/`)
- External service integrations (e.g., SendGrid, Azure Blob Storage)
- Domain event dispatching
- Entity Framework Core support
- Each service in its own subfolder with README where applicable

### UI Layer (`src/UI/`)
- Blazor UI components and helpers (optional, modular)
- Client-side utilities (e.g., AlertService, DataTable, Modal)

---

## Example Application (`examples/SimpleOrderApi/`)
- Demonstrates DDD and CQRS usage
- Contains Domain and Application layers, plus API setup
- README with walkthrough

---

## Testing (`Tests/`)
- Mirrors `src/` structure for Application, Domain, Infrastructure
- Includes value object and core logic tests

---

## Documentation (`docs/`)
- Guides, migration plans, best practices, release notes
- Reference documentation for each layer

---

## Key Architectural Patterns
- Clean separation: Domain → Application → Infrastructure → UI
- CQRS: Commands/Queries with handlers, dispatched via `IDispatcher`
- Domain events and value objects are first-class
- Async/await throughout
- No external dependencies for CQRS (no MediatR)

---

## References
- See `src/Application/README.md` and `src/Domain/README.md` for more details
- Example usage: `examples/SimpleOrderApi/README.md`
- GitHub: https://github.com/MasterSkriptor/EXCSLA.Shared
