using Xunit;
using EXCSLA.Shared.Domain.Exceptions;
using System;
using System.Threading.Tasks;

namespace EXCSLA.Shared.Domain.Tests
{
	/// <summary>
	/// Comprehensive test suite for custom exception classes covering instantiation,
	/// message handling, and exception hierarchy.
	/// </summary>
	public class CoreExceptionsShould
	{
		[Fact]
		public async Task MaximumLengthExceededException_CanBeThrown_WithoutMessage()
		{
			await Assert.ThrowsAsync<MaximumLengthExceededException>(() => throw new MaximumLengthExceededException());
		}

		[Fact]
		public async Task MaximumLengthExceededException_CanBeThrown_WithMessage()
		{
			var message = "Maximum length of 50 characters exceeded";
            
			var exception = await Assert.ThrowsAsync<MaximumLengthExceededException>(() => throw new MaximumLengthExceededException(message));

			Assert.Equal(message, exception.Message);
		}

		[Fact]
		public async Task MaximumLengthExceededException_CanBeThrown_WithInnerException()
		{
			var innerException = new ArgumentException("Inner error");
			var message = "Maximum length exceeded";

			var exception = await Assert.ThrowsAsync<MaximumLengthExceededException>(() => throw new MaximumLengthExceededException(message, innerException));

			Assert.Equal(message, exception.Message);
			Assert.Equal(innerException, exception.InnerException);
		}

		[Fact]
		public async Task MinimumLengthExceededException_CanBeThrown_WithMessage()
		{
			var message = "Minimum length of 5 characters required";

			var exception = await Assert.ThrowsAsync<MinimumLengthExceededException>(() => throw new MinimumLengthExceededException(message));

			Assert.Equal(message, exception.Message);
		}

		[Fact]
		public async Task EmailAddressOutOfBoundsException_CanBeThrown_WithMessage()
		{
			var message = "Email address is invalid";

			var exception = await Assert.ThrowsAsync<EmailAddressOutOfBoundsException>(() => throw new EmailAddressOutOfBoundsException(message));

			Assert.Equal(message, exception.Message);
		}

		[Fact]
		public async Task PhoneNumberOutOfBoundsException_CanBeThrown_WithMessage()
		{
			var message = "Phone number format is invalid";

			var exception = await Assert.ThrowsAsync<PhoneNumberOutOfBoundsException>(() => throw new PhoneNumberOutOfBoundsException(message));

			Assert.Equal(message, exception.Message);
		}

		[Fact]
		public async Task FileNameMalformedException_CanBeThrown_WithMessage()
		{
			var message = "File name contains invalid characters";

			var exception = await Assert.ThrowsAsync<FileNameMalformedException>(() => throw new FileNameMalformedException(message));

			Assert.Equal(message, exception.Message);
		}

		[Fact]
		public async Task ItemIsDuplicateException_CanBeThrown_WithMessage()
		{
			var message = "This item already exists in the collection";

			var exception = await Assert.ThrowsAsync<ItemIsDuplicateException>(() => throw new ItemIsDuplicateException(message));

			Assert.Equal(message, exception.Message);
		}

		[Fact]
		public void CustomException_InheritsFrom_Exception()
		{
			var exception = new MaximumLengthExceededException("Test");

			Assert.IsAssignableFrom<Exception>(exception);
		}

		// ...existing code...
	}
}
