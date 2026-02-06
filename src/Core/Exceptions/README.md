# EXCSLA.Shared.Core.Exceptions

Custom domain exception classes for the EXCSLA DDD framework.

## Overview

Provides a hierarchy of domain-specific exceptions used throughout the EXCSLA framework for clear error handling and reporting.

## Exception Types

- **DomainException** - Base exception for domain logic violations
- **InvalidSpecificationException** - Raised when specification criteria are invalid
- **RepositoryException** - Raised for repository operation failures
- **InvalidEntityException** - Raised for invalid entity state

## Installation

```bash
dotnet add package EXCSLA.Shared.Core.Exceptions
```

## Usage

```csharp
public class Order : AggregateRoot
{
    public void Cancel()
    {
        if (Status == OrderStatus.Completed)
        {
            throw new DomainException("Cannot cancel completed order");
        }
        Status = OrderStatus.Cancelled;
    }
}
```

## Dependencies

- .NET 10.0 or higher

## License

See LICENSE file in repository

## Support

For issues and questions, visit the [GitHub repository](https://github.com/MasterSkriptor/EXCSLA.Shared)
