using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPortal.Models.Exceptions
{
    public class BadRequestException : Exception
    {
        public string Reason { get; set; }

        public BadRequestException(string reason)
        {
            Reason = reason;
        }
    }
}