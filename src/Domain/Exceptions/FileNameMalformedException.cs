using System;
using System.Runtime.Serialization;

namespace EXCSLA.Shared.Domain.Exceptions;

[Serializable]
public class FileNameMalformedException : Exception
{
    public FileNameMalformedException()
    {
    }

    public FileNameMalformedException(string message) : base(message)
    {
    }

    public FileNameMalformedException(string message, Exception innerException) : base(message, innerException)
    {
    }
}