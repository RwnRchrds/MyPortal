using System;
using System.Threading.Tasks;
using MyPortal.Database.Models;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IAttendanceWeekRepository : IReadWriteRepository<AttendanceWeek>
    {
        Task<AttendanceWeek> GetByDate(DateTime date);
    }
}
