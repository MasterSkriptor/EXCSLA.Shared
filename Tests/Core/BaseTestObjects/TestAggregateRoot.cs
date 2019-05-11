using EXCSLA.Shared.Core;

namespace EXCSLA.Shared.Tests.Core.UnitTests.BaseTestObjects
{
    public class TestAggregateRoot : AggregateRoot<int>
    {
        public string FirstName {get; private set;}
        public string LastName {get; private set;}

        public TestAggregateRoot(int id, string firstName, string lastName)
        {
            this.Id = id;
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        public void UpdateName(string firstName, string lastName)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.AddDomainEvent(new TestBaseDomainEvent(this));
        }

    }
}