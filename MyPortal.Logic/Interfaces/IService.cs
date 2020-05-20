using System;
using System.Collections.Generic;
using System.Text;
using MyPortal.Logic.Models.Exceptions;

namespace MyPortal.Logic.Interfaces
{
    public interface IService : IDisposable
    {
        ServiceException NotFound(string message = null);
        ServiceException BadRequest(string message = null);
        ServiceException BadRequest(Exception ex);
        ServiceException Forbidden(string message = null);
    }
}
