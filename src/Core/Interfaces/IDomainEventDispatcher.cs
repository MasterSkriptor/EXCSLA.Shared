using EXCSLA.Shared.Core;

namespace Core.Interfaces
{
    public interface IDomainEventDispatcher
    {
         void Dispatch(BaseDomainEvent domainEvent);
    }
}