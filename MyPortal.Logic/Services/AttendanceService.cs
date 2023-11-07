using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Models.Search;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Data.Attendance;
using MyPortal.Logic.Models.Data.Attendance.Register;
using MyPortal.Logic.Models.Reporting;
using MyPortal.Logic.Models.Requests.Attendance;
using MyPortal.Logic.Models.Summary;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Services
{
    public class AttendanceService : BaseService, IAttendanceService
    {
        public AttendanceService(ISessionUser user) : base(user)
        {
        }

        public async Task<AttendanceMarkModel> GetAttendanceMark(Guid studentId, Guid attendanceWeekId, Guid periodId)
        {
            await using var unitOfWork = await User.GetConnection();
            var attendanceMark = await unitOfWork.AttendanceMarks.GetMark(studentId, attendanceWeekId, periodId);

            if (attendanceMark == null)
            {
                return null;
            }

            return new AttendanceMarkModel(attendanceMark);
        }

        public async Task<AttendanceRegisterDataModel> GetRegisterBySession(Guid attendanceWeekId, Guid sessionId,
            Guid periodId)
        {
            await using var unitOfWork = await User.GetConnection();

            var metadata = (await unitOfWork.SessionPeriods.GetPeriodDetailsBySession(sessionId, attendanceWeekId))
                .ToArray();

            if (metadata == null)
            {
                throw new NotFoundException("Session not found.");
            }

            var registerPeriod = metadata.FirstOrDefault(p => p.PeriodId == periodId);

            if (registerPeriod == null)
            {
                throw new NotFoundException("Session was not found for this period.");
            }

            var title =
                $"{registerPeriod.PeriodName} {registerPeriod.StartTime.Date: dd/MM/yyyy} ({registerPeriod.ClassCode})";

            if (registerPeriod.TeacherId.HasValue)
            {
                title += $" - {registerPeriod.TeacherName}";
            }

            var studentsInSession =
                await unitOfWork.StudentGroupMemberships.GetMembershipsByGroup(registerPeriod.StudentGroupId,
                    registerPeriod.StartTime, registerPeriod.EndTime);

            var extraNames = Array.Empty<SessionExtraName>();

            if (registerPeriod.SessionId.HasValue)
            {
                extraNames =
                    (await unitOfWork.SessionExtraNames.GetExtraNamesBySession(registerPeriod.SessionId.Value,
                        registerPeriod.AttendanceWeekId)).ToArray();
            }

            var studentIds = studentsInSession.Select(s => s.StudentId)
                .Union(extraNames.Select(n => n.StudentId)).ToArray();

            var register = await GetRegisterByDateRange(studentIds, registerPeriod.StartTime, registerPeriod.EndTime,
                title,
                metadata.Select(m => m.PeriodId).ToArray());

            register.FlagExtraNames(extraNames);

            return register;
        }

        public async Task<AttendanceRegisterDataModel> GetRegisterByStudentGroup(Guid studentGroupId,
            Guid attendanceWeekId, Guid periodId)
        {
            await using var unitOfWork = await User.GetConnection();

            var period = await unitOfWork.AttendancePeriods.GetInstanceByPeriodId(attendanceWeekId, periodId);

            if (period == null)
            {
                throw new NotFoundException("The attendance period was not found.");
            }

            return await GetRegisterByDateRange(studentGroupId, period.ActualStartTime.Date, period.ActualEndTime.Date);
        }

        public async Task<IEnumerable<AttendanceRegisterSummaryModel>> GetRegisters(RegisterSearchRequestModel model)
        {
            await using var unitOfWork = await User.GetConnection();

            var searchOptions = new RegisterSearchOptions
            {
                DateFrom = model.Date.Date,
                DateTo = model.Date.Date,
                PeriodId = model.PeriodId,
                TeacherId = model.TeacherId
            };

            var sessions = await unitOfWork.SessionPeriods.SearchPeriodDetails(searchOptions);

            return sessions.Select(s => new AttendanceRegisterSummaryModel(s)).ToArray();
        }

        public async Task<AttendanceRegisterDataModel> GetRegisterByDateRange(IEnumerable<Guid> studentIds,
            DateTime dateFrom, DateTime dateTo, string title, Guid[] unlockedPeriods = null)
        {
            await using var unitOfWork = await User.GetConnection();

            var studentCollection = studentIds.ToArray();

            var register = await InitialiseRegister(dateFrom, dateTo, title, unlockedPeriods);

            var existingMarks =
                await unitOfWork.AttendanceMarks.GetRegisterMarks(studentCollection, register.Periods);

            var possibleMarks =
                await unitOfWork.AttendanceMarks.GetPossibleMarksByStudents(studentCollection, register.Periods);

            register.PopulateMarks(existingMarks);
            register.PopulateMissingMarks(possibleMarks);

            return register;
        }

        private async Task<AttendanceRegisterDataModel> InitialiseRegister(DateTime dateFrom, DateTime dateTo,
            string title, Guid[] unlockedPeriods = null)
        {
            await using var unitOfWork = await User.GetConnection();

            var register = new AttendanceRegisterDataModel();

            register.Title = title;

            var periods = (await unitOfWork.AttendancePeriods
                .GetInstancesByDateRange(dateFrom.Date, dateTo.Date)).ToArray();

            register.Periods = periods;

            register.PopulateColumnGroups(periods, unlockedPeriods);

            var codes = (await unitOfWork.AttendanceCodes.GetAll())
                .Select(c => new AttendanceCodeModel(c))
                .ToArray();

            register.Codes = codes;

            return register;
        }

        public async Task<AttendanceRegisterDataModel> GetRegisterByDateRange(Guid studentGroupId, DateTime dateFrom,
            DateTime dateTo, string title = null, Guid[] unlockedPeriods = null)
        {
            await using var unitOfWork = await User.GetConnection();

            var studentGroup = await unitOfWork.StudentGroups.GetById(studentGroupId);

            if (studentGroup == null)
            {
                throw new NotFoundException("Student group not found.");
            }

            var registerTitle = string.IsNullOrWhiteSpace(title)
                ? $"{studentGroup.Description}, {dateFrom:dd/MM/yyyy}-{dateTo:dd/MM/yyyy}"
                : title;

            var register = await InitialiseRegister(dateFrom, dateTo, registerTitle, unlockedPeriods);

            // Get any attendance marks that already exist
            var existingMarks = await unitOfWork.AttendanceMarks
                .GetRegisterMarks(studentGroupId, register.Periods);

            // Get possible attendance mark "slots" (whether or not an actual mark exists yet in the system)
            var possibleMarks =
                await unitOfWork.AttendanceMarks.GetPossibleMarksByStudentGroup(studentGroupId, register.Periods);

            register.PopulateMarks(existingMarks);
            register.PopulateMissingMarks(possibleMarks);

            return register;
        }

        public async Task UpdateAttendanceMarks(params AttendanceMarkSummaryModel[] marks)
        {
            await using var unitOfWork = await User.GetConnection();

            foreach (var model in marks)
            {
                Validate(model);

                var academicYearService = new AcademicYearService(User);
                await academicYearService.IsAcademicYearLockedByWeek(model.WeekId);

                if (!model.CodeId.HasValue || model.CodeId == Guid.Empty)
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
                        Id = Guid.NewGuid(),
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

        public async Task DeleteAttendanceMarks(params Guid[] attendanceMarkIds)
        {
            await using var unitOfWork = await User.GetConnection();

            foreach (var attendanceMarkId in attendanceMarkIds)
            {
                await unitOfWork.AttendanceMarks.Delete(attendanceMarkId);
            }

            await unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAttendanceMarks(params AttendanceRegisterStudentDataModel[] markCollections)
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
            await using var unitOfWork = await User.GetConnection();

            var codes = (await unitOfWork.AttendanceCodes.GetAll())
                .Select(c => new AttendanceCodeModel(c))
                .ToList();

            var marks =
                (await unitOfWork.AttendanceMarks.GetByStudent(studentId, academicYearId))
                .Select(m => new AttendanceMarkModel(m)).ToList();

            var summary = new AttendanceSummary(codes, marks);

            return summary;
        }

        public async Task<AttendancePeriodModel> GetPeriodById(Guid periodId)
        {
            await using var unitOfWork = await User.GetConnection();

            var period = await unitOfWork.AttendancePeriods.GetById(periodId);

            if (period == null)
            {
                throw new NotFoundException("Period not found.");
            }

            return new AttendancePeriodModel(period);
        }

        public async Task<AttendanceWeekModel> GetWeekById(Guid attendanceWeekId)
        {
            await using var unitOfWork = await User.GetConnection();

            var attendanceWeek = await unitOfWork.AttendanceWeeks.GetById(attendanceWeekId);

            if (attendanceWeek == null)
            {
                throw new NotFoundException("Attendance week not found.");
            }

            return new AttendanceWeekModel(attendanceWeek);
        }

        public async Task<AttendanceWeekModel> GetWeekByDate(DateTime date, bool throwIfNotFound = true)
        {
            await using var unitOfWork = await User.GetConnection();

            var week = await unitOfWork.AttendanceWeeks.GetByDate(date);

            if (week == null && throwIfNotFound)
            {
                throw new NotFoundException("Attendance week not found.");
            }

            return new AttendanceWeekModel(week);
        }

        public async Task AddExtraName(ExtraNameRequestModel model)
        {
            Validate(model);

            await using var unitOfWork = await User.GetConnection();

            var existingNames =
                await unitOfWork.SessionExtraNames.GetExtraNamesBySession(model.SessionId, model.AttendanceWeekId);

            if (existingNames.Any(n => n.StudentId == model.StudentId))
            {
                throw new LogicException("The student has already been added to this session.");
            }

            var extraName = new SessionExtraName
            {
                Id = Guid.NewGuid(),
                AttendanceWeekId = model.AttendanceWeekId,
                SessionId = model.SessionId,
                StudentId = model.StudentId
            };

            unitOfWork.SessionExtraNames.Create(extraName);

            await unitOfWork.SaveChangesAsync();
        }

        public async Task RemoveExtraName(Guid extraNameId)
        {
            await using var unitOfWork = await User.GetConnection();

            await unitOfWork.SessionExtraNames.Delete(extraNameId);

            await unitOfWork.SaveChangesAsync();
        }
    }
}