using System.Threading.Tasks;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Repositories;
using MyPortal.Logic.Interfaces;

namespace MyPortal.Logic.Services
{
    public class SchoolService : BaseService, ISchoolService
    {
        private readonly ISchoolRepository _schoolRepository;

        public SchoolService(ApplicationDbContext context)
        {
            _schoolRepository = new SchoolRepository(context);
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