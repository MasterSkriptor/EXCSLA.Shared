using EXCSLA.Shared.Core;

namespace EXCSLA.Shared.Tests.Core.UnitTests.BaseTestObjects
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