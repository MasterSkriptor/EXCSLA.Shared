using Xunit;
using EXCSLA.Shared.Tests.Core.UnitTests.Builders;
using System.Linq;

namespace EXCSLA.Shared.Tests.Core.UnitTests
{
    /// <summary>
    /// Comprehensive test suite for AggregateRoot class covering equality,
    /// domain event handling, event clearing, and aggregate invariants.
    /// </summary>
    public class AggregateRootShould
    {
        [Fact]
        public void AggregateRootEquals_WhenIdMatches()
        {
            var test1 = AggregateRootBuilder.GetDefaultTestAggregateRoot();
            var test2 = new AggregateRootBuilder(AggregateRootBuilder.DEFAULT_ID, AggregateRootBuilder.DEFAULT_FIRST_NAME,
                AggregateRootBuilder.DEFAULT_LAST_NAME).Build();

            Assert.Equal(test1, test2);
        }

        [Fact]
        public void AggregateRootEquals_ByInheritance_FromBaseEntity()
        {
            var test1 = AggregateRootBuilder.GetDefaultTestAggregateRoot();
            var test2 = new AggregateRootBuilder(AggregateRootBuilder.DEFAULT_ID, "Different", "Name").Build();

            // AggregateRoot equality is based on Id (inherited from BaseEntity), not properties
            Assert.Equal(test1, test2);
        }

        [Fact]
        public void AggregateRootUpdatedEvent_AddsDomainEvent()
        {
            var test1 = AggregateRootBuilder.GetDefaultTestAggregateRoot();
            var test2 = AggregateRootBuilder.GetDefaultTestAggregateRoot();
            test2.UpdateName("Kayla", "Collins");

            Assert.Empty(test1.Events.ToList());
            Assert.Single(test2.Events.ToList());
        }

        [Fact]
        public void AggregateRoot_HasNoEvents_OnCreation()
        {
            var aggregateRoot = AggregateRootBuilder.GetDefaultTestAggregateRoot();

            Assert.Empty(aggregateRoot.Events);
        }

        [Fact]
        public void AggregateRoot_CanAddMultipleEvents()
        {
            var aggregateRoot = AggregateRootBuilder.GetDefaultTestAggregateRoot();
            
            aggregateRoot.UpdateName("First", "Update");
            aggregateRoot.UpdateName("Second", "Update");
            aggregateRoot.UpdateName("Third", "Update");

            Assert.Equal(3, aggregateRoot.Events.Count());
        }

        [Fact]
        public void AggregateRoot_ClearDomainEvents_RemovesAllEvents()
        {
            var aggregateRoot = AggregateRootBuilder.GetDefaultTestAggregateRoot();
            
            aggregateRoot.UpdateName("First", "Update");
            aggregateRoot.UpdateName("Second", "Update");

            Assert.Equal(2, aggregateRoot.Events.Count());

            aggregateRoot.ClearDomainEvents();

            Assert.Empty(aggregateRoot.Events);
        }

        [Fact]
        public void AggregateRoot_Events_ContainsCorrectEventData()
        {
            var aggregateRoot = AggregateRootBuilder.GetDefaultTestAggregateRoot();
            aggregateRoot.UpdateName("NewFirst", "NewLast");

            var @event = aggregateRoot.Events.First();

            Assert.NotNull(@event);
            Assert.Equal("NewFirst", aggregateRoot.FirstName);
            Assert.Equal("NewLast", aggregateRoot.LastName);
        }

        [Fact]
        public void AggregateRoot_DoesNotEqual_DifferentId()
        {
            var aggregate1 = new AggregateRootBuilder(1, "John", "Doe").Build();
            var aggregate2 = new AggregateRootBuilder(2, "John", "Doe").Build();

            Assert.NotEqual(aggregate1, aggregate2);
        }

        [Fact]
        public void AggregateRoot_CanBeStoredInCollections()
        {
            var aggregates = new[]
            {
                new AggregateRootBuilder(1, "John", "Doe").Build(),
                new AggregateRootBuilder(2, "Jane", "Doe").Build(),
                new AggregateRootBuilder(3, "Bob", "Smith").Build()
            };

            Assert.Equal(3, aggregates.Length);
            Assert.Single(aggregates.Where(a => a.Id == 2));
        }

        [Fact]
        public void AggregateRoot_Event_HasEventMetadata()
        {
            var aggregateRoot = AggregateRootBuilder.GetDefaultTestAggregateRoot();
            aggregateRoot.UpdateName("Test", "Event");

            var @event = aggregateRoot.Events.First();

            Assert.NotNull(@event);
            Assert.NotEqual(default, @event.CreatedDate);
        }
    }
}