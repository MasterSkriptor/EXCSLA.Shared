using System.Collections.Generic;

namespace EXCSLA.Shared.Core
{
    /// <summary>
    /// Base class for DDD Entity objects. Handles equality based off of Id field.
    /// Inherit from this class for any domain entity that needs to be tracked by an Id.
    /// </summary>
    public abstract class BaseEntity
    {
        public virtual int Id { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as BaseEntity;

            if (ReferenceEquals(other, null))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (ReferenceEquals(other, default) || ReferenceEquals(this, default))
                return false;

            return EqualityComparer<object>.Default.Equals(Id, other.Id);
        }

        public static bool operator ==(BaseEntity a, BaseEntity b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(BaseEntity a, BaseEntity b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}