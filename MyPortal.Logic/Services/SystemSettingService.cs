using System;
using System.Threading.Tasks;
using MyPortal.Database.Interfaces;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Interfaces.Services;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Services
{
    public class SystemSettingService : BaseService, ISystemSettingService
    {
        public SystemSettingService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task SetValue(string name, string value)
        {
            var setting = await UnitOfWork.SystemSettings.GetForEditing(name);

            if (setting == null)
            {
                throw new NotFoundException("Setting not found.");
            }

            setting.Setting = value;

            await UnitOfWork.SaveChanges();
        }

        public async Task<int> GetDatabaseVersion()
        {
            var databaseVersion = await UnitOfWork.SystemSettings.Get("DatabaseVersion");

            if (databaseVersion == null)
            {
                throw new NotFoundException("Database version not found.");
            }

            return Convert.ToInt32(databaseVersion.Setting);
        }
    }
}
