using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IAttendancePeriodRepository : IReadWriteRepository<AttendancePeriod>,
        IUpdateRepository<AttendancePeriod>
    {
        Task<IEnumerable<AttendancePeriod>> GetByWeekday(DayOfWeek weekday);
    }
}
