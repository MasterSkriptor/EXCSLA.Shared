using Xunit;
using EXCSLA.Shared.Core;
using EXCSLA.Shared.Tests.Core.UnitTests.BaseTestObjects;
using System.Linq;

namespace EXCSLA.Shared.Tests.Core.UnitTests
{
    /// <summary>
    /// Comprehensive test suite for BaseDomainEvent class covering event creation,
    /// metadata, and event propagation through aggregates.
    /// </summary>
    public class BaseDomainEventShould
    {
        [Fact]
        public void BaseDomainEvent_HasCreatedDate_OnInstantiation()
        {
            var aggregateRoot = new TestAggregateRoot(1, "Test", "User");
            var domainEvent = new TestBaseDomainEvent(aggregateRoot);

            Assert.NotEqual(default, domainEvent.CreatedDate);
        }

        [Fact]
        public void BaseDomainEvent_CreatedDate_IsRecent()
        {
            var aggregateRoot = new TestAggregateRoot(1, "Test", "User");
            var beforeCreation = System.DateTime.UtcNow;
            
            var domainEvent = new TestBaseDomainEvent(aggregateRoot);
            
            var afterCreation = System.DateTime.UtcNow;

            Assert.InRange(domainEvent.CreatedDate, beforeCreation, afterCreation.AddSeconds(1));
        }

        [Fact]
        public void BaseDomainEvent_CanStoreAggregateData()
        {
            var aggregateRoot = new TestAggregateRoot(42, "John", "Doe");
            var domainEvent = new TestBaseDomainEvent(aggregateRoot);

            Assert.NotNull(domainEvent.UpdatedTestAggregateRoot);
            Assert.Equal("John", domainEvent.UpdatedTestAggregateRoot.FirstName);
            Assert.Equal("Doe", domainEvent.UpdatedTestAggregateRoot.LastName);
            Assert.Equal(42, domainEvent.UpdatedTestAggregateRoot.Id);
        }

        [Fact]
        public void AggregateRoot_AddDomainEvent_AddsEventToCollection()
        {
            var aggregateRoot = new TestAggregateRoot(1, "Test", "User");
            
            Assert.Empty(aggregateRoot.Events);

            var domainEvent = new TestBaseDomainEvent(aggregateRoot);
            aggregateRoot.AddDomainEvent(domainEvent);

            Assert.Single(aggregateRoot.Events);
            Assert.Equal(domainEvent, aggregateRoot.Events.First());
        }

        [Fact]
        public void AggregateRoot_AddDomainEvent_MultipleEvents_PreservesOrder()
        {
            var aggregateRoot = new TestAggregateRoot(1, "Test", "User");
            
            var event1 = new TestBaseDomainEvent(aggregateRoot);
            System.Threading.Thread.Sleep(10); // Ensure different timestamps
            var event2 = new TestBaseDomainEvent(aggregateRoot);
            System.Threading.Thread.Sleep(10);
            var event3 = new TestBaseDomainEvent(aggregateRoot);

            aggregateRoot.AddDomainEvent(event1);
            aggregateRoot.AddDomainEvent(event2);
            aggregateRoot.AddDomainEvent(event3);

            var events = aggregateRoot.Events.ToList();
            Assert.Equal(3, events.Count);
            Assert.Equal(event1.CreatedDate, events[0].CreatedDate);
            Assert.Equal(event2.CreatedDate, events[1].CreatedDate);
            Assert.Equal(event3.CreatedDate, events[2].CreatedDate);
        }

        [Fact]
        public void AggregateRoot_ClearDomainEvents_RemovesAllEvents()
        {
            var aggregateRoot = new TestAggregateRoot(1, "Test", "User");
            
            aggregateRoot.AddDomainEvent(new TestBaseDomainEvent(aggregateRoot));
            aggregateRoot.AddDomainEvent(new TestBaseDomainEvent(aggregateRoot));
            aggregateRoot.AddDomainEvent(new TestBaseDomainEvent(aggregateRoot));

            Assert.Equal(3, aggregateRoot.Events.Count());

            aggregateRoot.ClearDomainEvents();

            Assert.Empty(aggregateRoot.Events);
        }

        [Fact]
        public void AggregateRoot_Events_AllowMultipleIterations()
        {
            var aggregateRoot = new TestAggregateRoot(1, "Test", "User");
            
            aggregateRoot.AddDomainEvent(new TestBaseDomainEvent(aggregateRoot));
            aggregateRoot.AddDomainEvent(new TestBaseDomainEvent(aggregateRoot));

            // First iteration
            var firstCount = aggregateRoot.Events.Count();
            
            // Second iteration
            var secondCount = aggregateRoot.Events.Count();

            Assert.Equal(2, firstCount);
            Assert.Equal(2, secondCount);
        }

        [Fact]
        public void DomainEvent_ReflectsAggregateStateAtTimeOfCreation()
        {
            var aggregateRoot = new TestAggregateRoot(1, "Original", "Name");
            var eventBeforeUpdate = new TestBaseDomainEvent(aggregateRoot);

            // Update aggregate after event creation
            aggregateRoot.UpdateName("Updated", "Name");

            // Event should still reflect original state
            Assert.Equal("Original", eventBeforeUpdate.UpdatedTestAggregateRoot.FirstName);
        }

        [Fact]
        public void BaseDomainEvent_CanBeInherited_AndUsedCustomly()
        {
            var aggregateRoot = new TestAggregateRoot(1, "Test", "User");
            var customEvent = new TestBaseDomainEvent(aggregateRoot);

            Assert.IsAssignableFrom<BaseDomainEvent>(customEvent);
            Assert.NotNull(customEvent.UpdatedTestAggregateRoot);
        }
    }
}
