using System;
using System.Threading.Tasks;

namespace MyPortal.Logic.Interfaces.Services
{
    public interface ISystemSettingService
    {
        public Task<int> GetDatabaseVersion();
    }
}
