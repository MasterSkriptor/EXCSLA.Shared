using EXCSLA.Shared.Core;

namespace EXCSLA.Shared.Core.Interfaces
{
    public interface IHandle<T> where T : BaseDomainEvent
    {
         void Handle(T domainEvent);
    }
}