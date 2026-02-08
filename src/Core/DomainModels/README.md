# EXCSLA.Shared.Core

Core Domain-Driven Design abstractions and domain model classes for EXCSLA.

## Overview

This metapackage provides the complete core layer with domain model classes including AggregateRoot, Entity, ValueObject, and BaseDomainEvent.

## Key Classes

- **AggregateRoot** - Base class for domain aggregates
- **Entity** - Base class for domain entities  
- **ValueObject** - Base class for value objects
- **BaseDomainEvent** - Base class for domain events

## Installation

```bash
dotnet add package EXCSLA.Shared.Core
```

## Usage

### Create an Aggregate Root with Integer ID (Default)

```csharp
public class Order : AggregateRoot
{
    public OrderNumber OrderNumber { get; private set; }
    public OrderStatus Status { get; private set; }
    
    public Order(OrderNumber orderNumber)
    {
        OrderNumber = orderNumber;
        Status = OrderStatus.Pending;
    }
}
```

### Create an Aggregate Root with GUID ID

```csharp
public class Product : AggregateRoot<Guid>
{
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    
    public Product(Guid id, string name, decimal price)
    {
        Id = id;
        Name = name;
        Price = price;
    }
}
```

### Create an Entity with String ID

```csharp
public class Category : BaseEntity<string>
{
    public string Name { get; private set; }
    
    public Category(string id, string name)
    {
        Id = id;
        Name = name;
    }
}
```

### Generic Identity Support

The framework supports generic identity types for entities and aggregates:

- **BaseEntity<TId>** - Generic base entity with custom ID type
- **AggregateRoot<TId>** - Generic aggregate root with custom ID type
- **BaseEntity** - Convenience class for integer IDs (backward compatible)
- **AggregateRoot** - Convenience class for integer IDs (backward compatible)

Supported identity types include:
- `int` (default, backward compatible)
- `Guid` (for distributed systems)
- `string` (for custom identifiers)
- Any other comparable type

### Repository with Generic Identity

```csharp
public interface IProductRepository : IAsyncRepository<Product, Guid>
{
    // Custom repository methods
}
```

### Create a Value Object

```csharp
public class Money : ValueObject
{
    public decimal Amount { get; }
    public Currency Currency { get; }
    
    public Money(decimal amount, Currency currency)
    {
        Amount = amount;
        Currency = currency;
    }
}
```

## Dependencies

- .NET 10.0 or higher
- EXCSLA.Shared.Core.Abstractions
- EXCSLA.Shared.Core.DomainModels
- EXCSLA.Shared.Core.Exceptions
- EXCSLA.Shared.Core.GuardClauses
- EXCSLA.Shared.Core.Specifications
- EXCSLA.Shared.Core.ValueObjects

## License

See LICENSE file in repository

## Support

For issues and questions, visit the [GitHub repository](https://github.com/MasterSkriptor/EXCSLA.Shared)
