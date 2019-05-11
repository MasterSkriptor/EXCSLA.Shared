using EXCSLA.Shared.Tests.Core.UnitTests.BaseTestObjects;

namespace EXCSLA.Shared.Tests.Core.UnitTests.Builders
{
    public class BaseEntityBuilder
    {
        public static int DEFAULT_ID = 1;
        public static string DEFAULT_FIRST_NAME = "Harold";
        public static string DEFAULT_LAST_NAME = "Collins";

        private TestBaseEntity _baseEntity;

        public BaseEntityBuilder(int id, string firstName, string lastName)
        {
            _baseEntity  = new TestBaseEntity(id, firstName, lastName);
        }

        public TestBaseEntity Build()
        {
            return _baseEntity;
        }

        public static TestBaseEntity GetDefaultTestBaseEntity()
        {
            var tvo = new BaseEntityBuilder(DEFAULT_ID, DEFAULT_FIRST_NAME, DEFAULT_LAST_NAME);
            return tvo.Build();
        }

    }
}