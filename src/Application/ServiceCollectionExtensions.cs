using Microsoft.Extensions.DependencyInjection;

namespace EXCSLA.Shared.Application;

/// <summary>
/// Extension methods for registering Application layer services with the DI container.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers the Dispatcher as a singleton service.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The service collection for chaining.</returns>
    public static IServiceCollection AddDispatcher(this IServiceCollection services)
    {
        services.AddSingleton<IDispatcher, Dispatcher>();
        return services;
    }
}
