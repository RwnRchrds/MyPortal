using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.ListModels;
using MyPortal.Logic.Models.Requests.Attendance;

namespace MyPortal.Logic.Interfaces
{
    public interface IAttendanceMarkService : IService
    {
        Task<AttendanceSummary> GetSummaryByStudent(Guid studentId, Guid academicYearId, bool asPercentage);
        Task<AttendanceMarkModel> Get(Guid studentId, Guid attendanceWeekId, Guid periodId);
        Task Save(params AttendanceMarkListModel[] marks);
        Task Delete(params Guid[] attendanceMarkIds);
        Task Save(params StudentAttendanceMarkCollection[] markCollections);
    }
}
