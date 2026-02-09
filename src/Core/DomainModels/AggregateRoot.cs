using System.Collections.Generic;

namespace EXCSLA.Shared.Core;

/// <summary>
/// Base class for DDD Aggregates. This class bakes domain events into the base entity.
/// </summary>
/// <typeparam name="TId">The type of the entity's identifier (e.g., int, Guid, string).</typeparam>
public abstract class AggregateRoot<TId> : BaseEntity<TId>
{
    private readonly List<BaseDomainEvent> _events = new List<BaseDomainEvent>();
    public virtual IReadOnlyList<BaseDomainEvent> Events => _events;

    /// <summary>
    /// Add new domain event from inside inherited class.
    /// </summary>
    /// <param name="newEvent">The domain event to add to the list of events.</param>
    protected virtual void AddDomainEvent(BaseDomainEvent newEvent)
    {
        _events.Add(newEvent);
    }

    /// <summary>
    /// Clears all domain events from the aggregate.
    /// </summary>
    public virtual void ClearEvents()
    {
        _events.Clear();
    }
}

/// <summary>
/// Base class for DDD Aggregates with integer identity.
/// This is a convenience class for backward compatibility.
/// </summary>
public abstract class AggregateRoot : AggregateRoot<int>
{
}