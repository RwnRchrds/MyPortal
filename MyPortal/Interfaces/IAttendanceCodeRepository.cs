using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using MyPortal.Models.Database;

namespace MyPortal.Interfaces
{
    public interface IAttendanceCodeRepository : IReadRepository<AttendanceCode>
    {
        Task<AttendanceCode> Get(string code);
        Task<IEnumerable<AttendanceCode>> GetUsable();

    }
}