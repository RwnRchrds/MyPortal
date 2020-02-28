using System.Threading.Tasks;
using MyPortal.Database.Interfaces;
using MyPortal.Logic.Interfaces;

namespace MyPortal.Logic.Services
{
    public class SchoolService : BaseService, ISchoolService
    {
        private readonly ISchoolRepository _repository;

        public SchoolService(ISchoolRepository repository)
        {
            _repository = repository;
        }
        
        public async Task<string> GetLocalSchoolName()
        {
            return await _repository.GetLocalSchoolName();
        }
    }
}