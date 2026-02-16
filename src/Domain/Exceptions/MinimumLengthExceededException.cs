using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace EXCSLA.Shared.Domain.Exceptions;

public class MinimumLengthExceededException : Exception
{
    public MinimumLengthExceededException()
    {
    }

    public MinimumLengthExceededException(string message) : base(message)
    {
    }

    public MinimumLengthExceededException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
