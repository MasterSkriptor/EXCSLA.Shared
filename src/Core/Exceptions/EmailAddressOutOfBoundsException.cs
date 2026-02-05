using System;
using System.Runtime.Serialization;

namespace EXCSLA.Shared.Core.Exceptions
{
    [Serializable]
    public class EmailAddressOutOfBoundsException : Exception
    {
        public EmailAddressOutOfBoundsException()
        {
        }

        public EmailAddressOutOfBoundsException(string message) : base(message)
        {
        }

        public EmailAddressOutOfBoundsException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}