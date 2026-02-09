using System.Collections.Generic;

namespace EXCSLA.Shared.Core;

/// <summary>
/// Base class for DDD Entity objects. Handles equality based off of Id field.
/// Inherit from this class for any domain entity that needs to be tracked by an Id.
/// </summary>
/// <typeparam name="TId">The type of the entity's identifier (e.g., int, Guid, string).</typeparam>
public abstract class BaseEntity<TId>
{
    public virtual TId Id { get; set; } = default!;

    public override bool Equals(object? obj)
    {
        var other = obj as BaseEntity<TId>;

        if (ReferenceEquals(other, null))
            return false;

        if (ReferenceEquals(this, other))
            return true;

        if (ReferenceEquals(other, default) || ReferenceEquals(this, default))
            return false;

        return EqualityComparer<TId>.Default.Equals(Id, other.Id);
    }

    public static bool operator ==(BaseEntity<TId>? a, BaseEntity<TId>? b)
    {
        if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
            return true;

        if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
            return false;

        return a.Equals(b);
    }

    public static bool operator !=(BaseEntity<TId>? a, BaseEntity<TId>? b)
    {
        return !(a == b);
    }

    public override int GetHashCode()
    {
        return EqualityComparer<TId>.Default.GetHashCode(Id);
    }
}

/// <summary>
/// Base class for DDD Entity objects with integer identity.
/// This is a convenience class for backward compatibility.
/// </summary>
public abstract class BaseEntity : BaseEntity<int>
{
}