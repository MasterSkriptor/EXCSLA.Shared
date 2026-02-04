# EXCSLA.Shared v5.0 - DDD Framework for .NET 10

A comprehensive Domain-Driven Design (DDD) framework for building modern .NET websites and APIs using Clean Architecture principles.

## 🚀 Version 5.0 MVP Release

Version 5.0 is a major release focusing on modernization and simplicity:

- **✅ .NET 10 Support**: All Core and Application projects upgraded to .NET 10
- **✅ Clean Architecture**: New Application layer with built-in Command/Query pattern
- **✅ Zero External Dependencies**: Simple internal dispatcher replacing MediatR
- **✅ Project References**: All Core projects now use project references instead of NuGet packages for better maintainability
- **✅ Modern C#**: Nullable reference types and implicit usings enabled

## 📦 NuGet Packages (MVP)

### Core Domain Layer
- **EXCSLA.Shared.Core** (DomainModels) - Base classes for Aggregates, Entities, Value Objects
- **EXCSLA.Shared.Core.Abstractions** - Core interfaces and contracts  
- **EXCSLA.Shared.Core.Specifications** - Specification pattern implementation
- **EXCSLA.Shared.Core.Exceptions** - Custom domain exceptions
- **EXCSLA.Shared.Core.GuardClauses** - Input validation extensions
- **EXCSLA.Shared.Core.ValueObjects.Common** - Common value objects (Email, Phone, Address, etc.)
- **EXCSLA.Shared.Core.Abstractions.AlertService** - Alert service interfaces
- **EXCSLA.Shared.Core.Abstractions.DomainEventDispatcher** - Domain event interfaces

### Application Layer
- **EXCSLA.Shared.Application** - Command/Query/Handler patterns with simple internal dispatcher

## 🏗️ Architecture

The framework follows Clean Architecture principles with clear separation of concerns:

```
┌─────────────────────────────────────┐
│          UI Layer (TBD)             │
│  Blazor Components, Web APIs        │
└─────────────────────────────────────┘
              ↓
┌─────────────────────────────────────┐
│      Application Layer ✅            │
│  Commands, Queries, Handlers        │
│  Simple Internal Dispatcher         │
└─────────────────────────────────────┘
              ↓
┌─────────────────────────────────────┐
│       Domain Layer ✅                │
│  Aggregates, Entities, Value Objs   │
│  Domain Events, Specifications      │
└─────────────────────────────────────┘
              ↓
┌─────────────────────────────────────┐
│    Infrastructure Layer (TBD)       │
│  EF Core, Azure, SendGrid, etc.     │
└─────────────────────────────────────┘
```

## 🎯 Quick Start

### 1. Install Core Packages

```bash
dotnet add package EXCSLA.Shared.Core --version 5.0.0
dotnet add package EXCSLA.Shared.Core.Abstractions --version 5.0.0
dotnet add package EXCSLA.Shared.Application --version 5.0.0
```

### 2. Create a Domain Entity

```csharp
using EXCSLA.Shared.Core;

namespace MyApp.Domain;

public class Order : AggregateRoot
{
    public string OrderNumber { get; private set; }
    public decimal TotalAmount { get; private set; }
    
    private Order() { } // EF Core
    
    public Order(string orderNumber, decimal totalAmount)
    {
        OrderNumber = Guard.Against.NullOrEmpty(orderNumber);
        TotalAmount = Guard.Against.NegativeOrZero(totalAmount);
        
        AddDomainEvent(new OrderCreatedEvent(this));
    }
}

public class OrderCreatedEvent : BaseDomainEvent
{
    public Order Order { get; }
    
    public OrderCreatedEvent(Order order)
    {
        Order = order;
    }
}
```

### 3. Create Commands and Handlers

```csharp
using EXCSLA.Shared.Application;

namespace MyApp.Application;

// Command
public record CreateOrderCommand(string OrderNumber, decimal Amount) : ICommand<int>;

// Handler
public class CreateOrderCommandHandler : ICommandHandler<CreateOrderCommand, int>
{
    private readonly IAsyncRepository<Order> _orderRepository;
    
    public CreateOrderCommandHandler(IAsyncRepository<Order> orderRepository)
    {
        _orderRepository = orderRepository;
    }
    
    public async Task<int> HandleAsync(CreateOrderCommand command, CancellationToken cancellationToken = default)
    {
        var order = new Order(command.OrderNumber, command.Amount);
        var savedOrder = await _orderRepository.AddAsync(order);
        return savedOrder.Id;
    }
}
```

