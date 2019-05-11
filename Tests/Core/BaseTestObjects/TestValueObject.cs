using EXCSLA.Shared.Core;

namespace EXCSLA.Shared.Tests.Core.UnitTests.BaseTestObjects
{
    public class TestValueObject : ValueObject
    {
        public string FirstName {get; private set;}
        public string LastName {get; private set;}

        public TestValueObject(string firstName, string lastName)
        {
            this.FirstName = firstName;
            this.LastName  = lastName;
        }

        public override string ToString()
        {
            return this.FirstName + " " + this.LastName;
        }
    }
}