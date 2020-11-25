using System.Threading.Tasks;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IAttendanceCodeRepository : IReadRepository<AttendanceCode>
    {
        Task<AttendanceCode> GetByCode(string code);
    }
}
