using System;

namespace MyPortal.Logic.Exceptions;

public class UnauthorisedException : Exception
{
    public UnauthorisedException(string message) : base(message)
    {
    }

    public UnauthorisedException(string message, Exception innerException) : base(message, innerException)
    {
    }
}