using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Data.Models;

namespace MyPortal.Data.Interfaces
{
    public interface IAttendanceCodeRepository : IReadRepository<AttendanceCode>
    {
        Task<AttendanceCode> Get(string code);
        Task<IEnumerable<AttendanceCode>> GetUsable();

    }
}