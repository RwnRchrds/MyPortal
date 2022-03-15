using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Database.Exceptions;

namespace MyPortal.Database.Helpers
{
    internal class ExceptionHelper
    {
        #region System Entities

        internal static SystemEntityException UpdateSystemEntityException => new("System entities cannot be modified.");

        internal static SystemEntityException DeleteSystemEntityException => new("System entities cannot be deleted.");

        #endregion
    }
}
