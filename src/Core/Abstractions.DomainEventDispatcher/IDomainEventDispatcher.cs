using EXCSLA.Shared.Core;
using System.Threading.Tasks;

namespace EXCSLA.Shared.Core.Interfaces;

public interface IDomainEventDispatcher
{
    void Dispatch(BaseDomainEvent domainEvent);
    Task DispatchAsync(BaseDomainEvent domainEvent);
}