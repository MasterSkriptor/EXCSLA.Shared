using System;
using System.Runtime.Serialization;

namespace EXCSLA.Shared.Core.Exceptions
{
    [Serializable]
    public class PhoneNumberOutOfBoundsException : Exception
    {
        public PhoneNumberOutOfBoundsException()
        {
        }

        public PhoneNumberOutOfBoundsException(string message) : base(message)
        {
        }

        public PhoneNumberOutOfBoundsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PhoneNumberOutOfBoundsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}