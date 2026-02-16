using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace EXCSLA.Shared.Domain.Exceptions;

/// <summary>
/// Exception thrown when a value exceeds its maximum allowed length.
/// Commonly used in value objects and domain validations.
/// </summary>
public class MaximumLengthExceededException : Exception
{
    /// <summary>
    /// Initializes a new instance of the MaximumLengthExceededException class.
    /// </summary>
    public MaximumLengthExceededException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the MaximumLengthExceededException class with a message.
    /// </summary>
    /// <param name="message">The exception message describing the validation error.</param>
    public MaximumLengthExceededException(string message) : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the MaximumLengthExceededException class with a message and inner exception.
    /// </summary>
    /// <param name="message">The exception message describing the validation error.</param>
    /// <param name="innerException">The inner exception that caused this exception.</param>
    public MaximumLengthExceededException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
