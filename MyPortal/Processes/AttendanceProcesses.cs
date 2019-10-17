using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MyPortal.Dtos;
using MyPortal.Dtos.LiteDtos;
using MyPortal.Models.Database;
using MyPortal.Models.Exceptions;
using MyPortal.Models.Misc;

namespace MyPortal.Processes
{
    public static class AttendanceProcesses
    {
        public static async Task CreateAttendanceWeeksForAcademicYear(int academicYearId,
            MyPortalDbContext context)
        {
            var academicYear = await context.CurriculumAcademicYears.SingleOrDefaultAsync(x => x.Id == academicYearId);

            if (academicYear == null)
            {
                throw new ProcessException(ExceptionType.NotFound,"Academic year not found");
            }

            var pointer = academicYear.FirstDate;
            while (pointer < academicYear.LastDate)
            {
                var weekBeginning = pointer.StartOfWeek();

                if (!await context.AttendanceWeeks.AnyAsync(x => x.Beginning == weekBeginning))
                {
                    var attendanceWeek = new AttendanceWeek()
                    {
                        AcademicYearId = academicYear.Id,
                        Beginning = weekBeginning
                    };

                    context.AttendanceWeeks.Add(attendanceWeek);
                }

                pointer = weekBeginning.AddDays(7);
            }

            await context.SaveChangesAsync();
        }

        public static async Task<IEnumerable<AttendancePeriodDto>> GetAllPeriods(MyPortalDbContext context)
        {
            var attendancePeriods = await context.AttendancePeriods.OrderBy(x => x.Weekday).ThenBy(x => x.StartTime)
                .ToListAsync();

            return attendancePeriods.Select(Mapper.Map<AttendancePeriod, AttendancePeriodDto>);
        }

        public static async Task<AttendanceMark> GetAttendanceMark(MyPortalDbContext context, int attendanceWeekId, int periodId, int studentId)
        {
            var mark = await context.AttendanceMarks.SingleOrDefaultAsync(x =>
                x.PeriodId == periodId && x.WeekId == attendanceWeekId && x.StudentId == studentId);

            var verifyValues = await context.AttendanceWeeks.AnyAsync(x => x.Id == attendanceWeekId) &&
                               await context.AttendancePeriods.AnyAsync(x => x.Id == periodId) &&
                               await context.Students.AnyAsync(x => x.Id == studentId);

            if (mark == null)
            {
                if (!verifyValues)
                {
                    return null;
                }

                mark = new AttendanceMark
                {
                    Mark = "-",
                    WeekId = attendanceWeekId,
                    PeriodId = periodId,
                    StudentId = studentId,
                };
            }

            return mark;
        }

        public static async Task<AttendanceMeaning> GetMeaning(MyPortalDbContext context, string mark)
        {
            var codeInDb = await context.AttendanceCodes.SingleOrDefaultAsync(x => x.Code == mark);

            if (codeInDb == null)
            {
                throw new ProcessException(ExceptionType.NotFound,"Attendance code not found");
            }

            return codeInDb.Meaning;
        }

        public static async Task<AttendancePeriodDto> GetPeriodById(int periodId, MyPortalDbContext context)
        {
            var period = await context.AttendancePeriods.SingleOrDefaultAsync(x => x.Id == periodId);

            if (period == null)
            {
                throw new ProcessException(ExceptionType.NotFound,"Period not found");
            }

            return Mapper.Map<AttendancePeriod, AttendancePeriodDto>(period);
        }

        public static async Task<DateTime> GetPeriodDate(int weekId, int periodId, MyPortalDbContext context)
        {
            var week = await context.AttendanceWeeks.SingleOrDefaultAsync(x => x.Id == weekId);

            if (week == null)
            {
                throw new ProcessException(ExceptionType.NotFound,"Attendance week not found");
            }

            var period = await context.AttendancePeriods.SingleOrDefaultAsync(x => x.Id == periodId);

            if (period == null)
            {
                throw new ProcessException(ExceptionType.NotFound,"");
            }

            return week.Beginning.GetDayOfWeek(period.Weekday);
        }

        public static string GetPeriodTime(AttendancePeriod period)
        {
            var startTime = period.StartTime.ToString(@"hh\:mm");
            var endTime = period.EndTime.ToString(@"hh\:mm");

            var periodTime = $"{startTime} - {endTime}";

            return periodTime;
        }

