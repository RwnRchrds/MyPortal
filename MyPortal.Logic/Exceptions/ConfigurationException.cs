using System;
using System.Collections.Generic;
using System.Text;

namespace MyPortal.Logic.Exceptions
{
    public class ConfigurationException : Exception
    {
        public ConfigurationException()
        {
        }

        public ConfigurationException(string? message) : base(message)
        {
        }
    }
}
