using System;
using System.Threading.Tasks;
using MyPortal.Data.Models;

namespace MyPortal.Data.Interfaces
{
    public interface IAttendanceWeekRepository : IReadWriteRepository<AttendanceWeek>
    {
        Task<AttendanceWeek> GetByWeekBeginning(int academicYearId, DateTime date);
    }
}
