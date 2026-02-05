using EXCSLA.Shared.Core;
using System.Threading.Tasks;

namespace EXCSLA.Shared.Core.Interfaces;

public interface IHandle<T> where T : BaseDomainEvent
{
    void Handle(T domainEvent);
    Task HandleAsync(T domainEvent);
}