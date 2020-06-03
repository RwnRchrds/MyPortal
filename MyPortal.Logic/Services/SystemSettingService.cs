using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Database.Interfaces;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces;

namespace MyPortal.Logic.Services
{
    public class SystemSettingService : BaseService, ISystemSettingService
    {
        private ISystemSettingRepository _systemSettingRepository;

        public SystemSettingService(ISystemSettingRepository systemSettingRepository) : base("Setting")
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
                throw NotFound();
            }

            setting.Setting = value;

            await _systemSettingRepository.SaveChanges();
        }

        public async Task<string> GetLicenceNumber()
        {
            var licenceNumber = await _systemSettingRepository.Get("LicenceNumber");

            if (licenceNumber == null)
            {
                throw NotFound("Licence number not found.");
            }

            return licenceNumber.Setting;
        }

        public async Task<bool> IsConfigured()
        {
            var isConfigured = await _systemSettingRepository.Get("InitialSetup");

            if (isConfigured == null)
            {
                throw NotFound();
            }

            return Convert.ToBoolean(isConfigured.Setting);
        }

        public async Task<int> GetDatabaseVersion()
        {
            var databaseVersion = await _systemSettingRepository.Get("DatabaseVersion");

            if (databaseVersion == null)
            {
                throw NotFound("Database version not found.");
            }

            return Convert.ToInt32(databaseVersion.Setting);
        }
    }
}
