using System;

namespace MyPortal.Exceptions
{
    public enum ExceptionType
    {
        BadRequest,
        NotFound,
        Forbidden,
        Conflict
    }
    
    public class ProcessException : Exception
    {
        public ExceptionType ExceptionType { get; set; }

        public ProcessException(ExceptionType exceptionType, string message) : base(message)
        {
            ExceptionType = exceptionType;
        }
    }
}