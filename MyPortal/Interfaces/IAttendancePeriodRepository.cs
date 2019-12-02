using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Models.Database;

namespace MyPortal.Interfaces
{
    public interface IAttendancePeriodRepository : IReadWriteRepository<AttendancePeriod>
    {
        Task<IEnumerable<AttendancePeriod>> GetByDayOfWeek(DayOfWeek weekDay);
        Task<IEnumerable<AttendancePeriod>> GetByClass(int classId);
        Task<IEnumerable<AttendancePeriod>> GetRegPeriods();
    }
}
