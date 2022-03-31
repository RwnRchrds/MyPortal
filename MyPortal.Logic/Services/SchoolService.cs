using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Entity;

namespace MyPortal.Logic.Services
{
    public class SchoolService : BaseService, ISchoolService
    {
        private async Task<string> GetLocalSchoolNameFromDb()
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var localSchoolName = await unitOfWork.Schools.GetLocalSchoolName();

                return localSchoolName;
            }
        }

        public async Task<string> GetLocalSchoolName()
        {
            var localSchoolName =
                await CacheHelper.StringCache.GetOrCreate(CacheKeys.LocalSchoolName, GetLocalSchoolNameFromDb);

            return localSchoolName;
        }
    }
}