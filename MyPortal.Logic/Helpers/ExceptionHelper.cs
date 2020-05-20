using System;
using System.Collections.Generic;
using System.Text;

namespace MyPortal.Logic.Helpers
{
    public class ExceptionHelper
    {
        public static string GetRootExceptionMessage(Exception ex)
        {
            if (ex.InnerException != null)
            {
                return GetRootExceptionMessage(ex.InnerException);
            }

            return ex.Message;
        }
    }
}
