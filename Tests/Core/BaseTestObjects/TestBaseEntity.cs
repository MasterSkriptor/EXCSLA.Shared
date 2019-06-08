using EXCSLA.Shared.Core;
using System;
using System.Collections.Generic;

namespace EXCSLA.Shared.Tests.Core.UnitTests.BaseTestObjects
{
    public class TestIntBaseEntity : BaseEntity
    {
        public string FirstName {get; private set;}
        public string LastName {get; private set;}

        public TestIntBaseEntity(int id, string firstName, string lastName)
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

    public class TestGuidBaseEntity : BaseEntity
    {
        public new Guid Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public TestGuidBaseEntity(Guid id, string firstName, string lastName)
        {
            this.Id = id;
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        public override string ToString()
        {
            return this.FirstName + " " + this.LastName;
        }

        public override bool Equals(object obj)
        {
            var other = obj as TestGuidBaseEntity;

            if (ReferenceEquals(other, null))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (ReferenceEquals(other, default) || ReferenceEquals(this, default))
                return false;

            return EqualityComparer<object>.Default.Equals(Id, other.Id);
        }

        public static bool operator ==(TestGuidBaseEntity a, TestGuidBaseEntity b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(TestGuidBaseEntity a, TestGuidBaseEntity b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}