using System;
using System.Threading.Tasks;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface ISystemSettingRepository : IDisposable
    {
        Task<SystemSetting> Get(string name);
        Task<SystemSetting> GetWithTracking(string name);
        Task SaveChanges();
    }
}
