using System;
using System.Runtime.Serialization;

namespace EXCSLA.Shared.Domain.Exceptions;

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
}