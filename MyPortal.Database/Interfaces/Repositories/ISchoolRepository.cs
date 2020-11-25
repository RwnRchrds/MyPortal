using System.Threading.Tasks;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface ISchoolRepository : IReadWriteRepository<School>
    {
        Task<string> GetLocalSchoolName();
        Task<School> GetLocal();
    }
}
