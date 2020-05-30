using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Database.Models;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Interfaces
{
    public interface IAttendanceMarkRepository : IReadWriteRepository<AttendanceMark>
    {
        Task<IEnumerable<AttendanceMark>> GetByStudent(Guid studentId, Guid academicYearId);
        Task<AttendanceMark> Get(Guid studentId, Guid attendanceWeekId, Guid periodId);
        Task Update(AttendanceMark mark);
    }
}
