using System.Threading.Tasks;
using MyPortal.Database.Interfaces;
using MyPortal.Logic.Interfaces.Services;

namespace MyPortal.Logic.Services
{
    public class SchoolService : BaseService, ISchoolService
    {
        public SchoolService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<string> GetLocalSchoolName()
        {
            var localSchoolName = await UnitOfWork.Schools.GetLocalSchoolName();

            return localSchoolName;
        }
    }
}