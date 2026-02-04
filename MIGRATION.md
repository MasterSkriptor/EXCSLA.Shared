# Migration Guide: EXCSLA.Shared v4.x to v5.0

This guide helps you migrate your projects from EXCSLA.Shared v4.x to v5.0.

## 🎯 Overview of Changes

Version 5.0 introduces significant improvements focused on modernization and MVP delivery:

### Breaking Changes
1. **Target Framework**: Upgraded from .NET 6.0 to .NET 10.0
2. **Dependencies**: Core projects now use project references instead of NuGet package references
3. **Ardalis.GuardClauses**: Updated from v3.3.0 to v5.0.0
4. **Nullable Reference Types**: Now enabled by default
5. **New Application Layer**: Added Command/Query/Handler patterns with internal dispatcher

### New Features
- ✅ **Application Layer**: New EXCSLA.Shared.Application package with CQRS support
- ✅ **Simple Dispatcher**: Built-in command/query dispatcher (no MediatR dependency)
- ✅ **Modern C#**: Implicit usings and nullable reference types enabled

## 📋 Migration Steps

### Step 1: Update Your .NET Version

Ensure you're using .NET 10 SDK:

```bash
dotnet --version
# Should show 10.x.x or higher
```

If not installed, download from: https://dotnet.microsoft.com/download/dotnet/10.0

### Step 2: Update Your Project Files

Update your project's `TargetFramework`:

```xml
<!-- Before (v4.x) -->
<TargetFramework>net6.0</TargetFramework>

<!-- After (v5.0) -->
<TargetFramework>net10.0</TargetFramework>
```

Optionally, enable modern C# features:

```xml
<PropertyGroup>
  <TargetFramework>net10.0</TargetFramework>
  <ImplicitUsings>enable</ImplicitUsings>
  <Nullable>enable</Nullable>
</PropertyGroup>
```

### Step 3: Update Package References

Update all EXCSLA.Shared packages to version 5.0.0:

```xml
<!-- Core packages -->
<PackageReference Include="EXCSLA.Shared.Core" Version="5.0.0" />
<PackageReference Include="EXCSLA.Shared.Core.Abstractions" Version="5.0.0" />
<PackageReference Include="EXCSLA.Shared.Core.Specifications" Version="5.0.0" />
<PackageReference Include="EXCSLA.Shared.Core.Exceptions" Version="5.0.0" />
<PackageReference Include="EXCSLA.Shared.Core.GuardClauses" Version="5.0.0" />
<PackageReference Include="EXCSLA.Shared.Core.ValueObjects.Common" Version="5.0.0" />

<!-- New Application layer -->
<PackageReference Include="EXCSLA.Shared.Application" Version="5.0.0" />
```

Then run:

```bash
dotnet restore
dotnet build
```

### Step 4: Address Nullable Reference Type Warnings

If you enabled nullable reference types, you may see new warnings. Address them:

```csharp
// Before (v4.x)
public class MyEntity : BaseEntity
{
    public string Name { get; set; }
}

// After (v5.0) - Option 1: Make nullable if null is valid
public class MyEntity : BaseEntity
{
    public string? Name { get; set; }
}

// After (v5.0) - Option 2: Initialize or use required
public class MyEntity : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    // OR
    public required string Name { get; set; }
}
```

### Step 5: Update Using Statements (Optional)

If you enabled `ImplicitUsings`, you can remove common using statements:

```csharp
// Before (v4.x) - explicit usings
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EXCSLA.Shared.Core;

namespace MyApp.Domain;

public class MyClass { }

// After (v5.0) - implicit usings enabled, file-scoped namespace
using EXCSLA.Shared.Core;

namespace MyApp.Domain;

public class MyClass { }
```

### Step 6: Adopt Application Layer (Optional but Recommended)

If you want to use the new Application layer with Command/Query pattern:

#### 6.1. Install the Application Package

```bash
dotnet add package EXCSLA.Shared.Application --version 5.0.0
```

#### 6.2. Create Commands and Handlers

