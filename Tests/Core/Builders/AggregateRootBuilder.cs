using Core.BaseTestObjects;

namespace Core.Builders
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
}