using System;
using System.Collections.Generic;
using System.Text;

namespace MyPortal.Logic.Models.Exceptions
{
    public enum ExceptionType
    {
        NotFound,
        BadRequest,
        Forbidden
    }

    public class ServiceException : Exception
    {
        public ExceptionType ExceptionType { get; set; }

        public ServiceException(ExceptionType exceptionType, string message) : base(message)
        {
            ExceptionType = exceptionType;
        }
    }
}
