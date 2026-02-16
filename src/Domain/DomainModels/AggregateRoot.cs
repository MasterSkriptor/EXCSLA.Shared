using System.Collections.Generic;

namespace EXCSLA.Shared.Domain;

/// <summary>
/// Base class for DDD Aggregates. This class bakes domain events into the base entity.
/// </summary>
public abstract class BaseAggregateRoot : BaseEntity
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
