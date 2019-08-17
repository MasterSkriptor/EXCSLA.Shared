using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace EXCSLA.Shared.Core.Exceptions
{
    [Serializable]
    public class ItemIsDuplicateException : Exception
    {
        public ItemIsDuplicateException()
        {
        }

        public ItemIsDuplicateException(string message) : base(message)
        {
        }

        public ItemIsDuplicateException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ItemIsDuplicateException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
