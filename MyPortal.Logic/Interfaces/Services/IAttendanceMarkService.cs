using System;
using System.Threading.Tasks;
using MyPortal.Logic.Models.DataGrid;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Requests.Attendance;

namespace MyPortal.Logic.Interfaces.Services
{
    public interface IAttendanceMarkService : IService
    {
        Task<AttendanceSummary> GetSummaryByStudent(Guid studentId, Guid academicYearId);
        Task<AttendanceMarkModel> Get(Guid studentId, Guid attendanceWeekId, Guid periodId);
        Task Save(params AttendanceMarkListModel[] marks);
        Task Delete(params Guid[] attendanceMarkIds);
        Task Save(params StudentRegisterMarkCollection[] markCollections);
    }
}
