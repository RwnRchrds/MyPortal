using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Models.Database;

namespace MyPortal.Interfaces
{
    public interface IAttendancePeriodRepository : IRepository<AttendancePeriod>
    {
        Task<IEnumerable<AttendancePeriod>> GetAllPeriods();
        Task<IEnumerable<AttendancePeriod>> GetPeriodsByDayOfWeek(DayOfWeek weekDay);
        Task<IEnumerable<AttendancePeriod>> GetPeriodsByClass(int classId);
        Task<IEnumerable<AttendancePeriod>> GetRegPeriods();
    }
}
