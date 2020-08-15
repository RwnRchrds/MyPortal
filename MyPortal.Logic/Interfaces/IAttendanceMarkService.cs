using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Logic.Models.Attendance;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.List;

namespace MyPortal.Logic.Interfaces
{
    public interface IAttendanceMarkService : IService
    {
        Task<AttendanceSummary> GetSummaryByStudent(Guid studentId, Guid academicYearId, bool asPercentage);
        Task<AttendanceMarkModel> Get(Guid studentId, Guid attendanceWeekId, Guid periodId);
        Task Save(params AttendanceMarkListModel[] marks);
        Task Delete(params Guid[] attendanceMarkIds);
        Task Save(params StudentRegisterMarkCollection[] markCollections);
    }
}
