using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MyPortal.Database;
using MyPortal.Database.Constants;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Reporting;
using MyPortal.Logic.Models.Response.Attendance;
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

                if (metadata == null || metadata.AttendanceWeekId == Guid.Empty)
                {
                    throw new NotFoundException("Register not found.");
                }

                var register = new AttendanceRegisterModel(metadata);

                var codes = await unitOfWork.AttendanceCodes.GetAll(true, false);

                register.Codes = codes.Select(c => new AttendanceCodeModel(c)).ToList();

                var possibleMarks = (await unitOfWork.AttendanceMarks.GetRegisterMarks(
                    register.Metadata.CurriculumGroupId, register.Metadata.StartTime.Date,
                    register.Metadata.StartTime.Date.AddDays(1))).GroupBy(m => m.StudentId);

                foreach (var possibleMark in possibleMarks)
                {
                    var student = await unitOfWork.Students.GetById(possibleMark.Key);

                    if (student == null)
                    {
                        throw new NotFoundException("Student not found.");
                    }

                    var studentModel = new StudentModel(student);

                    var registerStudent = new AttendanceRegisterStudentModel
                    {
                        StudentId = possibleMark.Key,
                        StudentName = studentModel.Person.GetName(),
                        Marks = possibleMark.Select(m => new AttendanceMarkSummaryModel
                        {
                            StudentId = m.StudentId,
                            WeekId = m.WeekId,
                            PeriodId = m.PeriodId,
                            Comments = m.Comments,
                            MinutesLate = m.MinutesLate,
                            CodeId = m.CodeId ?? Guid.Empty
                        }).ToList()
                    };

                    register.Students.Add(registerStudent);
                }

                return register;
            }
        }

        public async Task UpdateAttendanceMarks(params AttendanceMarkSummaryModel[] marks)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                foreach (var model in marks)
                {
                    if (model.CodeId == Guid.Empty)
                    {
                        throw new AttendanceCodeException("Cannot insert blank attendance codes.");
                    }

                    var markInDb = await GetAttendanceMark(model.StudentId, model.WeekId, model.PeriodId);

                    if (markInDb != null && markInDb.Id.HasValue)
                    {
                        markInDb.CodeId = model.CodeId;
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
                            CodeId = model.CodeId,
                            MinutesLate = model.MinutesLate ?? 0,
                            Comments = model.Comments
                        };

                        unitOfWork.AttendanceMarks.Create(mark);
                    }
                }

                await unitOfWork.SaveChangesAsync();
            }
        }

        public async Task Delete(params Guid[] attendanceMarkIds)
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

        public AttendanceService(ClaimsPrincipal user) : base(user)
        {
        }
    }
}
