namespace EXCSLA.Shared.Tests.Core.UnitTests
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
            var entity1 = BaseEntityBuilder.GetDefaultTestBaseEntity();
            var entity2 = new BaseEntityBuilder(BaseEntityBuilder.DEFAULT_ID, "Different", "Name").Build();

            Assert.Equal(entity1.GetHashCode(), entity2.GetHashCode());
        }

        [Fact]
        public void BaseEntity_CanBeUsedInHashSet_WithoutDuplicates()
        {
            var entity1 = BaseEntityBuilder.GetDefaultTestBaseEntity();
            var entity2 = new BaseEntityBuilder(BaseEntityBuilder.DEFAULT_ID, "Different", "Name").Build();

            var hashSet = new HashSet<TestIntBaseEntity> { entity1, entity2 };

            Assert.Single(hashSet);
        }

        [Fact]
        public void GuidBaseEntity_CanBeUsedInHashSet_WithoutDuplicates()
        {
            var entity1 = GuidBaseEntityBuilder.GetDefaultTestBaseEntity();
            var entity2 = new GuidBaseEntityBuilder(GuidBaseEntityBuilder.DEFAULT_ID, "Different", "Name").Build();

            var hashSet = new HashSet<TestGuidBaseEntity> { entity1, entity2 };

            Assert.Single(hashSet);
        }

        [Fact]
        public void GuidBaseEntity_GetHashCode_IsSame_ForEqualEntities()
        {
            var entity1 = GuidBaseEntityBuilder.GetDefaultTestBaseEntity();
            var entity2 = new GuidBaseEntityBuilder(GuidBaseEntityBuilder.DEFAULT_ID, "Different", "Name").Build();

            Assert.Equal(entity1.GetHashCode(), entity2.GetHashCode());
        }

        [Fact]
        public void GuidBaseEntity_EqualityOperator_ReturnsTrue_ForEqualEntities()
        {
            var entity1 = GuidBaseEntityBuilder.GetDefaultTestBaseEntity();
            var entity2 = new GuidBaseEntityBuilder(GuidBaseEntityBuilder.DEFAULT_ID, "Any", "Name").Build();

            Assert.True(entity1 == entity2);
        }

        [Fact]
        public void GuidBaseEntity_InequalityOperator_ReturnsTrue_ForDifferentEntities()
        {
            var entity1 = GuidBaseEntityBuilder.GetDefaultTestBaseEntity();
            var entity2 = new GuidBaseEntityBuilder(Guid.NewGuid(), "Any", "Name").Build();

            Assert.True(entity1 != entity2);
        }

        [Fact]
        public void BaseEntity_GetHashCode_HandlesDefaultId()
        {
            var entity1 = new TestIntBaseEntity(0, "Test", "User");
            var entity2 = new TestIntBaseEntity(0, "Different", "User");

            Assert.Equal(entity1.GetHashCode(), entity2.GetHashCode());
        }

        [Fact]
        public void GuidBaseEntity_GetHashCode_HandlesDefaultId()
        {
            var entity1 = new TestGuidBaseEntity(Guid.Empty, "Test", "User");
            var entity2 = new TestGuidBaseEntity(Guid.Empty, "Different", "User");

            Assert.Equal(entity1.GetHashCode(), entity2.GetHashCode());
        }

        [Fact]
        public void BaseEntity_Equals_HandlesDefaultId()
        {
            var entity1 = new TestIntBaseEntity(0, "Test", "User");
            var entity2 = new TestIntBaseEntity(0, "Different", "User");

            Assert.Equal(entity1, entity2);
        }

        [Fact]
        public void GuidBaseEntity_Equals_HandlesDefaultId()
        {
            var entity1 = new TestGuidBaseEntity(Guid.Empty, "Test", "User");
            var entity2 = new TestGuidBaseEntity(Guid.Empty, "Different", "User");

            Assert.Equal(entity1, entity2);
        }

        // String ID Tests
        [Fact]
        public void StringBaseEntityEquals_WhenIdAndTypeMatch()
        {
            var test1 = StringBaseEntityBuilder.GetDefaultTestBaseEntity();
            var test2 = new StringBaseEntityBuilder(StringBaseEntityBuilder.DEFAULT_ID, StringBaseEntityBuilder.DEFAULT_FIRST_NAME,
                StringBaseEntityBuilder.DEFAULT_LAST_NAME).Build();

            Assert.Equal(test1, test2);
        }

        [Fact]
        public void StringBaseEntityDoesNotEqual_WhenIdDiffers()
        {
            var test1 = StringBaseEntityBuilder.GetDefaultTestBaseEntity();
            var test2 = new StringBaseEntityBuilder("string-id-2", "Harold", "Collins").Build();

            Assert.NotEqual(test1, test2);
        }

        [Fact]
        public void StringBaseEntity_HasCorrectId_AfterConstruction()
        {
            var expectedId = "test-string-id";
            var entity = new StringBaseEntityBuilder(expectedId, "Test", "User").Build();

            Assert.Equal(expectedId, entity.Id);
        }

        [Fact]
        public void StringBaseEntity_EqualityOperator_ReturnsTrue_ForEqualEntities()
        {
            var entity1 = StringBaseEntityBuilder.GetDefaultTestBaseEntity();
            var entity2 = new StringBaseEntityBuilder(StringBaseEntityBuilder.DEFAULT_ID, "Any", "Name").Build();

            Assert.True(entity1 == entity2);
        }

        [Fact]
        public void StringBaseEntity_InequalityOperator_ReturnsTrue_ForDifferentEntities()
        {
            var entity1 = StringBaseEntityBuilder.GetDefaultTestBaseEntity();
            var entity2 = new StringBaseEntityBuilder("different-id", "Any", "Name").Build();

            Assert.True(entity1 != entity2);
        }

        [Fact]
        public void StringBaseEntity_GetHashCode_IsSame_ForEqualEntities()
        {
            var entity1 = StringBaseEntityBuilder.GetDefaultTestBaseEntity();
            var entity2 = new StringBaseEntityBuilder(StringBaseEntityBuilder.DEFAULT_ID, "Different", "Name").Build();

            Assert.Equal(entity1.GetHashCode(), entity2.GetHashCode());
        }

        [Fact]
        public void StringBaseEntity_CanBeUsedInHashSet_WithoutDuplicates()
        {
            var entity1 = StringBaseEntityBuilder.GetDefaultTestBaseEntity();
            var entity2 = new StringBaseEntityBuilder(StringBaseEntityBuilder.DEFAULT_ID, "Different", "Name").Build();

            var hashSet = new HashSet<TestStringBaseEntity> { entity1, entity2 };

            Assert.Single(hashSet);
        }

        [Fact]
        public void StringBaseEntity_GetHashCode_HandlesEmptyId()
        {
            var entity1 = new TestStringBaseEntity(string.Empty, "Test", "User");
            var entity2 = new TestStringBaseEntity(string.Empty, "Different", "User");

            Assert.Equal(entity1.GetHashCode(), entity2.GetHashCode());
        }

        [Fact]
        public void StringBaseEntity_Equals_HandlesEmptyId()
        {
            var entity1 = new TestStringBaseEntity(string.Empty, "Test", "User");
            var entity2 = new TestStringBaseEntity(string.Empty, "Different", "User");

            Assert.Equal(entity1, entity2);
        }

        [Fact]
        public void StringBaseEntity_Equals_WhenPropertiesDifferButIdMatches()
        {
            var test1 = StringBaseEntityBuilder.GetDefaultTestBaseEntity();
            var test2 = new StringBaseEntityBuilder(StringBaseEntityBuilder.DEFAULT_ID, "Different", "Names").Build();

            Assert.Equal(test1, test2);
        }

        // Long ID Tests
        [Fact]
        public void LongBaseEntityEquals_WhenIdAndTypeMatch()
        {
            var test1 = LongBaseEntityBuilder.GetDefaultTestBaseEntity();
            var test2 = new LongBaseEntityBuilder(LongBaseEntityBuilder.DEFAULT_ID, LongBaseEntityBuilder.DEFAULT_FIRST_NAME,
                LongBaseEntityBuilder.DEFAULT_LAST_NAME).Build();

            Assert.Equal(test1, test2);
        }

        [Fact]
        public void LongBaseEntityDoesNotEqual_WhenIdDiffers()
        {
            var test1 = LongBaseEntityBuilder.GetDefaultTestBaseEntity();
            var test2 = new LongBaseEntityBuilder(2000000000L, "Harold", "Collins").Build();

            Assert.NotEqual(test1, test2);
        }

        [Fact]
        public void LongBaseEntity_HasCorrectId_AfterConstruction()
        {
            var expectedId = 999999999999L;
            var entity = new LongBaseEntityBuilder(expectedId, "Test", "User").Build();

            Assert.Equal(expectedId, entity.Id);
        }

        [Fact]
        public void LongBaseEntity_EqualityOperator_ReturnsTrue_ForEqualEntities()
        {
            var entity1 = LongBaseEntityBuilder.GetDefaultTestBaseEntity();
            var entity2 = new LongBaseEntityBuilder(LongBaseEntityBuilder.DEFAULT_ID, "Any", "Name").Build();

            Assert.True(entity1 == entity2);
        }

        [Fact]
        public void LongBaseEntity_InequalityOperator_ReturnsTrue_ForDifferentEntities()
        {
            var entity1 = LongBaseEntityBuilder.GetDefaultTestBaseEntity();
            var entity2 = new LongBaseEntityBuilder(999L, "Any", "Name").Build();

            Assert.True(entity1 != entity2);
        }

        [Fact]
        public void LongBaseEntity_GetHashCode_IsSame_ForEqualEntities()
        {
            var entity1 = LongBaseEntityBuilder.GetDefaultTestBaseEntity();
            var entity2 = new LongBaseEntityBuilder(LongBaseEntityBuilder.DEFAULT_ID, "Different", "Name").Build();

            Assert.Equal(entity1.GetHashCode(), entity2.GetHashCode());
        }

        [Fact]
        public void LongBaseEntity_CanBeUsedInHashSet_WithoutDuplicates()
        {
            var entity1 = LongBaseEntityBuilder.GetDefaultTestBaseEntity();
            var entity2 = new LongBaseEntityBuilder(LongBaseEntityBuilder.DEFAULT_ID, "Different", "Name").Build();

            var hashSet = new HashSet<TestLongBaseEntity> { entity1, entity2 };

            Assert.Single(hashSet);
        }

        [Fact]
        public void LongBaseEntity_GetHashCode_HandlesDefaultId()
        {
            var entity1 = new TestLongBaseEntity(0L, "Test", "User");
            var entity2 = new TestLongBaseEntity(0L, "Different", "User");

            Assert.Equal(entity1.GetHashCode(), entity2.GetHashCode());
        }

        [Fact]
        public void LongBaseEntity_Equals_HandlesDefaultId()
        {
            var entity1 = new TestLongBaseEntity(0L, "Test", "User");
            var entity2 = new TestLongBaseEntity(0L, "Different", "User");

            Assert.Equal(entity1, entity2);
        }

        [Fact]
        public void LongBaseEntity_Equals_WhenPropertiesDifferButIdMatches()
        {
            var test1 = LongBaseEntityBuilder.GetDefaultTestBaseEntity();
            var test2 = new LongBaseEntityBuilder(LongBaseEntityBuilder.DEFAULT_ID, "Different", "Names").Build();

            Assert.Equal(test1, test2);
        }
    }
}