# Copilot AI Agent Instructions for EXCSLA.Shared

## Project Overview
- **EXCSLA.Shared** is a modular .NET framework for building DDD (Domain-Driven Design) applications, targeting NuGet distribution for use in modern web APIs and sites.
- The architecture follows Clean Architecture principles: clear separation between Domain, Application, and Infrastructure layers.

## Key Architectural Patterns
- **Domain Layer** (`src/Domain/`): Contains aggregates, entities, value objects, domain events, guard clauses, specifications, and custom exceptions. No dependencies on other layers.
- **Application Layer** (`src/Application/`): Implements CQRS (Command/Query Responsibility Segregation) with `ICommand`, `IQuery`, `ICommandHandler`, `IQueryHandler`, and `IDispatcher` abstractions. Application logic is orchestrated here, referencing domain models.
- **Infrastructure Layer** (`src/Infrastructure/`): Provides implementations for external services (e.g., Azure Blob Storage, SendGrid). Use dependency injection for all infrastructure services.
- **Value Objects** are immutable. When updating, create a new instance rather than mutating properties. For EF Core, use `OwnsOne` to map value objects.

## Developer Workflows
- **Build**: Use `dotnet build` or the provided VS Code task (`build`) to compile the solution or test projects.
- **Test**: Run tests with `dotnet test` or the VS Code task (`watch` for continuous test running).
- **Run Example**: Navigate to `examples/SimpleOrderApi/` and run `dotnet run` to see a working API example.
- **NuGet Packages**: Each major layer can be published as a separate NuGet package.

## Project Conventions
- **Naming**: Use `EXCSLA.Shared.[Layer].[Component]` for namespaces and folders.
- **Events**: Domain events inherit from `BaseDomainEvent` and are dispatched via the event dispatcher.
- **CQRS**: Commands/queries are handled by their respective handlers; use the dispatcher for orchestration.
- **Immutability**: Value objects are always immutable.
- **Separation**: Never reference infrastructure from domain or application layers.

## Integration Points
- **Azure Blob Storage**: Use the abstractions and DI setup in `src/Infrastructure/AzureBlobService/README.md`.
- **SendGrid**: Email service integration is provided in `src/Infrastructure/SendGridEmailService/`.

## Examples
- See `examples/SimpleOrderApi/README.md` for a full-stack example of DDD, CQRS, and Clean Architecture in action.
- Domain modeling: `src/Domain/README.md`
- Application orchestration: `src/Application/README.md`

## Additional Notes
- Always prefer async/await patterns for I/O and service calls.
- Follow the modular structure for new features: Domain → Application → Infrastructure.
- For new value objects, ensure immutability and proper ORM mapping.

---
For more details, see the main `README.md` and layer-specific `README.md` files.
