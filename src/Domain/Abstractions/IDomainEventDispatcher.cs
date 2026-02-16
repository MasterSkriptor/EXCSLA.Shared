using EXCSLA.Shared.Domain;
using System.Threading.Tasks;

namespace EXCSLA.Shared.Domain.Interfaces;

public interface IDomainEventDispatcher
{
    void Dispatch(BaseDomainEvent domainEvent);
    Task DispatchAsync(BaseDomainEvent domainEvent);
}