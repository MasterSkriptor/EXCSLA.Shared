using Xunit;
using EXCSLA.Shared.Tests.Core.UnitTests.Builders;
using System;

namespace EXCSLA.Shared.Tests.Core.UnitTests
{
    public class BaseEntityShould
    {
        [Fact]
        public void BaseEntityEquals()
        {
            var test1 = BaseEntityBuilder.GetDefaultTestBaseEntity();
            var test2 = new BaseEntityBuilder(BaseEntityBuilder.DEFAULT_ID, BaseEntityBuilder.DEFAULT_FIRST_NAME, 
                BaseEntityBuilder.DEFAULT_LAST_NAME).Build();

            Assert.Equal(test1, test2);
        }

        [Fact]
        public void BaseEntityDoesNotEqual()
        {
            var test1 = BaseEntityBuilder.GetDefaultTestBaseEntity();
            var test2 = new BaseEntityBuilder(2, "Harold", "Collins").Build();

            Assert.NotEqual(test1, test2);
        }

        [Fact]
        public void GuidBaseEntityEquals()
        {
            var test1 = GuidBaseEntityBuilder.GetDefaultTestBaseEntity();
            var test2 = new GuidBaseEntityBuilder(GuidBaseEntityBuilder.DEFAULT_ID, GuidBaseEntityBuilder.DEFAULT_FIRST_NAME,
                GuidBaseEntityBuilder.DEFAULT_LAST_NAME).Build();

            Assert.Equal(test1, test2);
        }

        [Fact]
        public void GuidBaseEntityDoesNotEqual()
        {
            var test1 = GuidBaseEntityBuilder.GetDefaultTestBaseEntity();
            var test2 = new GuidBaseEntityBuilder(Guid.NewGuid(), "Harold", "Collins").Build();

            Assert.NotEqual(test1, test2);
        }
    }
}