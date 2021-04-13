using System;
using System.Collections.Generic;
using System.Text;

namespace MyPortal.Database.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException()
        {
        }

        public EntityNotFoundException(string message) : base(message)
        {
        }
    }
}
