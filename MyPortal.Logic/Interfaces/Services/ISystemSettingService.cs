using System;
using System.Threading.Tasks;

namespace MyPortal.Logic.Interfaces.Services
{
    public interface ISystemSettingService : IService
    {
        public Task<int> GetDatabaseVersion();
    }
}
