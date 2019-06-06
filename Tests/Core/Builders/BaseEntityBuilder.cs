using EXCSLA.Shared.Tests.Core.UnitTests.BaseTestObjects;
using System;

namespace EXCSLA.Shared.Tests.Core.UnitTests.Builders
{
    public class BaseEntityBuilder
    {
        public static int DEFAULT_ID = 1;
        public static string DEFAULT_FIRST_NAME = "Harold";
        public static string DEFAULT_LAST_NAME = "Collins";

        private TestIntBaseEntity _baseEntity;

        public BaseEntityBuilder(int id, string firstName, string lastName)
        {
            _baseEntity  = new TestIntBaseEntity(id, firstName, lastName);
        }

        public TestIntBaseEntity Build()
        {
            return _baseEntity;
        }

        public static TestIntBaseEntity GetDefaultTestBaseEntity()
        {
            var tvo = new BaseEntityBuilder(DEFAULT_ID, DEFAULT_FIRST_NAME, DEFAULT_LAST_NAME);
            return tvo.Build();
        }

    }

    public class GuidBaseEntityBuilder
    {
        public static Guid DEFAULT_ID = Guid.NewGuid();
        public static string DEFAULT_FIRST_NAME = "Harold";
        public static string DEFAULT_LAST_NAME = "Collins";

        private TestGuidBaseEntity _baseEntity;

        public GuidBaseEntityBuilder(Guid id, string firstName, string lastName)
        {
            _baseEntity = new TestGuidBaseEntity(id, firstName, lastName);
        }

        public TestGuidBaseEntity Build()
        {
            return _baseEntity;
        }

        public static TestGuidBaseEntity GetDefaultTestBaseEntity()
        {
            var tvo = new GuidBaseEntityBuilder(DEFAULT_ID, DEFAULT_FIRST_NAME, DEFAULT_LAST_NAME);
            return tvo.Build();
        }

    }
}