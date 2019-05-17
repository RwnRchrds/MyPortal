using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPortal.Models.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public string Reason { get; set; }

        public EntityNotFoundException(string reason)
        {
            Reason = reason;
        }
    }
}