        public static async Task<IEnumerable<StudentAttendanceMarkCollection>> GetRegisterMarks(int academicYearId, int weekId,
            int sessionId, MyPortalDbContext context, bool retrieveWholeDay)
        {

            if (!await context.AttendanceWeeks.AnyAsync(x => x.Id == weekId))
            {
                throw new ProcessException(ExceptionType.NotFound,"Attendance week not found");
            }

            var session = await context.CurriculumSessions.SingleOrDefaultAsync(x => x.Id == sessionId);

            if (session == null)
            {
                throw new ProcessException(ExceptionType.NotFound,"Session not found");
            }

            var markList = new List<StudentAttendanceMarkCollection>();

            foreach (var enrolment in session.Class.Enrolments)
            {
                var markObject = new StudentAttendanceMarkCollection();
                markObject.StudentName = PeopleProcesses.GetStudentDisplayName(enrolment.Student).ResponseObject;
                var marks = new List<AttendanceMark>();

                var mark = await GetAttendanceMark(context, weekId, session.PeriodId, enrolment.StudentId);

                marks.Add(mark);

                if (retrieveWholeDay)
                {
                    var periodsInDay = context.AttendancePeriods.Where(x => x.Weekday == session.Period.Weekday).ToList();

                    foreach (var period in periodsInDay)
                    {
                        mark = await GetAttendanceMark(context, weekId, session.PeriodId, enrolment.StudentId);

                        marks.Add(mark);
                    }
                }

                var liteMarks = await PrepareLiteMarkList(context, marks, true);

                markObject.Marks = liteMarks.ToList();
                markList.Add(markObject);
            }

            return markList.ToList().OrderBy(x => x.StudentName);
        }

        public static async Task<AttendanceSummary> GetSummary(int studentId, int academicYearId, MyPortalDbContext context, bool asPercentage = false)
        {
            var marksForStudent = await context.AttendanceMarks.Where(x =>
                x.Week.AcademicYearId == academicYearId && x.StudentId == studentId).ToListAsync();

            if (!marksForStudent.Any())
            {
                return null;
            }

            var summary = new AttendanceSummary();

            foreach (var mark in marksForStudent)
            {
                var meaning = await GetMeaning(context, mark.Mark);

                if (meaning != null)
                {
                    switch (meaning.Code)
                    {
                        case "P":
                            summary.Present++;
                            break;
                        case "AA":
                            summary.AuthorisedAbsence++;
                            break;
                        case "AEA":
                            summary.ApprovedEdActivity++;
                            break;
                        case "UA":
                            summary.UnauthorisedAbsence++;
                            break;
                        case "ANR":
                            summary.NotRequired++;
                            break;
                        case "L":
                            summary.Late++;
                            break;
                    }
                }
            }

            if (asPercentage)
            {
                summary.ConvertToPercentage();
            }

            return summary;
        }

        public static async Task<AttendanceWeekDto> GetWeekByDate(int academicYearId, DateTime date, MyPortalDbContext context)
        {
            var weekBeginning = date.StartOfWeek();

            var selectedWeek = await context.AttendanceWeeks.SingleOrDefaultAsync(x =>
                x.Beginning == weekBeginning && x.AcademicYearId == academicYearId);

            if (selectedWeek == null)
            {
               throw new ProcessException(ExceptionType.NotFound,"Attendance week not found");
            }

            return Mapper.Map<AttendanceWeek, AttendanceWeekDto>(selectedWeek);
        }

        public static async Task<IEnumerable<AttendanceMarkLite>> PrepareLiteMarkList(MyPortalDbContext context, List<AttendanceMark> marks, bool retrieveMeanings)
        {
            var liteMarks = marks.OrderBy(x => x.Period.StartTime)
                .Select(Mapper.Map<AttendanceMark, AttendanceMarkLite>).ToList();

            if (retrieveMeanings)
            {
                foreach (var mark in liteMarks)
                {
                    var meaning = await GetMeaning(context, mark.Mark);
                    mark.MeaningCode = meaning.Code;
                }
            }

            return liteMarks;
        }

        public static async Task SaveRegisterMarks(IEnumerable<StudentAttendanceMarkCollection> markCollections,
            MyPortalDbContext context)
        {
            foreach (var collection in markCollections)
            {
                foreach (var mark in collection.Marks)
                {
                    if (mark.Id == 0)
                    {
                        var attMark = new AttendanceMark
                        {
                            Mark = mark.Mark,
                            PeriodId = mark.PeriodId,
                            WeekId = mark.WeekId,
                            StudentId = mark.StudentId
                        };

                        context.AttendanceMarks.Add(attMark);
                    }

                    else
                    {
                        var markInDb = await context.AttendanceMarks.SingleOrDefaultAsync(x => x.Id == mark.Id);

                        if (markInDb == null)
                        {
                            throw new ProcessException(ExceptionType.NotFound,"Attendance mark not found");
                        }

                        markInDb.Mark = mark.Mark;
                    }
                }
            }

            await context.SaveChangesAsync();
        }
    }
}