using System.Collections.Generic;

namespace EXCSLA.Shared.Core
{
    public abstract class BaseEntity<TId>
    {
        public TId Id {get; set;} 

        public override bool Equals(object obj)
        {
            var other = obj as BaseEntity<TId>;

            if (ReferenceEquals(other, null))
                return false;
            
            if (ReferenceEquals(this, other))
                return true;

            if(ReferenceEquals(other, default(TId)) || ReferenceEquals(this, default(TId)))
                return false;

            return EqualityComparer<TId>.Default.Equals(Id, other.Id);
        }

        public static bool operator ==(BaseEntity<TId> a, BaseEntity<TId> b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(BaseEntity<TId> a, BaseEntity<TId> b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}