# Simple Order API Example

This example demonstrates how to use the EXCSLA.Shared v5.0 framework to build a simple order management API using DDD principles and Clean Architecture.

## Structure

```
SimpleOrderApi/
├── Domain/           # Domain layer with aggregates, entities, value objects
├── Application/      # Application layer with commands, queries, handlers
└── Program.cs        # API setup and configuration
```

## What This Example Demonstrates

1. **Domain Layer**:
   - Creating aggregates that inherit from `AggregateRoot`
   - Using domain events with `BaseDomainEvent`
   - Implementing value objects
   - Using guard clauses for validation

2. **Application Layer**:
   - Defining commands with `ICommand<T>`
   - Defining queries with `IQuery<T>`
   - Implementing command handlers with `ICommandHandler<T, TResult>`
   - Implementing query handlers with `IQueryHandler<T, TResult>`
   - Using the `IDispatcher` to send commands and queries

3. **Clean Architecture**:
   - Proper separation of concerns
   - Dependency flow from outer to inner layers
   - Domain-centric design

## Domain Model

### Order Aggregate
- Properties: OrderNumber, CustomerName, TotalAmount, Status
- Domain Events: OrderCreated, OrderCompleted
- Business Rules: Order must have positive total, can only complete once

## API Endpoints

- `POST /api/orders` - Create a new order
- `GET /api/orders/{id}` - Get order by ID
- `GET /api/orders` - List all orders
- `PUT /api/orders/{id}/complete` - Mark order as completed

## Running the Example

```bash
cd examples/SimpleOrderApi
dotnet run
```

Then visit: https://localhost:5001/swagger

## Key Takeaways

This example shows how the EXCSLA.Shared framework provides:
- ✅ Clean separation between domain and application logic
- ✅ Simple command/query pattern without MediatR
- ✅ Built-in domain event support
- ✅ Guard clauses for validation
- ✅ Type-safe, testable code structure

The framework handles the plumbing so you can focus on your business logic!
