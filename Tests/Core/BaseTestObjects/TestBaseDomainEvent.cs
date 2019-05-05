using EXCSLA.Shared.Core;

namespace Core.BaseTestObjects
{
    public class TestBaseDomainEvent : BaseDomainEvent
    {
        public TestAggregateRoot UpdatedTestAggregateRoot {get; private set;}

        public TestBaseDomainEvent(TestAggregateRoot testAggregateRoot)
        {
            UpdatedTestAggregateRoot = testAggregateRoot;
        }
        
    }
}