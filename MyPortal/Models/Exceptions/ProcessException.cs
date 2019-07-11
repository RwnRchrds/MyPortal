using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPortal.Models.Exceptions
{
    public enum ExceptionType
    {
        BadRequest,
        NotFound
    }
    public class ProcessException : Exception
    {
        public ExceptionType ExceptionType { get; set; }

        public ProcessException(string message, ExceptionType exceptionType) : base(message)
        {
            ExceptionType = exceptionType;
        }
    }
}