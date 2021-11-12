using System;
using System.Collections.Generic;
using System.Text;

namespace MyPortal.Logic.Exceptions
{
    public class ConfigurationException : LogicException
    {
        public ConfigurationException(string message) : base(message)
        {
        }
    }
}
