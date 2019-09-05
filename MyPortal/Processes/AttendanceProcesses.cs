using System;
using System.Collections.Generic;
using System.Linq;
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
        public static ProcessResponse<bool> VerifyAttendanceCodes(this MyPortalDbContext context, ListContainer<AttendanceMark> register)
        {
            var codesVerified = true;
            var codes = context.AttendanceCodes.ToList();

            foreach (var mark in register.Objects)
            {
                if (!codesVerified)
                {
                    break;
                }

                if (codes.All(x => x.Code != mark.Mark))
                {
                    codesVerified = false;
                }
            }

            return new ProcessResponse<bool>(ResponseType.Ok, null, codesVerified);
        }

        public static ProcessResponse<AttendanceMark> GetAttendanceMark(MyPortalDbContext context, AttendanceWeek attendanceWeek, AttendancePeriod period, Student student)
        {
            var mark = context.AttendanceMarks.SingleOrDefault(x =>
                x.PeriodId == period.Id && x.WeekId == attendanceWeek.Id && x.StudentId == student.Id);

            if (mark == null)
            {                
                if (student == null || period == null || attendanceWeek == null)
                {
                    return null;
                }

                mark = new AttendanceMark
                {
                    Student = student,
                    Mark = "-",
                    WeekId = attendanceWeek.Id,
                    AttendanceWeek = attendanceWeek,
                    PeriodId = period.Id,
                    StudentId = student.Id,
                    AttendancePeriod = period
                };
            }

            return new ProcessResponse<AttendanceMark>(ResponseType.Ok, null, mark);
        }

        public static IEnumerable<AttendanceMarkLite> PrepareLiteMarkList(MyPortalDbContext context, List<AttendanceMark> marks, bool retrieveMeanings)
        {
            var liteMarks = marks.OrderBy(x => x.AttendancePeriod.StartTime)
                .Select(Mapper.Map<AttendanceMark, AttendanceMarkLite>).ToList();

            if (retrieveMeanings)
            {
                foreach (var mark in liteMarks)
                {
                    mark.MeaningCode = GetMeaning(context, mark.Mark).ResponseObject.Code;
                }
            }

            return liteMarks;
        }

        public static ProcessResponse<AttendanceMeaning> GetMeaning(MyPortalDbContext context, string mark)
        {
            var codeInDb = context.AttendanceCodes.SingleOrDefault(x => x.Code == mark);

            if (codeInDb == null)
            {
                return new ProcessResponse<AttendanceMeaning>(ResponseType.NotFound, "Attendance code not found", null);
            }
            
            return new ProcessResponse<AttendanceMeaning>(ResponseType.Ok, null, codeInDb.AttendanceMeaning);
        }

        public static ProcessResponse<AttendanceSummary> GetSummary(int studentId, int academicYearId, MyPortalDbContext context, bool asPercentage = false)
        {
            var marksForStudent = context.AttendanceMarks.Where(x =>
                x.AttendanceWeek.AcademicYearId == academicYearId && x.StudentId == studentId);

            if (!marksForStudent.Any())
            {
                return new ProcessResponse<AttendanceSummary>(ResponseType.NotFound, "No attendance data", null);
            }

            var summary = new AttendanceSummary();

            foreach (var mark in marksForStudent)
            {
                var meaning = GetMeaning(context, mark.Mark).ResponseObject;

                if (meaning == null)
                {
                    continue;
                }

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

            if (asPercentage)
            {
                summary.ConvertToPercentage();
            }

            return new ProcessResponse<AttendanceSummary>(ResponseType.Ok, null, summary);
        }

        public static ProcessResponse<IEnumerable<StudentAttendanceMarkCollection>> GetRegisterMarks(int academicYearId, int weekId,
            int sessionId, MyPortalDbContext context, bool retrieveWholeDay)
        {
            var attendanceWeek =
                context.AttendanceWeeks.SingleOrDefault(x => x.Id == weekId && x.AcademicYearId == academicYearId);

            if (attendanceWeek == null)
            {
                return new ProcessResponse<IEnumerable<StudentAttendanceMarkCollection>>(ResponseType.NotFound, "Attendance week not found", null);
            }

            var session = context.CurriculumSessions.SingleOrDefault(x => x.Id == sessionId);

            if (session == null)
            {
                return new ProcessResponse<IEnumerable<StudentAttendanceMarkCollection>>(ResponseType.NotFound, "Period not found", null);
            }

            var markList = new List<StudentAttendanceMarkCollection>();

            foreach (var enrolment in session.CurriculumClass.Enrolments)
            {
                var markObject = new StudentAttendanceMarkCollection();
                var student = enrolment.Student;
                markObject.StudentName = PeopleProcesses.GetStudentDisplayName(enrolment.Student).ResponseObject;
                var marks = new List<AttendanceMark>();

                var mark = GetAttendanceMark(context, attendanceWeek, session.AttendancePeriod, student).ResponseObject;

                marks.Add(mark);

                if (retrieveWholeDay)
                {
                    var periodsInDay = context.AttendancePeriods.Where(x => x.Weekday == session.AttendancePeriod.Weekday).ToList();

                    foreach (var period in periodsInDay)
                    {
                        mark = GetAttendanceMark(context, attendanceWeek, period, student).ResponseObject;

                        marks.Add(mark);
                    }
                }

                marks = marks.OrderBy(x => x.AttendancePeriod.StartTime).ToList();

                var liteMarks = PrepareLiteMarkList(context, marks, true);

                markObject.Marks = liteMarks.ToList();
                markList.Add(markObject);
            }

            return new ProcessResponse<IEnumerable<StudentAttendanceMarkCollection>>(ResponseType.Ok, null,
                markList.ToList().OrderBy(x => x.StudentName));
        }

        public static ProcessResponse<object> SaveRegisterMarks(IEnumerable<StudentAttendanceMarkSingular> markCollection,
            MyPortalDbContext context)
        {
            foreach (var mark in markCollection)
            {
                if (mark.Mark.Id == 0)
                {
                    var attMark = new AttendanceMark
                    {
                        Mark = mark.Mark.Mark,
                        PeriodId = mark.Mark.PeriodId,
                        WeekId = mark.Mark.WeekId,
                        StudentId = mark.Mark.StudentId
                    };

                    context.AttendanceMarks.Add(attMark);
                }

                else
                {
                    var markInDb = context.AttendanceMarks.SingleOrDefault(x => x.Id == mark.Mark.Id);

                    if (markInDb == null)
                    {
                        return new ProcessResponse<object>(ResponseType.NotFound,
                            "An attendance mark could not be found", null);
                    }

                    markInDb.Mark = mark.Mark.Mark;
                }
            }

            context.SaveChanges();
            return new ProcessResponse<object>(ResponseType.Ok, "Register saved", null);
        }

        public static ProcessResponse<IEnumerable<AttendancePeriodDto>> GetAllPeriods(MyPortalDbContext context)
        {
            var dayIndex = new List<string> {"Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun"};
            return new ProcessResponse<IEnumerable<AttendancePeriodDto>>(ResponseType.Ok, null,
                context.AttendancePeriods.ToList().OrderBy(x => dayIndex.IndexOf(x.Weekday))
                    .ThenBy(x => x.StartTime).Select(Mapper.Map<AttendancePeriod, AttendancePeriodDto>));
        }

        public static ProcessResponse<AttendancePeriodDto> GetPeriodById(int periodId, MyPortalDbContext context)
        {
            var period = context.AttendancePeriods.SingleOrDefault(x => x.Id == periodId);

            if (period == null)
            {
                return new ProcessResponse<AttendancePeriodDto>(ResponseType.NotFound, "Period not found", null);
            }

            return new ProcessResponse<AttendancePeriodDto>(ResponseType.Ok, null,
                Mapper.Map<AttendancePeriod, AttendancePeriodDto>(period));
        }

        public static ProcessResponse<object> CreateAttendanceWeeksForAcademicYear(int academicYearId,
            MyPortalDbContext context)
        {
            var academicYear = context.CurriculumAcademicYears.SingleOrDefault(x => x.Id == academicYearId);

            if (academicYear == null)
            {
                return new ProcessResponse<object>(ResponseType.NotFound, "Academic year not found", null);
            }

            var pointer = academicYear.FirstDate;
            while (pointer < academicYear.LastDate)
            {
                var weekBeginning = pointer.StartOfWeek();

                if (context.AttendanceWeeks.Any(x => x.Beginning == weekBeginning))
                {
                    continue;
                }

                var attendanceWeek = new AttendanceWeek()
                {
                    AcademicYearId = academicYear.Id,
                    Beginning = weekBeginning
                };

                context.AttendanceWeeks.Add(attendanceWeek);
                
                pointer = weekBeginning.AddDays(7);
            }

            context.SaveChanges();

            return new ProcessResponse<object>(ResponseType.Ok, "Attendance weeks added for academic year", null);
        }

        public static ProcessResponse<AttendanceWeekDto> GetWeekByDate(int academicYearId, DateTime date, MyPortalDbContext context)
        {
            var weekBeginning = date.StartOfWeek();

            var selectedWeek = context.AttendanceWeeks.SingleOrDefault(x => x.Beginning == weekBeginning && x.AcademicYearId == academicYearId);

            if (selectedWeek == null)
            {
                return new ProcessResponse<AttendanceWeekDto>(ResponseType.NotFound, "Attendance week not found", null);
            }

            return new ProcessResponse<AttendanceWeekDto>(ResponseType.Ok, null,
                Mapper.Map<AttendanceWeek, AttendanceWeekDto>(selectedWeek));
        }

        public static ProcessResponse<string> GetPeriodTime(AttendancePeriod period)
        {
            var startTime = period.StartTime.ToString(@"hh\:mm");
            var endTime = period.EndTime.ToString(@"hh\:mm");

            var periodTime = $"{startTime} - {endTime}";

            return new ProcessResponse<string>(ResponseType.Ok, null, periodTime);
        }

        public static ProcessResponse<DateTime> GetPeriodDate(int weekId, int periodId, MyPortalDbContext context)
        {
            var week = context.AttendanceWeeks.SingleOrDefault(x => x.Id == weekId);

            if (week == null)
            {
                return new ProcessResponse<DateTime>(ResponseType.NotFound, "Week not found", new DateTime());
            }

            var period = context.AttendancePeriods.SingleOrDefault(x => x.Id == periodId);

            if (period == null)
            {
                return new ProcessResponse<DateTime>(ResponseType.NotFound, "Period not found", new DateTime());
            }

            var periodDay = DayOfWeek.Monday;

            switch (period.Weekday)
            {
                case "Tue":
                    periodDay = DayOfWeek.Tuesday;
                    break;
                case "Wed":
                    periodDay = DayOfWeek.Wednesday;
                    break;
                case "Thu":
                    periodDay = DayOfWeek.Thursday;
                    break;
                case "Fri":
                    periodDay = DayOfWeek.Friday;
                    break;
                case "Sat":
                    periodDay = DayOfWeek.Saturday;
                    break;
                case "Sun":
                    periodDay = DayOfWeek.Sunday;
                    break;
            }

            return new ProcessResponse<DateTime>(ResponseType.Ok, null, week.Beginning.GetDayOfWeek(periodDay));
        }
    }
}