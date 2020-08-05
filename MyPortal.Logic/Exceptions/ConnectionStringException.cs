using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace MyPortal.Logic.Exceptions
{
    public class ConnectionStringException : Exception
    {
        public ConnectionStringException()
        {
        }

        protected ConnectionStringException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public ConnectionStringException(string? message) : base(message)
        {
        }

        public ConnectionStringException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
