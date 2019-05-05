using Xunit;
using Core.Builders;

using System.Linq;

namespace Core
{
    public class AggregateRootShould
    {
        [Fact]
        public void AggregateRootEquals()
        {
            var test1 = AggregateRootBuilder.GetDefaultTestAggregateRoot();
            var test2 = new AggregateRootBuilder(AggregateRootBuilder.DEFAULT_ID, AggregateRootBuilder.DEFAULT_FIRST_NAME,
                AggregateRootBuilder.DEFAULT_LAST_NAME).Build();

            Assert.Equal(test1, test2);
        }

        [Fact]
        public void AggregateRootUpdatedEvent()
        {
            var test1 = AggregateRootBuilder.GetDefaultTestAggregateRoot();
            var test2 = AggregateRootBuilder.GetDefaultTestAggregateRoot();
            test2.UpdateName("Kayla", "Collins");

            Assert.Empty(test1.Events.ToList());
            Assert.Single(test2.Events.ToList());
        }


    }
}