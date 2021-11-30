﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Logic.Models.Collection;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Reporting;
using MyPortal.Logic.Models.Response.Attendance;

namespace MyPortal.Logic.Interfaces.Services
{
    public interface IAttendanceService
    {
        Task<AttendanceSummary> GetAttendanceSummaryByStudent(Guid studentId, Guid academicYearId);
        Task<AttendanceMarkModel> GetAttendanceMark(Guid studentId, Guid attendanceWeekId, Guid periodId, bool returnNoMark = false);
        Task<AttendanceRegisterModel> GetRegisterBySession(Guid attendanceWeekId, Guid sessionId);
        Task UpdateAttendanceMarks(params AttendanceMarkCollectionModel[] marks);
        Task Delete(params Guid[] attendanceMarkIds);
        Task<AttendancePeriodModel> GetPeriodById(Guid periodId);
        Task<AttendanceWeekModel> GetWeekById(Guid attendanceWeekId);
        Task<AttendanceWeekModel> GetWeekByDate(DateTime date, bool throwIfNotFound = true);
    }
}