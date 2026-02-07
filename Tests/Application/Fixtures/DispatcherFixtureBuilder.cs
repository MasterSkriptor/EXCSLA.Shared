using EXCSLA.Shared.Application;
using Microsoft.Extensions.DependencyInjection;

namespace EXCSLA.Shared.Application.Tests.Fixtures;

/// <summary>
/// Builder for configuring test scenarios with commands and queries.
/// Provides fluent API for setting up dispatcher test fixtures.
/// </summary>
public sealed class DispatcherFixtureBuilder
{
    private readonly IServiceCollection _services;

    public DispatcherFixtureBuilder()
    {
        _services = new ServiceCollection();
    }

    /// <summary>
    /// Configures the default application services.
    /// </summary>
    public DispatcherFixtureBuilder WithDefaultServices()
    {
        _services.AddDispatcher();
        return this;
    }

    /// <summary>
    /// Registers a custom command handler.
    /// </summary>
    public DispatcherFixtureBuilder WithCommandHandler<TCommand, THandler>()
        where TCommand : ICommand
        where THandler : class, ICommandHandler<TCommand>
    {
        _services.AddTransient<ICommandHandler<TCommand>, THandler>();
        return this;
    }

    /// <summary>
    /// Registers a custom query handler.
    /// </summary>
    public DispatcherFixtureBuilder WithQueryHandler<TQuery, TResult, THandler>()
        where TQuery : IQuery<TResult>
        where THandler : class, IQueryHandler<TQuery, TResult>
    {
        _services.AddTransient<IQueryHandler<TQuery, TResult>, THandler>();
        return this;
    }

    /// <summary>
    /// Adds a custom service to the container.
    /// </summary>
    public DispatcherFixtureBuilder WithService<TInterface, TImplementation>()
        where TInterface : class
        where TImplementation : class, TInterface
    {
        _services.AddTransient<TInterface, TImplementation>();
        return this;
    }

    /// <summary>
    /// Builds the dispatcher with configured services.
    /// </summary>
    public Dispatcher Build()
    {
        var serviceProvider = _services.BuildServiceProvider();
        return new Dispatcher(serviceProvider);
    }

    /// <summary>
    /// Builds and returns both the service provider and dispatcher.
    /// </summary>
    public (ServiceProvider ServiceProvider, Dispatcher Dispatcher) BuildWithProvider()
    {
        var serviceProvider = (ServiceProvider)_services.BuildServiceProvider();
        var dispatcher = new Dispatcher(serviceProvider);
        return (serviceProvider, dispatcher);
    }
}
