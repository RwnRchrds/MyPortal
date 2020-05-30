using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Logic.Models.Requests.Attendance;

namespace MyPortal.Logic.Interfaces
{
    public interface IAttendanceMarkService : IService
    {
        Task<AttendanceSummary> GetSummaryByStudent(Guid studentId, Guid academicYearId, bool asPercentage);
    }
}
