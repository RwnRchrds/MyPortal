﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Models.Search;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Reporting;
using MyPortal.Logic.Models.Requests.Attendance;
using MyPortal.Logic.Models.Response.Attendance;
using MyPortal.Logic.Models.Response.Attendance.Register;
using MyPortal.Logic.Models.Summary;

namespace MyPortal.Logic.Interfaces.Services
{
    public interface IAttendanceService
    {
        Task<AttendanceSummary> GetAttendanceSummaryByStudent(Guid studentId, Guid academicYearId);
        Task<AttendanceMarkModel> GetAttendanceMark(Guid studentId, Guid attendanceWeekId, Guid periodId, bool returnNoMark = false);
        Task<IEnumerable<AttendanceRegisterSummaryModel>> GetRegisters(RegisterSearchRequestModel model);
        Task<AttendanceRegisterModel> GetRegisterBySession(Guid attendanceWeekId, Guid sessionId);
        Task<AttendanceRegisterModel> GetRegisterByDateRange(Guid studentGroupId, DateTime dateFrom, DateTime dateTo,
            Guid? lockToPeriodId = null);
        Task UpdateAttendanceMarks(params AttendanceMarkSummaryModel[] marks);
        Task UpdateAttendanceMarks(params AttendanceRegisterStudentModel[] markCollections);
        Task DeleteAttendanceMarks(params Guid[] attendanceMarkIds);
        Task<AttendancePeriodModel> GetPeriodById(Guid periodId);
        Task<AttendanceWeekModel> GetWeekById(Guid attendanceWeekId);
        Task<AttendanceWeekModel> GetWeekByDate(DateTime date, bool throwIfNotFound = true);
    }
}
