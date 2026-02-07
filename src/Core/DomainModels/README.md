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

### Create an Aggregate Root

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
