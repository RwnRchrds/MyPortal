using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IAttendanceCodeRepository : IReadWriteRepository<AttendanceCode>, IUpdateRepository<AttendanceCode>
    {
        Task<AttendanceCode> GetByCode(string code);
        Task<IEnumerable<AttendanceCode>> GetAll(bool activeOnly, bool includeRestricted);
    }
}