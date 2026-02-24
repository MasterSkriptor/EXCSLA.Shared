using Xunit;
using Ardalis.GuardClauses;
using EXCSLA.Shared.Domain.Exceptions;
using EXCSLA.Shared.Domain.Tests.Builders;
using EXCSLA.Shared.Domain.Tests.BaseTestObjects;
using System;
using System.Collections.Generic;
using EXCSLA.Shared.Domain;

namespace EXCSLA.Shared.Domain.Tests
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
				Guard.Against.MinMaxLengthGuard(shortString, 3, 10);
			});
		}

		// ...existing code...
	}
}
