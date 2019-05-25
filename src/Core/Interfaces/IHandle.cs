using EXCSLA.Shared.Core;

namespace Core.Interfaces
{
    public interface IHandle<T> where T : BaseDomainEvent
    {
         void Handle(T domainEvent);
    }
}