using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace EXCSLA.Shared.Core.Exceptions
{
    public class MaximumLengthExceededException : Exception
    {
        public MaximumLengthExceededException()
        {
        }

        public MaximumLengthExceededException(string message) : base(message)
        {
        }

        public MaximumLengthExceededException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
