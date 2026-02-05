using Xunit;
using EXCSLA.Shared.Tests.Core.UnitTests.Builders;

namespace EXCSLA.Shared.Tests.Core.UnitTests
{
    /// <summary>
    /// Comprehensive test suite for ValueObject class covering equality,
    /// immutability semantics, and value-based identity.
    /// </summary>
    public class ValueObjectShould
    {
        [Fact]
        public void ValueObjectEquals_WhenValuesMatch()
        {
            var test1 = ValueObjectBaseBuilder.GetDefaultTestValueObject();
            var test2 = new ValueObjectBaseBuilder(ValueObjectBaseBuilder.DEFAULT_FIRST_NAME, ValueObjectBaseBuilder.DEFAULT_LAST_NAME).Build();

            Assert.Equal(test1, test2);
        }

        [Fact]
        public void ValueObjectDoesNotEqual_WhenValuesDiffer()
        {
            var test1 = ValueObjectBaseBuilder.GetDefaultTestValueObject();
            var test2 = new ValueObjectBaseBuilder("Kayla", "Collins").Build();

            Assert.NotEqual(test1, test2);
        }

        [Fact]
        public void ValueObject_AreEqual_EvenIfDifferentInstances()
        {
            var vo1 = new ValueObjectBaseBuilder("John", "Doe").Build();
            var vo2 = new ValueObjectBaseBuilder("John", "Doe").Build();

            // Value objects with same values should be equal even if different instances
            Assert.Equal(vo1, vo2);
            Assert.NotSame(vo1, vo2);
        }

        [Fact]
        public void ValueObject_AreNotEqual_WhenFirstNameDiffers()
        {
            var vo1 = new ValueObjectBaseBuilder("John", "Doe").Build();
            var vo2 = new ValueObjectBaseBuilder("Jane", "Doe").Build();

            Assert.NotEqual(vo1, vo2);
        }

        [Fact]
        public void ValueObject_AreNotEqual_WhenLastNameDiffers()
        {
            var vo1 = new ValueObjectBaseBuilder("John", "Doe").Build();
            var vo2 = new ValueObjectBaseBuilder("John", "Smith").Build();

            Assert.NotEqual(vo1, vo2);
        }

        [Fact]
        public void ValueObject_GetHashCode_IsSame_ForEqualValues()
        {
            var vo1 = new ValueObjectBaseBuilder("John", "Doe").Build();
            var vo2 = new ValueObjectBaseBuilder("John", "Doe").Build();

            Assert.Equal(vo1.GetHashCode(), vo2.GetHashCode());
        }

        [Fact]
        public void ValueObject_GetHashCode_IsDifferent_ForDifferentValues()
        {
            var vo1 = new ValueObjectBaseBuilder("John", "Doe").Build();
            var vo2 = new ValueObjectBaseBuilder("John", "Smith").Build();

            Assert.NotEqual(vo1.GetHashCode(), vo2.GetHashCode());
        }

        [Fact]
        public void ValueObject_CanBeUsedAsKeyInDictionary()
        {
            var vo1 = new ValueObjectBaseBuilder("John", "Doe").Build();
            var vo2 = new ValueObjectBaseBuilder("John", "Doe").Build();
            var vo3 = new ValueObjectBaseBuilder("Jane", "Smith").Build();

            var dict = new Dictionary<ValueObjectBaseBuilder.TestValueObject, string>
            {
                { vo1, "First" },
                { vo3, "Third" }
            };

            // vo2 has same value as vo1, so should retrieve the same entry
            Assert.Equal("First", dict[vo2]);
            Assert.Equal(2, dict.Count);
        }

        [Fact]
        public void ValueObject_CanBeUsedInHashSet_WithoutDuplicates()
        {
            var vo1 = new ValueObjectBaseBuilder("John", "Doe").Build();
            var vo2 = new ValueObjectBaseBuilder("John", "Doe").Build();
            var vo3 = new ValueObjectBaseBuilder("Jane", "Smith").Build();

            var set = new HashSet<ValueObjectBaseBuilder.TestValueObject> { vo1, vo2, vo3 };

            // vo1 and vo2 have same value, so only 2 items in set
            Assert.Equal(2, set.Count);
        }

        [Fact]
        public void ValueObject_EqualityOperator_ReturnsTrue_ForEqualValues()
        {
            var vo1 = new ValueObjectBaseBuilder("John", "Doe").Build();
            var vo2 = new ValueObjectBaseBuilder("John", "Doe").Build();

            Assert.True(vo1 == vo2);
        }

        [Fact]
        public void ValueObject_InequalityOperator_ReturnsTrue_ForDifferentValues()
        {
            var vo1 = new ValueObjectBaseBuilder("John", "Doe").Build();
            var vo2 = new ValueObjectBaseBuilder("John", "Smith").Build();

            Assert.True(vo1 != vo2);
        }

        [Fact]
        public void ValueObject_HasCorrectProperties_AfterConstruction()
        {
            var firstName = "TestFirst";
            var lastName = "TestLast";
            var vo = new ValueObjectBaseBuilder(firstName, lastName).Build();

            Assert.Equal(firstName, vo.FirstName);
            Assert.Equal(lastName, vo.LastName);
        }
    }
}