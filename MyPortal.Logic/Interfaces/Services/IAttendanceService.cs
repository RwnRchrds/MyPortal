using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Models.Search;
using MyPortal.Logic.Models.Data.Attendance;
using MyPortal.Logic.Models.Data.Attendance.Register;

using MyPortal.Logic.Models.Reporting;
using MyPortal.Logic.Models.Requests.Attendance;
using MyPortal.Logic.Models.Summary;

namespace MyPortal.Logic.Interfaces.Services
{
    public interface IAttendanceService : IService
    {
        Task<AttendanceSummary> GetAttendanceSummaryByStudent(Guid studentId, Guid academicYearId);
        Task<AttendanceMarkModel> GetAttendanceMark(Guid studentId, Guid attendanceWeekId, Guid periodId);
        Task<IEnumerable<AttendanceRegisterSummaryModel>> GetRegisters(RegisterSearchRequestModel model);
        Task<AttendanceRegisterDataModel> GetRegisterBySession(Guid attendanceWeekId, Guid sessionId);
        Task<AttendanceRegisterDataModel> GetRegisterByDateRange(Guid studentGroupId, DateTime dateFrom, DateTime dateTo,
            string title = null, Guid? lockToPeriodId = null);
        Task<AttendanceRegisterDataModel> GetRegisterByDateRange(IEnumerable<Guid> studentIds,
            DateTime dateFrom, DateTime dateTo, string title, Guid? lockToPeriodId = null);
        Task UpdateAttendanceMarks(params AttendanceMarkSummaryModel[] marks);
        Task UpdateAttendanceMarks(params AttendanceRegisterStudentDataModel[] markCollections);
        Task DeleteAttendanceMarks(params Guid[] attendanceMarkIds);
        Task<AttendancePeriodModel> GetPeriodById(Guid periodId);
        Task<AttendanceWeekModel> GetWeekById(Guid attendanceWeekId);
        Task<AttendanceWeekModel> GetWeekByDate(DateTime date, bool throwIfNotFound = true);
        Task AddExtraName(ExtraNameRequestModel model);
        Task RemoveExtraName(Guid extraNameId);
    }
}
