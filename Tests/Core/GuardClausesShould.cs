using Xunit;
using Ardalis.GuardClauses;
using EXCSLA.Shared.Core.Exceptions;
using EXCSLA.Shared.Tests.Core.UnitTests.Builders;
using EXCSLA.Shared.Tests.Core.UnitTests.BaseTestObjects;
using System;
using System.Collections.Generic;
using EXCSLA.Shared.Core;

namespace EXCSLA.Shared.Tests.Core.UnitTests
{
    /// <summary>
    /// Comprehensive test suite for Guard Clauses covering string length validation,
    /// duplicate detection, and error conditions.
    /// </summary>
    public class GuardClausesShould
    {
        [Fact]
        public void MinLengthGuard_DoesNotThrow_WhenLengthIsValid()
        {
            var validString = "Hello";

            // Should not throw
            Guard.Against.MinLengthGuard(validString, 3);
        }

        [Fact]
        public void MinLengthGuard_Throws_WhenLengthIsTooShort()
        {
            var shortString = "Hi";

            Assert.Throws<MinimumLengthExceededException>(() =>
            {
                Guard.Against.MinLengthGuard(shortString, 5);
            });
        }

        [Fact]
        public void MinLengthGuard_Throws_WithCorrectMessage()
        {
            var shortString = "Hi";
            int minLength = 10;

            var exception = Assert.Throws<MinimumLengthExceededException>(() =>
            {
                Guard.Against.MinLengthGuard(shortString, minLength);
            });

            Assert.Contains(minLength.ToString(), exception.Message);
        }

        [Fact]
        public void MaxLengthGuard_DoesNotThrow_WhenLengthIsValid()
        {
            var validString = "Hello";

            // Should not throw
            Guard.Against.MaxLengthGuard(validString, 10);
        }

        [Fact]
        public void MaxLengthGuard_Throws_WhenLengthIsTooLong()
        {
            var longString = "This is a very long string that exceeds the maximum";

            Assert.Throws<MaximumLengthExceededException>(() =>
            {
                Guard.Against.MaxLengthGuard(longString, 10);
            });
        }

        [Fact]
        public void MaxLengthGuard_Throws_WithCorrectMessage()
        {
            var longString = "This is too long";
            int maxLength = 5;

            var exception = Assert.Throws<MaximumLengthExceededException>(() =>
            {
                Guard.Against.MaxLengthGuard(longString, maxLength);
            });

            Assert.Contains(maxLength.ToString(), exception.Message);
        }

        [Fact]
        public void MinMaxLengthGuard_DoesNotThrow_WhenLengthIsInRange()
        {
            var validString = "Hello";

            // Should not throw
            Guard.Against.MinMaxLengthGuard(validString, 3, 10);
        }

        [Fact]
        public void MinMaxLengthGuard_Throws_WhenLengthIsTooShort()
        {
            var shortString = "Hi";

            Assert.Throws<MinimumLengthExceededException>(() =>
            {
                Guard.Against.MinMaxLengthGuard(shortString, 5, 20);
            });
        }

        [Fact]
        public void MinMaxLengthGuard_Throws_WhenLengthIsTooLong()
        {
            var longString = "This is a very long string that exceeds maximum";

            Assert.Throws<MaximumLengthExceededException>(() =>
            {
                Guard.Against.MinMaxLengthGuard(longString, 5, 20);
            });
        }

        [Fact]
        public void MinMaxLengthGuard_AllowsMinimumBoundary()
        {
            var boundaryString = "12345"; // Exactly min length

            // Should not throw
            Guard.Against.MinMaxLengthGuard(boundaryString, 5, 10);
        }

        [Fact]
        public void MinMaxLengthGuard_AllowsMaximumBoundary()
        {
            var boundaryString = "1234567890"; // Exactly max length

            // Should not throw
            Guard.Against.MinMaxLengthGuard(boundaryString, 5, 10);
        }

        [Fact]
        public void DuplicateInListGuard_DoesNotThrow_WhenNoDuplicate()
        {
            var entity1 = new BaseEntityBuilder(1, "John", "Doe").Build();
            var entity2 = new BaseEntityBuilder(2, "Jane", "Doe").Build();
            var list = new List<TestIntBaseEntity> { entity1 };

            // Should not throw
            Guard.Against.DuplicateInList<TestIntBaseEntity, int>(entity2, list);
        }

        [Fact]
        public void DuplicateInListGuard_Throws_WhenDuplicateExists()
        {
            var entity1 = new BaseEntityBuilder(1, "John", "Doe").Build();
            var entity2 = new BaseEntityBuilder(1, "John", "Doe").Build(); // Same ID
            var list = new List<TestIntBaseEntity> { entity1 };

            Assert.Throws<ItemIsDuplicateException>(() =>
            {
                Guard.Against.DuplicateInList<TestIntBaseEntity, int>(entity2, list);
            });
        }

        [Fact]
        public void DuplicateInListGuard_WorksWithMultipleItems()
        {
            var entity1 = new BaseEntityBuilder(1, "John", "Doe").Build();
            var entity2 = new BaseEntityBuilder(2, "Jane", "Doe").Build();
            var entity3 = new BaseEntityBuilder(3, "Bob", "Smith").Build();
            var entity4 = new BaseEntityBuilder(4, "Alice", "Johnson").Build();
            var entity5 = new BaseEntityBuilder(2, "Different", "Name").Build(); // ID 2 duplicate

            var list = new List<TestIntBaseEntity> { entity1, entity2, entity3, entity4 };

            Assert.Throws<ItemIsDuplicateException>(() =>
            {
                Guard.Against.DuplicateInList<TestIntBaseEntity, int>(entity5, list);
            });
        }

        [Fact]
        public void DuplicateInListGuard_EmptyList_NeverThrows()
        {
            var entity = new BaseEntityBuilder(1, "Test", "User").Build();
            var emptyList = new List<TestIntBaseEntity>();

            // Should not throw
            Guard.Against.DuplicateInList<TestIntBaseEntity, int>(entity, emptyList);
        }

        [Fact]
        public void GuardClause_MinLength_EmptyString_Throws()
        {
            Assert.Throws<MinimumLengthExceededException>(() =>
            {
                Guard.Against.MinLengthGuard("", 1);
            });
        }

        [Fact]
        public void GuardClause_MaxLength_EmptyString_DoesNotThrow()
        {
            // Should not throw for empty string with max length >= 0
            Guard.Against.MaxLengthGuard("", 1);
        }

        [Fact]
        public void GuardClause_CanChainMultipleChecks()
        {
            var validString = "ValidString";

            // Should not throw
            Guard.Against.MinLengthGuard(validString, 5);
            Guard.Against.MaxLengthGuard(validString, 50);
            Guard.Against.MinMaxLengthGuard(validString, 5, 50);
        }

        [Fact]
        public void GuardClause_ThrowsImmediately_OnFirstViolation()
        {
            var shortString = "Hi";

            // Only MinimumLengthExceededException should be thrown, not both
            Assert.Throws<MinimumLengthExceededException>(() =>
            {
                Guard.Against.MinMaxLengthGuard(shortString, 5, 20);
            });
        }
    }
}
