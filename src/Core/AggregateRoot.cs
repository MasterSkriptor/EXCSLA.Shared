using System.Collections.Generic;

namespace EXCSLA.Shared.Core
{
    public abstract class AggregateRoot<TId> : BaseEntity<TId>
    {
        private readonly List<BaseDomainEvent> _events = new List<BaseDomainEvent>();
        public virtual IReadOnlyList<BaseDomainEvent> Events => _events;

        protected virtual void AddDomainEvent(BaseDomainEvent newEvent)
        {
            _events.Add(newEvent);
        }

        public virtual void ClearEvents()
        {
            _events.Clear();
        }
    }

    public abstract class AggregateRoot : AggregateRoot<object>
    {
        
    }
}