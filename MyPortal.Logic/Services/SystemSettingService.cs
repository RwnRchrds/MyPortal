using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Repositories;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Services
{
    public class SystemSettingService : BaseService, ISystemSettingService
    {
        private ISystemSettingRepository _systemSettingRepository;

        public SystemSettingService(ISystemSettingRepository systemSettingRepository)
        {
            _systemSettingRepository = systemSettingRepository;
        }

        public override void Dispose()
        {
            _systemSettingRepository.Dispose();
        }

        public async Task SetValue(string name, string value)
        {
            var setting = await _systemSettingRepository.GetWithTracking(name);

            if (setting == null)
            {
                throw new NotFoundException("Setting not found.");
            }

            setting.Setting = value;

            await _systemSettingRepository.SaveChanges();
        }

        public async Task<int> GetDatabaseVersion()
        {
            var databaseVersion = await _systemSettingRepository.Get("DatabaseVersion");

            if (databaseVersion == null)
            {
                throw new NotFoundException("Database version not found.");
            }

            return Convert.ToInt32(databaseVersion.Setting);
        }
    }
}
