using Autofac;
using EXCSLA.Shared.Core;
using EXCSLA.Shared.Core.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EXCSLA.Shared.Infrastructure.Services
{
    // https://gist.github.com/jbogard/54d6569e883f63afebc7
    // http://lostechies.com/jimmybogard/2014/05/13/a-better-domain-events-pattern/
    public partial class DomainEventDispatcher : IDomainEventDispatcher
    {
        private readonly IComponentContext _container;

        public DomainEventDispatcher(IComponentContext container)
        {
            _container = container;
        }

        public async Task DispatchAsync(BaseDomainEvent domainEvent)
        {
            Type handlerType = typeof(IHandle<>).MakeGenericType(domainEvent.GetType());
            Type wrapperType = typeof(DomainEventHandler<>).MakeGenericType(domainEvent.GetType());
            IEnumerable handlers = (IEnumerable)_container.Resolve(typeof(IEnumerable<>).MakeGenericType(handlerType));
            IEnumerable<DomainEventHandler> wrappedHandlers = handlers.Cast<object>()
                .Select(handler => (DomainEventHandler)Activator.CreateInstance(wrapperType, handler));

            foreach (DomainEventHandler handler in wrappedHandlers)
            {
                await handler.HandleAsync(domainEvent);
            }
        }

        public void Dispatch(BaseDomainEvent domainEvent)
        {
            DispatchAsync(domainEvent).GetAwaiter().GetResult();
        }
    }
}
