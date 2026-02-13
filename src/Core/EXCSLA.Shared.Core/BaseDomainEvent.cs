using System;

namespace EXCSLA.Shared.Core;

/// <summary>
/// Base class for domain event. Adds DateOccured to the event for tracking when the event occured.
/// </summary>
public abstract class BaseDomainEvent
{
	public DateTime DateOccured {get; protected set; } = DateTime.UtcNow;
}
