# EXCSLA.Shared.Core.ValueObjects

Common value object implementations for the EXCSLA DDD framework.

## Overview

Provides reusable, immutable value objects with built-in validation for common domain concepts.

## Included Value Objects

- **Email** - Email address with RFC validation
- **FullName** - Person name with formatting
- **Address** - Postal address with components
- **FileName** - File name with extension validation

## Installation

```bash
dotnet add package EXCSLA.Shared.Core.ValueObjects
```

## Usage

```csharp
public class Customer : AggregateRoot
{
    public Email Email { get; private set; }
    public FullName Name { get; private set; }
    public Address Address { get; private set; }
    
    public Customer(Email email, FullName name, Address address)
    {
        Email = email;
        Name = name;
        Address = address;
    }
}

// Creating instances
var email = new Email("user@example.com");
var name = new FullName("John", "Doe");
var address = new Address("123 Main St", "Springfield", "IL", "62701", "USA");

var customer = new Customer(email, name, address);
```

## Features

- Immutable design
- Value equality comparison
- Built-in validation
- Easy serialization
- Type safety

## Dependencies

- .NET 10.0 or higher
- Ardalis.GuardClauses

## License

See LICENSE file in repository

## Support

For issues and questions, visit the [GitHub repository](https://github.com/MasterSkriptor/EXCSLA.Shared)
