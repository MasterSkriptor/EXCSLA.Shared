using System;

namespace EXCSLA.Shared.Core
{
    public abstract class BaseDomainEvent
    {
        public DateTime DateOccured {get; protected set; } = DateTime.UtcNow;
    }
}