using System;
using System.Runtime.Serialization;

namespace EXCSLA.Shared.Core.Exceptions
{
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

        protected FileNameMalformedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}