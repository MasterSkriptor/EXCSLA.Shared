using Xunit;
using Core.Builders;

namespace Core
{
    public class ValueObjectShould
    {
        [Fact]
        public void ValueObjectEquals()
        {
            var test1 = ValueObjectBaseBuilder.GetDefaultTestValueObject();
            var test2 = new ValueObjectBaseBuilder(ValueObjectBaseBuilder.DEFAULT_FIRST_NAME, ValueObjectBaseBuilder.DEFAULT_LAST_NAME).Build();

            Assert.Equal(test1, test2);
        }

        [Fact]
        public void ValueObjectDoesNotEqual()
        {
            var test1 = ValueObjectBaseBuilder.GetDefaultTestValueObject();
            var test2 = new ValueObjectBaseBuilder("Kayla", "Collins").Build();

            Assert.NotEqual(test1, test2);
        }
    }
}