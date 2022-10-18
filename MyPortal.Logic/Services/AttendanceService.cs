﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MyPortal.Database;
using MyPortal.Database.Constants;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Models.QueryResults.Attendance;
using MyPortal.Database.Models.Search;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Extensions;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Reporting;
using MyPortal.Logic.Models.Requests.Attendance;
using MyPortal.Logic.Models.Response.Attendance;
using MyPortal.Logic.Models.Response.Attendance.Register;
using MyPortal.Logic.Models.Summary;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Services
{
    public class AttendanceService : BaseService, IAttendanceService
    {
        public async Task<AttendanceMarkModel> GetAttendanceMark(Guid studentId, Guid attendanceWeekId, Guid periodId,
            bool returnNoMark = false)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var attendanceMark = await unitOfWork.AttendanceMarks.GetMark(studentId, attendanceWeekId, periodId);

                if (returnNoMark && attendanceMark == null)
                {
                    return AttendanceMarkModel.NoMark(studentId, attendanceWeekId, periodId);
                }

                return new AttendanceMarkModel(attendanceMark);
            }
        }

        public async Task<AttendanceRegisterModel> GetRegisterBySession(Guid attendanceWeekId, Guid sessionId)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var metadata = await unitOfWork.Sessions.GetMetadata(sessionId, attendanceWeekId);

                if (metadata == null)
                {
                    throw new NotFoundException("Session not found.");
                }

                var title =
                    $"Take Register - {metadata.PeriodName} {metadata.StartTime.Date: dd/MM/yyyy} ({metadata.ClassCode})";

                if (metadata.TeacherId.HasValue)
                {
                    title += $" - {metadata.TeacherName}";
                }

                return await GetRegisterByDateRange(metadata.StudentGroupId, metadata.StartTime, metadata.EndTime,
                    metadata.PeriodId, title);
            }
        }

        public async Task<IEnumerable<AttendanceRegisterSummaryModel>> GetRegisters(RegisterSearchRequestModel model)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var searchOptions = new RegisterSearchOptions
                {
                    DateFrom = model.Date.Date,
                    DateTo = model.Date.GetEndOfDay(),
                    PeriodId = model.PeriodId,
                    TeacherId = model.TeacherId
                };
                
                var sessions = await unitOfWork.Sessions.GetMetadata(searchOptions);

                return sessions.Select(s => new AttendanceRegisterSummaryModel(s)).ToArray();
            }
        }

        public async Task<AttendanceRegisterModel> GetRegisterByDateRange(Guid studentGroupId, DateTime dateFrom, DateTime dateTo, Guid? lockToPeriodId = null, string title = null)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var studentGroup = await unitOfWork.StudentGroups.GetById(studentGroupId);

                if (studentGroup == null)
                {
                    throw new NotFoundException("Student group not found.");
                }
                
                var register = new AttendanceRegisterModel();

                register.Title = string.IsNullOrWhiteSpace(title) ?
                    $"Edit Marks - {studentGroup.Description}, {dateFrom:dd/MM/yyyy}-{dateTo:dd/MM/yyyy}" : title;

                var periods = (await unitOfWork.AttendancePeriods.GetByDateRange(dateFrom.Date, dateTo.GetEndOfDay())).ToArray();
                
                register.PopulateColumnGroups(periods, lockToPeriodId);

                var codes = (await unitOfWork.AttendanceCodes.GetAll()).Select(c => new AttendanceCodeModel(c))
                    .ToArray();

                register.Codes = codes;

                var marks = await unitOfWork.AttendanceMarks.GetRegisterMarks(studentGroupId, periods);
                
                register.PopulateMarks(marks);

                return register;
            }
        }

        public async Task UpdateAttendanceMarks(params AttendanceMarkSummaryModel[] marks)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                foreach (var model in marks)
                {
                    Validate(model);

                    await AcademicHelper.IsAcademicYearLockedByWeek(model.WeekId, true);

                    if (model.CodeId == Guid.Empty)
                    {
                        throw new AttendanceCodeException("Cannot insert blank attendance codes.");
                    }

                    var markInDb = await GetAttendanceMark(model.StudentId, model.WeekId, model.PeriodId);

                    if (markInDb != null && markInDb.Id.HasValue)
                    {
                        markInDb.CodeId = model.CodeId.Value;
                        markInDb.MinutesLate = model.MinutesLate ?? 0;
                        markInDb.Comments = model.Comments;

                        var updatedMark = new AttendanceMark
                        {
                            Id = markInDb.Id.Value,
                            CodeId = markInDb.CodeId,
                            StudentId = markInDb.StudentId,
                            WeekId = markInDb.WeekId,
                            PeriodId = markInDb.PeriodId,
                            MinutesLate = markInDb.MinutesLate,
                            Comments = markInDb.Comments
                        };

                        await unitOfWork.AttendanceMarks.Update(updatedMark);
                    }
                    else
                    {
                        var mark = new AttendanceMark
                        {
                            StudentId = model.StudentId,
                            WeekId = model.WeekId,
                            PeriodId = model.PeriodId,
                            CodeId = model.CodeId.Value,
                            MinutesLate = model.MinutesLate ?? 0,
                            Comments = model.Comments
                        };

                        unitOfWork.AttendanceMarks.Create(mark);
                    }
                }

                await unitOfWork.SaveChangesAsync();
            }
        }

        public async Task DeleteAttendanceMarks(params Guid[] attendanceMarkIds)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                foreach (var attendanceMarkId in attendanceMarkIds)
                {
                    await unitOfWork.AttendanceMarks.Delete(attendanceMarkId);
                }

                await unitOfWork.SaveChangesAsync();
            }
        }

        public async Task UpdateAttendanceMarks(params AttendanceRegisterStudentModel[] markCollections)
        {
            var attendanceMarks = new List<AttendanceMarkSummaryModel>();

            foreach (var collection in markCollections)
            {
                attendanceMarks.AddRange(collection.Marks);
            }

            await UpdateAttendanceMarks(attendanceMarks.ToArray());
        }

        public async Task<AttendanceSummary> GetAttendanceSummaryByStudent(Guid studentId, Guid academicYearId)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var codes = (await unitOfWork.AttendanceCodes.GetAll()).Select(c => new AttendanceCodeModel(c))
                    .ToList();

                var marks =
                    (await unitOfWork.AttendanceMarks.GetByStudent(studentId, academicYearId)).Select(m => new AttendanceMarkModel(m)).ToList();

                var summary = new AttendanceSummary(codes, marks);

                return summary;
            }
        }
        
        public async Task<AttendancePeriodModel> GetPeriodById(Guid periodId)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var period = await unitOfWork.AttendancePeriods.GetById(periodId);

                if (period == null)
                {
                    throw new NotFoundException("Period not found.");
                }

                return new AttendancePeriodModel(period);
            }
        }
        
        public async Task<AttendanceWeekModel> GetWeekById(Guid attendanceWeekId)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var attendanceWeek = await unitOfWork.AttendanceWeeks.GetById(attendanceWeekId);

                if (attendanceWeek == null)
                {
                    throw new NotFoundException("Attendance week not found.");
                }

                return new AttendanceWeekModel(attendanceWeek);
            }
        }

        public async Task<AttendanceWeekModel> GetWeekByDate(DateTime date, bool throwIfNotFound = true)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var week = await unitOfWork.AttendanceWeeks.GetByDate(date);

                if (week == null && throwIfNotFound)
                {
                    throw new NotFoundException("Attendance week not found.");
                }

                return new AttendanceWeekModel(week);
            }
        }
    }
}
