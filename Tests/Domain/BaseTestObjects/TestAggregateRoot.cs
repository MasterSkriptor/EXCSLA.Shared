using EXCSLA.Shared.Domain;

namespace EXCSLA.Shared.Domain.Tests.BaseTestObjects
{
	public class TestAggregateRoot : BaseAggregateRoot<int>
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

	public class TestStringAggregateRoot : BaseAggregateRoot<string>
	{
		public string FirstName { get; private set; }
		public string LastName { get; private set; }

		public TestStringAggregateRoot(string id, string firstName, string lastName)
		{
			this.Id = id;
			this.FirstName = firstName;
			this.LastName = lastName;
		}

		public void UpdateName(string firstName, string lastName)
		{
			this.FirstName = firstName;
			this.LastName = lastName;
			this.AddDomainEvent(new TestStringDomainEvent(this));
		}
	}
}
