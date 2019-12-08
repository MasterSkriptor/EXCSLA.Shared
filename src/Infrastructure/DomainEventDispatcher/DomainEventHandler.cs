using EXCSLA.Shared.Core;
using System.Threading.Tasks;

namespace EXCSLA.Shared.Infrastructure.Services
{
    public partial class DomainEventDispatcher
    {
        private abstract class DomainEventHandler
        {
            public abstract void Handle(BaseDomainEvent domainEvent);
            public abstract Task HandleAsync(BaseDomainEvent domainEvent);
        }
    }
}
