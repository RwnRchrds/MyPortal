using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPortal.Models.Exceptions
{
    public class PersonNotFreeException : Exception
    {
        public PersonNotFreeException(string message) : base(message)
        {
        }
    }
}