using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MyPortal.Dtos.LiteDtos;
using MyPortal.Models.Database;
using MyPortal.Models.Exceptions;
using MyPortal.Models.Misc;

namespace MyPortal.Processes
{
    public static class AttendanceProcesses
    {

        static AttendanceProcesses()
        {
                       
        }        

        public static bool VerifyAttendanceCodes(this MyPortalDbContext context, ListContainer<AttendanceRegisterMark> register)
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

            return codesVerified;
        }

        public static AttendanceRegisterMark GetAttendanceMark(MyPortalDbContext context, AttendanceWeek attendanceWeek, AttendancePeriod period, Student student)
        {
            var mark = context.AttendanceMarks.SingleOrDefault(x =>
                x.PeriodId == period.Id && x.WeekId == attendanceWeek.Id && x.StudentId == student.Id);

            if (mark == null)
            {                
                if (student == null || period == null || attendanceWeek == null)
                {
                    return null;
                }

                mark = new AttendanceRegisterMark
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

            return mark;
        }

        public static IEnumerable<AttendanceRegisterMarkLite> PrepareLiteMarkList(MyPortalDbContext context, List<AttendanceRegisterMark> marks, bool retrieveMeanings)
        {
            var liteMarks = marks.OrderBy(x => x.AttendancePeriod.StartTime)
                .Select(Mapper.Map<AttendanceRegisterMark, AttendanceRegisterMarkLite>).ToList();

            foreach (var mark in liteMarks)
            {
                var meaning = GetMeaning(context, mark.Mark);

                if (meaning == null)
                {
                    continue;
                }

                if (retrieveMeanings)
                {
                    mark.MeaningCode = meaning.Code;
                }                
            }

            return liteMarks;
        }

        public static AttendanceRegisterCodeMeaning GetMeaning(MyPortalDbContext context, string mark)
        {
            var codeInDb = context.AttendanceCodes.SingleOrDefault(x => x.Code == mark);

            return codeInDb?.AttendanceRegisterCodeMeaning;
        }

        public static AttendanceSummary GetSummary(MyPortalDbContext context, int studentId, int academicYearId, bool asPercentage = false)
        {
            var marksForStudent = context.AttendanceMarks.Where(x =>
                x.AttendanceWeek.AcademicYearId == academicYearId && x.StudentId == studentId);

            if (!marksForStudent.Any())
            {
                throw new BadRequestException("No attendance data.");
            }

            var summary = new AttendanceSummary();

            foreach (var mark in marksForStudent)
            {
                var meaning = GetMeaning(context, mark.Mark);

                if (meaning == null)
                {
                    throw new EntityNotFoundException("Attendance meaning not found for code [" + mark.Mark + "]");
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

            return summary;
        }

        #region Extension Methods
       
        #endregion
    }
}