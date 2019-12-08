using EXCSLA.Shared.Core;
using EXCSLA.Shared.Core.Interfaces;
using System.Threading.Tasks;

namespace EXCSLA.Shared.Infrastructure.Services
{
    public partial class DomainEventDispatcher
    {
        private class DomainEventHandler<T> : DomainEventHandler
            where T : BaseDomainEvent
        {
            private readonly IHandle<T> _handler;

            public DomainEventHandler(IHandle<T> handler)
            {
                _handler = handler;
            }

            public override Task HandleAsync(BaseDomainEvent domainEvent)
            {
                return _handler.HandleAsync((T)domainEvent);
            }

            public override void Handle(BaseDomainEvent domainEvent)
            {
                _handler.Handle((T)domainEvent);
            }
        }
    }
}
