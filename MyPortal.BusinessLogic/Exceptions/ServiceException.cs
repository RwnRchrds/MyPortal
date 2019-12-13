using System;

namespace MyPortal.BusinessLogic.Exceptions
{
    public enum ExceptionType
    {
        BadRequest,
        NotFound,
        Forbidden,
        Conflict
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