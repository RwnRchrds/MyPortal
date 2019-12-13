using System.Threading.Tasks;
using MyPortal.Data.Models;

namespace MyPortal.Data.Interfaces
{
    public interface ISchoolRepository : IReadWriteRepository<School>
    {
        Task<School> GetLocal();
    }
}
