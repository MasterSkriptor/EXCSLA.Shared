# EXCSLA.Shared.Core.Abstractions

Core abstractions and interfaces for the EXCSLA Domain-Driven Design framework.

## Overview

This package provides the foundational abstractions for building applications using Domain-Driven Design patterns. It includes repository patterns, specifications, email services, and domain event handling interfaces.

## Key Interfaces

- **IAsyncRepository<T>** - Generic async repository pattern for aggregate root persistence
- **ISpecification<T>** - Specification pattern for complex query criteria
- **IEmailSender** - Email service abstraction
- **IHandle<T>** - Domain event handler interface

## Installation

```bash
dotnet add package EXCSLA.Shared.Core.Abstractions
```

## Usage

### Repository Pattern

```csharp
public interface IAsyncRepository<T> where T : class
{
    Task<T?> GetByIdAsync(object id);
    Task<IEnumerable<T>> ListAsync();
    Task<IEnumerable<T>> ListAsync(ISpecification<T> spec);
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task<int> CountAsync(ISpecification<T> spec);
    Task<bool> AnyAsync(ISpecification<T> spec);
}
```

### Email Service

```csharp
public interface IEmailSender
{
    Task SendEmailAsync(string email, string subject, string message);
}
```

## Dependencies

- .NET 10.0 or higher

## License

See LICENSE file in repository

## Support

For issues and questions, visit the [GitHub repository](https://github.com/MasterSkriptor/EXCSLA.Shared)
