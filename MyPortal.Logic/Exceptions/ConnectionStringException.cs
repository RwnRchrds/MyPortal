using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace MyPortal.Logic.Exceptions
{
    public class ConnectionStringException : ConfigurationException
    {
        public ConnectionStringException(string message) : base(message)
        {
        }
    }
}
