using System;
using System.Collections.Generic;
using System.Text;

namespace MyPortal.Database.Exceptions
{
    public class SystemEntityException : Exception
    {
        public SystemEntityException(string message) : base(message)
        {

        }
    }
}
