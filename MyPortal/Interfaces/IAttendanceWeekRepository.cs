using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Models.Database;

namespace MyPortal.Interfaces
{
    public interface IAttendanceWeekRepository : IReadWriteRepository<AttendanceWeek>
    {
        Task<AttendanceWeek> GetByDate(int academicYearId, DateTime date);
    }
}
