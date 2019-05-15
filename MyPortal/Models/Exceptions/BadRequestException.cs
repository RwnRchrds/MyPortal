using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPortal.Models.Exceptions
{
    public class BadRequestException : Exception
    {
        public string Message { get; set; }

        public BadRequestException(string message)
        {
            Message = message;
        }
    }
}