using EXCSLA.Shared.Core;

namespace EXCSLA.Shared.Tests.Core.UnitTests.BaseTestObjects
{
    public class TestBaseEntity : BaseEntity<int>
    {
        public string FirstName {get; private set;}
        public string LastName {get; private set;}

        public TestBaseEntity(int id, string firstName, string lastName)
        {
            this.Id = id;
            this.FirstName = firstName;
            this.LastName  = lastName;
        }

        public override string ToString()
        {
            return this.FirstName + " " + this.LastName;
        }
    }
}