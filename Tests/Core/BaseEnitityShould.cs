using Xunit;
using Core.Builders;

namespace Core
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
            var test2 = new BaseEntityBuilder(1, "Harold", "Collins").Build();

            Assert.NotEqual(test1, test2);
        }
    }
}