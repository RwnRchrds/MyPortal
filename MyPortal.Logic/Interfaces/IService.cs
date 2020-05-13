using System;
using System.Collections.Generic;
using System.Text;

namespace MyPortal.Logic.Interfaces
{
    public interface IService : IDisposable
    {
        void NotFound(string message = null);
        void BadRequest(string message = null);
        void Forbidden(string message = null);
    }
}
