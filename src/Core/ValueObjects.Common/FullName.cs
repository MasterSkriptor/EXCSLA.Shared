using EXCSLA.Shared;
using Ardalis.GuardClauses;

namespace EXCSLA.Shared.Core.ValueObjects.Common
{
    public class FullName : ValueObject
    {
        public string FirstName {get; private set;}
        public string LastName {get; private set;}

        public FullName(string firstName, string lastName)
        {
            Guard.Against.NullOrWhiteSpace(firstName, nameof(firstName));
            Guard.Against.NullOrWhiteSpace(lastName, nameof(lastName));

            this.FirstName = firstName;
            this.LastName  = lastName;
        }

        public string AsFormatedName(bool lastNameFirst = false)
        {
            if(lastNameFirst) return this.LastName + ", " + this.FirstName;
            return this.FirstName + " " + this.LastName;
        }

        public override string ToString()
        {
            return this.AsFormatedName(false);
        }
    }
}