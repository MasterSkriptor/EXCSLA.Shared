# EXCSLA.Shared.Core.Abstractions.DomainEventDispatcher

Domain event dispatcher abstraction for publishing domain events in the EXCSLA framework.

## Overview

Provides an abstraction for dispatching domain events to registered handlers throughout your application.

## Key Interface

- **IDomainEventDispatcher** - Event dispatcher for domain events

## Installation

```bash
dotnet add package EXCSLA.Shared.Core.Abstractions.DomainEventDispatcher
```

## Usage

```csharp
public class OrderCreatedEvent : BaseDomainEvent
{
    public OrderNumber OrderNumber { get; set; }
    public DateTime CreatedAt { get; set; }
}

// Usage
public class CreateOrderHandler : ICommandHandler<CreateOrderCommand>
{
    private readonly IDomainEventDispatcher _dispatcher;
    
    public async Task Handle(CreateOrderCommand command)
    {
        var order = new Order(command.OrderNumber);
        
        // Dispatch event to handlers
        await _dispatcher.DispatchAsync(
            new OrderCreatedEvent 
            { 
                OrderNumber = order.OrderNumber,
                CreatedAt = DateTime.UtcNow
            });
    }
}
```

## Features

- Type-safe event dispatching
- Support for multiple handlers per event
- Async/await patterns
- Easy registration in DI container

## Dependencies

- .NET 10.0 or higher

## License

See LICENSE file in repository

## Support

For issues and questions, visit the [GitHub repository](https://github.com/MasterSkriptor/EXCSLA.Shared)
