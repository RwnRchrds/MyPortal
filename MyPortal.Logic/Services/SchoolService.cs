using System.Threading.Tasks;
using MyPortal.Database.Interfaces;
using MyPortal.Logic.Interfaces;

namespace MyPortal.Logic.Services
{
    public class SchoolService : BaseService, ISchoolService
    {
        private readonly ISchoolRepository _schoolRepository;

        public SchoolService(ISchoolRepository schoolRepository)
        {
            _schoolRepository = schoolRepository;
        }
        
        public async Task<string> GetLocalSchoolName()
        {
            return await _schoolRepository.GetLocalSchoolName();
        }
    }
}