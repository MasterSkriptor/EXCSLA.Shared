using EXCSLA.Shared.Core;

namespace EXCSLA.Shared.Tests.Core.UnitTests.BaseTestObjects
{
    public class TestBaseDomainEvent : BaseDomainEvent
    {
        public TestAggregateRoot UpdatedTestAggregateRoot {get; private set;}

        public TestBaseDomainEvent(TestAggregateRoot testAggregateRoot)
        {
            // Create a snapshot of the aggregate state at event creation time
            UpdatedTestAggregateRoot = new TestAggregateRoot(testAggregateRoot.Id, testAggregateRoot.FirstName, testAggregateRoot.LastName);
        }
        
    }

    public class TestStringDomainEvent : BaseDomainEvent
    {
        public TestStringAggregateRoot UpdatedTestAggregateRoot { get; private set; }

        public TestStringDomainEvent(TestStringAggregateRoot testAggregateRoot)
        {
            // Create a snapshot of the aggregate state at event creation time
            UpdatedTestAggregateRoot = new TestStringAggregateRoot(testAggregateRoot.Id, testAggregateRoot.FirstName, testAggregateRoot.LastName);
        }
    }
}