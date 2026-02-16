using EXCSLA.Shared.Domain;
using System.Threading.Tasks;

namespace EXCSLA.Shared.Domain.Interfaces;

/// <summary>
/// Interface for domain event handlers.
/// Implement this interface to create handlers for specific domain events.
/// </summary>
/// <typeparam name="T">The type of domain event this handler processes.</typeparam>
/// <remarks>
/// Domain event handlers are used to react to domain events that occur within aggregates.
/// Typically used for side effects like sending notifications, triggering workflows, or updating other aggregates.
/// 
/// Example:06879+
/// <code>
/// public class OrderPlacedEventHandler : IHandle&lt;OrderPlacedEvent&gt;
/// {
///     public void Handle(OrderPlacedEvent domainEvent)
///     {
///         // Synchronous handling
///     }
///     
///     public async Task HandleAsync(OrderPlacedEvent domainEvent)
///     {
///         // Async handling (e.g., send email)
///     }
/// }
/// </code>
/// </remarks>
public interface IHandle<T> where T : BaseDomainEvent
{
    /// <summary>
    /// Handles the domain event synchronously.
    /// </summary>
    /// <param name="domainEvent">The domain event to handle.</param>
    void Handle(T domainEvent);

    /// <summary>
    /// Handles the domain event asynchronously.
    /// </summary>
    /// <param name="domainEvent">The domain event to handle.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task HandleAsync(T domainEvent);
}