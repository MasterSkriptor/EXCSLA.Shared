# EXCSLA.Shared.Application

CQRS Command and Query pattern implementation for the EXCSLA application layer.

## Overview

Provides abstractions and implementation for the Command Query Responsibility Segregation (CQRS) pattern without external dependencies.

## Key Interfaces

- **ICommand** - Marker interface for commands
- **ICommandHandler<TCommand>** - Handler for command execution
- **IQuery<TResult>** - Query with expected result type
- **IQueryHandler<TQuery, TResult>** - Handler for query execution
- **IDispatcher** - Command/Query dispatcher

## Installation

```bash
dotnet add package EXCSLA.Shared.Application
```

## Usage

### Define Commands

```csharp
public class CreateOrderCommand : ICommand
{
    public OrderNumber OrderNumber { get; set; }
    public CustomerId CustomerId { get; set; }
}

public class CreateOrderHandler : ICommandHandler<CreateOrderCommand>
{
    private readonly IAsyncRepository<Order> _repository;
    
    public async Task Handle(CreateOrderCommand command)
    {
        var order = new Order(command.OrderNumber, command.CustomerId);
        await _repository.AddAsync(order);
    }
}
```

### Define Queries

```csharp
public class GetOrderByIdQuery : IQuery<OrderDto>
{
    public OrderId Id { get; set; }
}

public class GetOrderByIdHandler : IQueryHandler<GetOrderByIdQuery, OrderDto>
{
    private readonly IAsyncRepository<Order> _repository;
    
    public async Task<OrderDto> Handle(GetOrderByIdQuery query)
    {
        var order = await _repository.GetByIdAsync(query.Id);
        return MapToDto(order);
    }
}
```

### Register in DI

```csharp
services.AddApplicationServices();
```

### Execute

```csharp
var command = new CreateOrderCommand { OrderNumber = orderNumber };
await dispatcher.DispatchAsync(command);

var query = new GetOrderByIdQuery { Id = orderId };
var result = await dispatcher.DispatchAsync(query);
```

## Features

- Zero external dependencies (no MediatR required)
- Type-safe command/query dispatch
- Async/await patterns
- IServiceProvider integration
- Decorator pattern support

## Building Blocks

- Commands - Write operations
- Queries - Read operations
- Handlers - Business logic
- Dispatcher - Orchestration

## Dependencies

- .NET 10.0 or higher

## License

See LICENSE file in repository

## Support

For issues and questions, visit the [GitHub repository](https://github.com/MasterSkriptor/EXCSLA.Shared)
