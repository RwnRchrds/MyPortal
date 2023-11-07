using System.Threading.Tasks;
using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface ISchoolRepository : IReadWriteRepository<School>, IUpdateRepository<School>
    {
        Task<string> GetLocalSchoolName();
        Task<School> GetLocal();
    }
}