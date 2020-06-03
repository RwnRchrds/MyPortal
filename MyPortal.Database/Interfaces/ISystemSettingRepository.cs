using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Database.Models;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Interfaces
{
    public interface ISystemSettingRepository : IDisposable
    {
        Task<SystemSetting> Get(string name);
        Task<SystemSetting> GetWithTracking(string name);
        Task SaveChanges();
    }
}
