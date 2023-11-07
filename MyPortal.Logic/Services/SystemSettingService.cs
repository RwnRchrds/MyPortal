using System;
using System.Threading.Tasks;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;

namespace MyPortal.Logic.Services
{
    public class SystemSettingService : BaseService, ISystemSettingService
    {
        public SystemSettingService(ISessionUser user) : base(user)
        {
        }

        public async Task SetValue(string name, string value)
        {
            await using var unitOfWork = await User.GetConnection();

            await unitOfWork.SystemSettings.Update(name, value);

            await unitOfWork.SaveChangesAsync();
        }

        public async Task<int> GetDatabaseVersion()
        {
            await using var unitOfWork = await User.GetConnection();

            var databaseVersion = await unitOfWork.SystemSettings.Get("DatabaseVersion");

            if (databaseVersion == null)
            {
                throw new NotFoundException("Database version not found.");
            }

            return Convert.ToInt32(databaseVersion.Setting);
        }
    }
}