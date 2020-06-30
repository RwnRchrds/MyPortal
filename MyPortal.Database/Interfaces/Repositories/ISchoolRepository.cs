using System.Threading.Tasks;
using MyPortal.Database.Models;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface ISchoolRepository : IReadWriteRepository<School>
    {
        Task<string> GetLocalSchoolName();
        Task<School> GetLocal();
    }
}