### 4. Use Queries

```csharp
// Query
public record GetOrderQuery(int OrderId) : IQuery<OrderDto>;

// Handler
public class GetOrderQueryHandler : IQueryHandler<GetOrderQuery, OrderDto>
{
    private readonly IAsyncRepository<Order> _orderRepository;
    
    public GetOrderQueryHandler(IAsyncRepository<Order> orderRepository)
    {
        _orderRepository = orderRepository;
    }
    
    public async Task<OrderDto> HandleAsync(GetOrderQuery query, CancellationToken cancellationToken = default)
    {
        var order = await _orderRepository.GetByIdAsync(query.OrderId);
        return new OrderDto(order.Id, order.OrderNumber, order.TotalAmount);
    }
}
```

### 5. Register Services

```csharp
using EXCSLA.Shared.Application;

var builder = WebApplication.CreateBuilder(args);

// Register the dispatcher
builder.Services.AddDispatcher();

// Register your handlers
builder.Services.AddScoped<ICommandHandler<CreateOrderCommand, int>, CreateOrderCommandHandler>();
builder.Services.AddScoped<IQueryHandler<GetOrderQuery, OrderDto>, GetOrderQueryHandler>();

var app = builder.Build();
```

### 6. Use the Dispatcher

```csharp
using EXCSLA.Shared.Application;

public class OrdersController : ControllerBase
{
    private readonly IDispatcher _dispatcher;
    
    public OrdersController(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
    {
        var command = new CreateOrderCommand(request.OrderNumber, request.Amount);
        var orderId = await _dispatcher.SendAsync<CreateOrderCommand, int>(command);
        return Ok(new { OrderId = orderId });
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrder(int id)
    {
        var query = new GetOrderQuery(id);
        var order = await _dispatcher.QueryAsync<GetOrderQuery, OrderDto>(query);
        return Ok(order);
    }
}
```

## 🔧 Key Features

### Domain Layer
- **Base Classes**: `AggregateRoot`, `BaseEntity`, `ValueObject`
- **Domain Events**: Built-in domain event support with `BaseDomainEvent`
- **Specifications**: Query specification pattern implementation
- **Guard Clauses**: Extension methods for input validation
- **Value Objects**: Pre-built common value objects (Email, Phone, Address, etc.)

### Application Layer
- **Command Pattern**: `ICommand`, `ICommandHandler` for write operations
- **Query Pattern**: `IQuery`, `IQueryHandler` for read operations  
- **Simple Dispatcher**: Built-in `IDispatcher` using `IServiceProvider` - no MediatR needed
- **Dependency Injection**: Easy registration with `AddDispatcher()` extension method

## 📚 Value Objects

The framework includes common value objects:

```csharp
using EXCSLA.Shared.Core.ValueObjects.Common;

var email = new Email("user@example.com");
var phone = new PhoneNumber("555-1234");
var name = new FullName("John", "Doe");
var address = new Address("123 Main St", "Anytown", "CA", "12345");
```

## 🔄 Migration from v4.x to v5.0

See [MIGRATION.md](./MIGRATION.md) for detailed migration instructions.

## 📅 Roadmap

### Completed ✅
- Core Domain Layer (.NET 10)
- Application Layer with Command/Query pattern
- Simple internal dispatcher
- Simplified architecture (removed EF Core, old event dispatcher)

### Coming Soon 🚧
- Infrastructure projects (.NET 10) - **Simplified**
  - Cloud integrations (Azure, SendGrid)
  - Alert services
  - **Note**: EF Core repositories and old event dispatcher have been removed. The Application layer Dispatcher now handles all dispatching needs. Repository implementations can be added by consumers as needed.
- UI Components (.NET 10)
  - Blazor components
- Enhanced testing and documentation

## 🤝 Contributing

This is an open-source project. Pull requests are welcome! Please see our GitHub repository for contribution guidelines.

## 📄 License

LGPL-3.0-or-later

## 🏢 About

Developed by **Executive Computer Systems, LLC** (EXCSLA)  
A Louisiana-based IT firm specializing in modern web applications.

Repository: https://github.com/MasterSkriptor/EXCSLA.Shared
