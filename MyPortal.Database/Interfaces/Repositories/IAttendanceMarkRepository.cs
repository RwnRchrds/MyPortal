using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Models.Query.Attendance;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IAttendanceMarkRepository : IReadWriteRepository<AttendanceMark>
    {
        Task<IEnumerable<AttendanceMark>> GetByStudent(Guid studentId, Guid academicYearId);
        Task<AttendanceMark> GetMark(Guid studentId, Guid attendanceWeekId, Guid periodId);
        Task<IEnumerable<PossibleAttendanceMark>> GetRegisterMarks(Guid groupTypeId, Guid groupId, DateTime startDate, DateTime endDate);
        void Update(AttendanceMark mark);
    }
}
