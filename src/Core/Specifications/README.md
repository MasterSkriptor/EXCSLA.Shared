# EXCSLA.Shared.Core.Specifications

Specification pattern implementation for query criteria composition in the EXCSLA framework.

## Overview

Implements the Specification pattern for building complex, reusable query criteria without polluting domain models.

## Features

- Type-safe query specifications
- Composable query criteria
- Include related entities support
- Pagination, sorting, and filtering
- LINQ predicate-based queries

## Installation

```bash
dotnet add package EXCSLA.Shared.Core.Specifications
```

## Usage

```csharp
public class UndeliveredOrdersSpec : Specification<Order>
{
    public UndeliveredOrdersSpec(DateTime since)
    {
        Query
            .Where(o => o.Status == OrderStatus.Pending && 
                        o.CreatedAt >= since)
            .Include(o => o.Customer)
            .Include(o => o.Items)
            .OrderBy(o => o.CreatedAt);
    }
}

// Usage in repository
var spec = new UndeliveredOrdersSpec(DateTime.UtcNow.AddDays(-7));
var orders = await repository.ListAsync(spec);
```

## Specification Base Class

- Defines query criteria (Where, Include, OrderBy, Take, Skip)
- Supports pagination and sorting
- Composable with other specifications
- Works seamlessly with IAsyncRepository<T>

## Dependencies

- .NET 10.0 or higher

## License

See LICENSE file in repository

## Support

For issues and questions, visit the [GitHub repository](https://github.com/MasterSkriptor/EXCSLA.Shared)
