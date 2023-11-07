using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IAttendanceWeekRepository : IReadWriteRepository<AttendanceWeek>, IUpdateRepository<AttendanceWeek>
    {
        Task<AttendanceWeek> GetByDate(DateTime date);
        Task<IEnumerable<AttendanceWeek>> GetByDateRange(DateTime startDate, DateTime endDate);
    }
}