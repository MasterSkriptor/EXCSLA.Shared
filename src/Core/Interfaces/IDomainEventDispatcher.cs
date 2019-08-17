using EXCSLA.Shared.Core;

namespace EXCSLA.Shared.Core.Interfaces
{
    public interface IDomainEventDispatcher
    {
         void Dispatch(BaseDomainEvent domainEvent);
    }
}