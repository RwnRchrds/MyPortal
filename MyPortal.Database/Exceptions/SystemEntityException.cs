using System;

namespace MyPortal.Database.Exceptions
{
    public class SystemEntityException : Exception
    {
        public SystemEntityException(string message) : base(message)
        {
        }
    }
}