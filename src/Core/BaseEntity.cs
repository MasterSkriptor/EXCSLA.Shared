using System.Collections.Generic;

namespace EXCSLA.Shared.Core
{
    public abstract class BaseEntity<TId> : BaseEntity
    {
        public new TId Id
        {
            get
            {
                return (TId)base.Id;
            }

            set
            {
                base.Id = value;
            }
        }
    }

    public abstract class BaseEntity
    {
        public object Id { get; set; }

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