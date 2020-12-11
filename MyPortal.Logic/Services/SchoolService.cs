using System.Threading.Tasks;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Repositories;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;

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

        public override void Dispose()
        {
            _schoolRepository.Dispose();
        }
    }
}