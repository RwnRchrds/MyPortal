using System.Threading.Tasks;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces.Services;

namespace MyPortal.Logic.Services
{
    public class SchoolService : BaseService, ISchoolService
    {
        public async Task<string> GetLocalSchoolName()
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var localSchoolName = await unitOfWork.Schools.GetLocalSchoolName();

                return localSchoolName;
            }
        }
    }
}