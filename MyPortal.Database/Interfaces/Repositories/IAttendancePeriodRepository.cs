using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Models.QueryResults.Attendance;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IAttendancePeriodRepository : IReadWriteRepository<AttendancePeriod>,
        IUpdateRepository<AttendancePeriod>
    {
        Task<IEnumerable<AttendancePeriodInstance>> GetInstancesByDateRange(DateTime dateFrom, DateTime dateTo);
        Task<AttendancePeriodInstance> GetInstanceByPeriodId(Guid attendanceWeekId, Guid periodId);
    }
}
