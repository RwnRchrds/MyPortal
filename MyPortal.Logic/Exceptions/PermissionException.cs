using System;
using System.Collections.Generic;
using System.Text;

namespace MyPortal.Logic.Exceptions
{
    public class PermissionException : Exception
    {
        public PermissionException(string message) : base(message)
        {
        }

        public PermissionException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