```csharp
using EXCSLA.Shared.Application;

// Define your command
public record CreateProductCommand(string Name, decimal Price) : ICommand<int>;

// Implement the handler
public class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, int>
{
    private readonly IAsyncRepository<Product> _repository;
    
    public CreateProductCommandHandler(IAsyncRepository<Product> repository)
    {
        _repository = repository;
    }
    
    public async Task<int> HandleAsync(CreateProductCommand command, CancellationToken cancellationToken = default)
    {
        var product = new Product(command.Name, command.Price);
        var result = await _repository.AddAsync(product);
        return result.Id;
    }
}
```

#### 6.3. Register Services

```csharp
// In Program.cs or Startup.cs
builder.Services.AddDispatcher();
builder.Services.AddScoped<ICommandHandler<CreateProductCommand, int>, CreateProductCommandHandler>();
```

#### 6.4. Use the Dispatcher

```csharp
public class ProductService
{
    private readonly IDispatcher _dispatcher;
    
    public ProductService(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }
    
    public async Task<int> CreateProduct(string name, decimal price)
    {
        var command = new CreateProductCommand(name, price);
        return await _dispatcher.SendAsync<CreateProductCommand, int>(command);
    }
}
```

### Step 7: Update Ardalis.GuardClauses Usage

Version 5.0 uses Ardalis.GuardClauses v5.0.0. Most usage remains the same, but check for any API changes:

```csharp
// Usage remains largely the same
public Order(string orderNumber)
{
    OrderNumber = Guard.Against.NullOrEmpty(orderNumber, nameof(orderNumber));
}
```

### Step 8: Test Your Application

After migration, thoroughly test your application:

```bash
# Run your tests
dotnet test

# Run your application
dotnet run
```

## 🚨 Known Issues and Workarounds

### Issue 1: Serialization Warnings in Custom Exceptions

You may see warnings about obsolete serialization constructors:

```
warning SYSLIB0051: 'Exception.Exception(SerializationInfo, StreamingContext)' is obsolete
```

**Workaround**: These warnings are from the Core.Exceptions package and will be addressed in a future update. They don't affect functionality and can be safely suppressed:

```xml
<PropertyGroup>
  <NoWarn>$(NoWarn);SYSLIB0051</NoWarn>
</PropertyGroup>
```

### Issue 2: Infrastructure Projects Not Yet Updated

Infrastructure projects (EntityFrameworkCore, DomainEventDispatcher, etc.) are not yet upgraded to .NET 10 in this MVP release.

**Workaround**: Continue using v4.x infrastructure packages until they are upgraded, or implement your own infrastructure layer following the abstractions in Core.Abstractions.

## 📝 What's Not Changing

The following remain backward compatible:

- ✅ **Base Classes**: `AggregateRoot`, `BaseEntity`, `ValueObject` - no API changes
- ✅ **Domain Events**: `BaseDomainEvent` - no API changes
- ✅ **Specifications**: `ISpecification<T>`, `BaseSpecification<T>` - no API changes
- ✅ **Value Objects**: Email, PhoneNumber, FullName, Address - no API changes
- ✅ **Repository Pattern**: `IAsyncRepository<T>` - no API changes

## 🔍 Verification Checklist

After migration, verify:

- [ ] Project builds without errors
- [ ] All tests pass
- [ ] Application runs correctly
- [ ] No runtime exceptions related to framework changes
- [ ] NuGet packages are version 5.0.0
- [ ] Target framework is net10.0

## 📞 Need Help?

If you encounter issues during migration:

1. Check the [GitHub Issues](https://github.com/MasterSkriptor/EXCSLA.Shared/issues)
2. Review the [README_MVP.md](./README_MVP.md) for usage examples
3. Open a new issue if your problem isn't already addressed

## 🎉 Benefits After Migration

After completing the migration, you'll have:

- ✅ Latest .NET 10 features and performance improvements
- ✅ Modern C# language features (nullable reference types, implicit usings)
- ✅ Clean Architecture with Application layer support
- ✅ Simple command/query pattern without external dependencies
- ✅ Better long-term maintainability and support

## 📅 What's Next?

Future releases will include:

- Infrastructure layer upgrades (.NET 10)
- UI component upgrades (.NET 10)
- Enhanced documentation and examples
- Additional application layer features

Stay tuned for updates!
