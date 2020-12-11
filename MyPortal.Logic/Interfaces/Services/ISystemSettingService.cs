using System;
using System.Threading.Tasks;

namespace MyPortal.Logic.Interfaces.Services
{
    public interface ISystemSettingService : IDisposable
    {
        public Task<int> GetDatabaseVersion();
    }
}
