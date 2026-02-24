using EXCSLA.Shared.Domain.Tests.BaseTestObjects;

namespace EXCSLA.Shared.Domain.Tests.Builders
{
	public class AggregateRootBuilder
	{
		public static int DEFAULT_ID = 1;
		public static string DEFAULT_FIRST_NAME = "Harold";
		public static string DEFAULT_LAST_NAME = "Collins";

		private TestAggregateRoot _testAggregateRoot;

		public AggregateRootBuilder(int id, string firstName, string lastName)
		{
			_testAggregateRoot = new TestAggregateRoot(id, firstName, lastName);
		}

		public TestAggregateRoot Build()
		{
			return this._testAggregateRoot;
		}

		public static TestAggregateRoot GetDefaultTestAggregateRoot()
		{
			var tar = new AggregateRootBuilder(DEFAULT_ID, DEFAULT_FIRST_NAME, DEFAULT_LAST_NAME);
			return tar.Build();
		}
	}

	public class StringAggregateRootBuilder
	{
		public static string DEFAULT_ID = "aggregate-string-id-1";
		public static string DEFAULT_FIRST_NAME = "Harold";
		public static string DEFAULT_LAST_NAME = "Collins";

		private TestStringAggregateRoot _testAggregateRoot;

		public StringAggregateRootBuilder(string id, string firstName, string lastName)
		{
			_testAggregateRoot = new TestStringAggregateRoot(id, firstName, lastName);
		}

		public TestStringAggregateRoot Build()
		{
			return this._testAggregateRoot;
		}

		public static TestStringAggregateRoot GetDefaultTestAggregateRoot()
		{
			var tar = new StringAggregateRootBuilder(DEFAULT_ID, DEFAULT_FIRST_NAME, DEFAULT_LAST_NAME);
			return tar.Build();
		}
	}
}
