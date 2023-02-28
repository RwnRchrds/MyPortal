using System;

namespace MyPortal.Logic.Exceptions;

public class YearLockedException : Exception
{
    public YearLockedException()
    {
    }

    public YearLockedException(string message) : base(message)
    {
    }
}