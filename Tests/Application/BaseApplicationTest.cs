using EXCSLA.Shared.Application;
using EXCSLA.Shared.Application.Tests.Fixtures;
using Microsoft.Extensions.DependencyInjection;

namespace EXCSLA.Shared.Application.Tests;

/// <summary>
/// Base class for application layer tests providing common setup and utilities.
/// Provides dependency injection container and dispatcher configuration for testing.
/// </summary>
public abstract class BaseApplicationTest : IDisposable
{
    protected readonly ServiceProvider ServiceProvider;
    protected readonly IServiceCollection Services;
    protected readonly Dispatcher Dispatcher;

    protected BaseApplicationTest()
    {
        Services = new ServiceCollection();
        ConfigureServices(Services);
        ServiceProvider = (ServiceProvider)Services.BuildServiceProvider();
        Dispatcher = new Dispatcher(ServiceProvider);
    }

    /// <summary>
    /// Override this method to configure services for your specific test.
    /// </summary>
    protected virtual void ConfigureServices(IServiceCollection services)
    {
        // Default configuration - add dispatcher and test handlers
        services.AddDispatcher();
        services.AddTransient<ICommandHandler<TestCommand>, TestCommandHandler>();
        services.AddTransient<IQueryHandler<TestQuery, TestQueryResult>, TestQueryHandler>();
    }

    /// <summary>
    /// Gets a service instance from the dependency injection container.
    /// </summary>
    protected T GetService<T>() where T : notnull
    {
        return ServiceProvider.GetRequiredService<T>();
    }

    /// <summary>
    /// Attempts to get a service instance, returning null if not registered.
    /// </summary>
    protected T? TryGetService<T>() where T : class
    {
        return ServiceProvider.GetService<T>();
    }

    public virtual void Dispose()
    {
        ServiceProvider?.Dispose();
        GC.SuppressFinalize(this);
    }
}
