using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Models.QueryResults.Attendance;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IAttendanceMarkRepository : IReadWriteRepository<AttendanceMark>, IUpdateRepository<AttendanceMark>
    {
        Task<IEnumerable<AttendanceMark>> GetByStudent(Guid studentId, Guid academicYearId);
        Task<AttendanceMark> GetMark(Guid studentId, Guid attendanceWeekId, Guid periodId);

        Task<IEnumerable<AttendanceMarkDetailModel>> GetRegisterMarks(Guid studentGroupId,
            AttendancePeriodInstance[] attendancePeriods);

        Task<IEnumerable<PossibleAttendanceMark>> GetPossibleMarksByStudentGroup(Guid studentGroupId,
            IEnumerable<AttendancePeriodInstance> attendancePeriods);
    }
}
