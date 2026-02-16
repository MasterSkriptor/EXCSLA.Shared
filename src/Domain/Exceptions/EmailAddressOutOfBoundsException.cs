using System;
using System.Runtime.Serialization;

namespace EXCSLA.Shared.Domain.Exceptions;

[Serializable]
public class EmailAddressOutOfBoundsException : Exception
{
    public EmailAddressOutOfBoundsException() : base()
    {
    }

    public EmailAddressOutOfBoundsException(string message) : base(message)
    {
    }

    public EmailAddressOutOfBoundsException(string message, Exception innerException) : base(message, innerException)
    {
    }
}