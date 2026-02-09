# EXCSLA.Shared.Core.Abstractions

Core abstractions and interfaces for the EXCSLA Domain-Driven Design framework.

## Overview

This package provides the foundational abstractions for building applications using Domain-Driven Design patterns. It includes repository patterns, specifications, email services, and domain event handling interfaces.

## Key Interfaces

- **IAsyncRepository<T, TId>** - Generic async repository pattern with custom ID type support
- **IAsyncRepository<T>** - Convenience interface for integer ID repositories
- **ISpecification<T>** - Specification pattern for complex query criteria
- **IEmailSender** - Email service abstraction
- **IHandle<T>** - Domain event handler interface

## Installation

```bash
dotnet add package EXCSLA.Shared.Core.Abstractions
```

## Usage

### Repository Pattern with Integer ID (Default)

```csharp
public interface IOrderRepository : IAsyncRepository<Order>
{
    // Custom repository methods
}

// Implementation
public class OrderRepository : IOrderRepository
{
    public async Task<Order> GetByIdAsync(int id)
    {
        // Implementation
    }
    
    // Other methods...
}
```

### Repository Pattern with GUID ID

```csharp
public interface IProductRepository : IAsyncRepository<Product, Guid>
{
    // Custom repository methods
}

// Implementation
public class ProductRepository : IProductRepository
{
    public async Task<Product> GetByIdAsync(Guid id)
    {
        // Implementation
    }
    
    // Other methods...
}
```

### Repository Pattern with String ID

```csharp
public interface ICategoryRepository : IAsyncRepository<Category, string>
{
    // Custom repository methods
}

// Implementation
public class CategoryRepository : ICategoryRepository
{
    public async Task<Category> GetByIdAsync(string id)
    {
        // Implementation
    }
    
    // Other methods...
}
```

### Generic Repository Interface

```csharp
public interface IAsyncRepository<T, TId> where T : BaseEntity<TId>
{
    Task<T> GetByIdAsync(TId id);
    Task<List<T>> ListAllAsync();
    Task<List<T>> ListAsync(ISpecification<T> spec);
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task<int> CountAsync(ISpecification<T> spec);
}
```

### Email Service

```csharp
public interface IEmailSender
{
    Task SendEmailAsync(string email, string subject, string message);
}
```

## Generic Identity Support

The framework now supports generic identity types for repositories:

- **IAsyncRepository<T, TId>** - Base generic interface with custom ID type
- **IAsyncRepository<T>** - Convenience interface for integer IDs (backward compatible)

This allows repositories to work with any ID type:
- `int` (default, backward compatible)
- `Guid` (for distributed systems)
- `string` (for custom identifiers)
- Any other type appropriate for use as an identifier

## Dependencies

- .NET 10.0 or higher

## License

See LICENSE file in repository

## Support

For issues and questions, visit the [GitHub repository](https://github.com/MasterSkriptor/EXCSLA.Shared)
