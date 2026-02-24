using Xunit;
using EXCSLA.Shared.Domain;
using EXCSLA.Shared.Domain.Tests.BaseTestObjects;
using System.Linq;

namespace EXCSLA.Shared.Domain.Tests
{
	/// <summary>
	/// Comprehensive test suite for BaseDomainEvent class covering event creation,
	/// metadata, and event propagation through aggregates.
	/// </summary>
	public class BaseDomainEventShould
	{
		[Fact]
		public void BaseDomainEvent_HasDateOccured_OnInstantiation()
		{
			var aggregateRoot = new TestAggregateRoot(1, "Test", "User");
			var domainEvent = new TestBaseDomainEvent(aggregateRoot);

			Assert.NotEqual(default, domainEvent.DateOccured);
		}

		[Fact]
		public void BaseDomainEvent_DateOccured_IsRecent()
		{
			var aggregateRoot = new TestAggregateRoot(1, "Test", "User");
			var beforeCreation = System.DateTime.UtcNow;
            
			var domainEvent = new TestBaseDomainEvent(aggregateRoot);
            
			var afterCreation = System.DateTime.UtcNow;

			Assert.InRange(domainEvent.DateOccured, beforeCreation, afterCreation.AddSeconds(1));
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
		public void AggregateRoot_UpdateName_AddsEventToCollection()
		{
			var aggregateRoot = new TestAggregateRoot(1, "Test", "User");
            
			Assert.Empty(aggregateRoot.Events);

			aggregateRoot.UpdateName("Updated", "Name");

			Assert.Single(aggregateRoot.Events);
		}

		[Fact]
		public void AggregateRoot_UpdateName_MultipleEvents_PreservesOrder()
		{
			var aggregateRoot = new TestAggregateRoot(1, "Test", "User");
            
			var beforeEvent1 = System.DateTime.UtcNow;
			aggregateRoot.UpdateName("First", "Update");
			System.Threading.Thread.Sleep(10); // Ensure different timestamps
            
			var beforeEvent2 = System.DateTime.UtcNow;
			aggregateRoot.UpdateName("Second", "Update");
			System.Threading.Thread.Sleep(10);
            
			var beforeEvent3 = System.DateTime.UtcNow;
			aggregateRoot.UpdateName("Third", "Update");

			var events = aggregateRoot.Events.ToList();
			Assert.Equal(3, events.Count);
			Assert.InRange(events[0].DateOccured, beforeEvent1, beforeEvent2.AddSeconds(1));
			Assert.InRange(events[1].DateOccured, beforeEvent2, beforeEvent3.AddSeconds(1));
		}

		[Fact]
		public void AggregateRoot_ClearEvents_RemovesAllEvents()
		{
			var aggregateRoot = new TestAggregateRoot(1, "Test", "User");
            
			aggregateRoot.UpdateName("First", "Update");
			aggregateRoot.UpdateName("Second", "Update");
			aggregateRoot.UpdateName("Third", "Update");

			Assert.Equal(3, aggregateRoot.Events.Count());

			aggregateRoot.ClearEvents();

			Assert.Empty(aggregateRoot.Events);
		}

		[Fact]
		public void AggregateRoot_Events_AllowMultipleIterations()
		{
			// ...existing code...
		}
	}
}
