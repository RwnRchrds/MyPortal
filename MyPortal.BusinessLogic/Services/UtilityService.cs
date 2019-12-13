using System;

namespace MyPortal.BusinessLogic.Services
{
    public class UtilityService : IDisposable
    {
        public string GenerateId()
        {
            return Guid.NewGuid().ToString("N");
        }

        public void Dispose()
        {
            
        }
    }
}