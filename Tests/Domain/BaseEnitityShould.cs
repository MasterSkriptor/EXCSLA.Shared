using Xunit;
using EXCSLA.Shared.Domain.Tests.Builders;
using EXCSLA.Shared.Domain.Tests.BaseTestObjects;
using System;
using System.Collections.Generic;

namespace EXCSLA.Shared.Domain.Tests
{
	/// <summary>
	/// Comprehensive test suite for BaseEntity class covering equality, 
	/// identity comparison, and edge cases.
	/// </summary>
	public class BaseEntityShould
	{
		[Fact]
		public void BaseEntityEquals_WhenIdAndTypeMatch()
		{
			var test1 = BaseEntityBuilder.GetDefaultTestBaseEntity();
			var test2 = new BaseEntityBuilder(BaseEntityBuilder.DEFAULT_ID, BaseEntityBuilder.DEFAULT_FIRST_NAME, 
				BaseEntityBuilder.DEFAULT_LAST_NAME).Build();

			Assert.Equal(test1, test2);
		}

		[Fact]
		public void BaseEntityDoesNotEqual_WhenIdDiffers()
		{
			var test1 = BaseEntityBuilder.GetDefaultTestBaseEntity();
			var test2 = new BaseEntityBuilder(2, "Harold", "Collins").Build();

			Assert.NotEqual(test1, test2);
		}

		[Fact]
		public void BaseEntityEquals_WhenPropertiesDifferButIdMatches()
		{
			// BaseEntity equality is based on Id, not property values
			var test1 = BaseEntityBuilder.GetDefaultTestBaseEntity();
			var test2 = new BaseEntityBuilder(BaseEntityBuilder.DEFAULT_ID, "Different", "Names").Build();

			Assert.Equal(test1, test2);
		}

		[Fact]
		public void GuidBaseEntityEquals_WhenIdAndTypeMatch()
		{
			var test1 = GuidBaseEntityBuilder.GetDefaultTestBaseEntity();
			var test2 = new GuidBaseEntityBuilder(GuidBaseEntityBuilder.DEFAULT_ID, GuidBaseEntityBuilder.DEFAULT_FIRST_NAME,
				GuidBaseEntityBuilder.DEFAULT_LAST_NAME).Build();

			Assert.Equal(test1, test2);
		}

		[Fact]
		public void GuidBaseEntityDoesNotEqual_WhenIdDiffers()
		{
			var test1 = GuidBaseEntityBuilder.GetDefaultTestBaseEntity();
			var test2 = new GuidBaseEntityBuilder(Guid.NewGuid(), "Harold", "Collins").Build();

			Assert.NotEqual(test1, test2);
		}

		[Fact]
		public void BaseEntity_HasCorrectId_AfterConstruction()
		{
			var expectedId = 42;
			var entity = new BaseEntityBuilder(expectedId, "Test", "User").Build();

			Assert.Equal(expectedId, entity.Id);
		}

		[Fact]
		public void GuidBaseEntity_HasCorrectId_AfterConstruction()
		{
			var expectedId = Guid.NewGuid();
			var entity = new GuidBaseEntityBuilder(expectedId, "Test", "User").Build();

			Assert.Equal(expectedId, entity.Id);
		}

		[Fact]
		public void BaseEntity_EqualityOperator_ReturnsTrue_ForEqualEntities()
		{
			var entity1 = BaseEntityBuilder.GetDefaultTestBaseEntity();
			var entity2 = new BaseEntityBuilder(BaseEntityBuilder.DEFAULT_ID, "Any", "Name").Build();

			Assert.True(entity1 == entity2);
		}

		[Fact]
		public void BaseEntity_InequalityOperator_ReturnsTrue_ForDifferentEntities()
		{
			var entity1 = BaseEntityBuilder.GetDefaultTestBaseEntity();
			var entity2 = new BaseEntityBuilder(999, "Any", "Name").Build();

			Assert.True(entity1 != entity2);
		}

		[Fact]
		public void BaseEntity_GetHashCode_IsSame_ForEqualEntities()
		{
			// ...existing code...
		}
	}
}
