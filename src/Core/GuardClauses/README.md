# EXCSLA.Shared.Core.GuardClauses

Guard clause extensions for input validation in the EXCSLA framework.

## Overview

Provides fluent extension methods for guard clauses based on Ardalis.GuardClauses for clean, readable input validation.

## Features

- Fluent API for input validation
- Clear error messages
- Prevents invalid state at entry points
- Based on Ardalis.GuardClauses design patterns

## Installation

```bash
dotnet add package EXCSLA.Shared.Core.GuardClauses
```

## Usage

```csharp
using EXCSLA.Shared.Core.GuardClauses;

public class Order : AggregateRoot
{
    public Order(OrderNumber orderNumber, Customer customer)
    {
        Guard.Against.Null(orderNumber, nameof(orderNumber));
        Guard.Against.Null(customer, nameof(customer));
        
        OrderNumber = orderNumber;
        Customer = customer;
    }
}
```

## Common Guard Methods

- **Guard.Against.Null()** - Prevents null references
- **Guard.Against.NullOrEmpty()** - Prevents null or empty strings
- **Guard.Against.NullOrWhiteSpace()** - Prevents null, empty, or whitespace strings
- **Guard.Against.InvalidInput()** - Validates against predicates
- **Guard.Against.OutOfRange()** - Ensures values are within range

## Dependencies

- .NET 10.0 or higher
- Ardalis.GuardClauses

## License

See LICENSE file in repository

## Support

For issues and questions, visit the [GitHub repository](https://github.com/MasterSkriptor/EXCSLA.Shared)
