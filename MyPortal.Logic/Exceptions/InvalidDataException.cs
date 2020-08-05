using System;
using System.Collections.Generic;
using System.Text;

namespace MyPortal.Logic.Exceptions
{
    public class InvalidDataException : Exception
    {
        public InvalidDataException(string? message) : base(message)
        {
        }

        public InvalidDataException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
