using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MyPortal.Dtos;
using MyPortal.Dtos.LiteDtos;
using MyPortal.Exceptions;
using MyPortal.Interfaces;
using MyPortal.Models.Database;
using MyPortal.Models.Misc;

namespace MyPortal.Services
{
    public class AttendanceService : MyPortalService
    {
        public AttendanceService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public async Task CreateAttendanceWeeksForAcademicYear(int academicYearId)
        {
            var academicYear = await _unitOfWork.CurriculumAcademicYears.GetByIdAsync(academicYearId);

            if (academicYear == null)
            {
                throw new ProcessException(ExceptionType.NotFound,"Academic year not found");
            }

            var pointer = academicYear.FirstDate;
            while (pointer < academicYear.LastDate)
            {
                var weekBeginning = pointer.StartOfWeek();

                if (!await _unitOfWork.AttendanceWeeks.AnyAsync(x => x.Beginning == weekBeginning))
                {
                    var attendanceWeek = new AttendanceWeek()
                    {
                        AcademicYearId = academicYear.Id,
                        Beginning = weekBeginning
                    };

                    _unitOfWork.AttendanceWeeks.Add(attendanceWeek);
                }

                pointer = weekBeginning.AddDays(7);
            }

            await _unitOfWork.Complete();
        }

        public async Task<IEnumerable<AttendancePeriodDto>> GetAllPeriods()
        {
            var attendancePeriods = await _unitOfWork.AttendancePeriods.GetAllPeriods();

            return attendancePeriods.Select(Mapper.Map<AttendancePeriod, AttendancePeriodDto>);
        }

        public async Task<AttendanceMark> GetAttendanceMark(int attendanceWeekId, int periodId, int studentId)
        {
            var mark = await _unitOfWork.AttendanceMarks.GetAttendanceMark(studentId, attendanceWeekId, periodId) ?? new AttendanceMark
            {
                Mark = "-",
                WeekId = attendanceWeekId,
                PeriodId = periodId,
                StudentId = studentId,
            };

            return mark;
        }

        public async Task<AttendanceMeaning> GetMeaning(string mark)
        {
            var codeInDb = await _unitOfWork.AttendanceCodes.GetCode(mark);

            if (codeInDb == null)
            {
                throw new ProcessException(ExceptionType.NotFound,"Attendance code not found");
            }

            return codeInDb.Meaning;
        }

        public async Task<AttendancePeriodDto> GetPeriodById(int periodId)
        {
            var period = await _unitOfWork.AttendancePeriods.GetByIdAsync(periodId);

            if (period == null)
            {
                throw new ProcessException(ExceptionType.NotFound,"Period not found");
            }

            return Mapper.Map<AttendancePeriod, AttendancePeriodDto>(period);
        }

        public async Task<DateTime> GetPeriodDate(int weekId, int periodId)
        {
            var week = await _unitOfWork.AttendanceWeeks.GetByIdAsync(weekId);

            if (week == null)
            {
                throw new ProcessException(ExceptionType.NotFound,"Attendance week not found");
            }

            var period = await _unitOfWork.AttendancePeriods.GetByIdAsync(periodId);

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

        public async Task<IEnumerable<StudentAttendanceMarkCollection>> GetRegisterMarks(int weekId,
            int sessionId)
        {

            if (!await _unitOfWork.AttendanceWeeks.AnyAsync(x => x.Id == weekId))
            {
                throw new ProcessException(ExceptionType.NotFound,"Attendance week not found");
            }

            var session = await _unitOfWork.CurriculumSessions.GetByIdAsync(sessionId);

            if (session == null)
            {
                throw new ProcessException(ExceptionType.NotFound,"Session not found");
            }

            var markList = new List<StudentAttendanceMarkCollection>();

            foreach (var enrolment in session.Class.Enrolments)
            {
                var markObject = new StudentAttendanceMarkCollection();
                markObject.StudentName = enrolment.Student.GetDisplayName();
                var marks = new List<AttendanceMark>();

                var periodsInDay = await _unitOfWork.AttendancePeriods.GetPeriodsByDayOfWeek(session.Period.Weekday);

                foreach (var period in periodsInDay)
                {
                    var mark = await GetAttendanceMark(weekId, period.Id, enrolment.StudentId);

                    marks.Add(mark);
                }

                var liteMarks = await PrepareLiteMarkList(marks, true);

                markObject.Marks = liteMarks.ToList();
                markList.Add(markObject);
            }

            return markList.ToList().OrderBy(x => x.StudentName);
        }

        public async Task<AttendanceSummary> GetSummary(int studentId, int academicYearId, bool asPercentage = false)
        {
            var marksForStudent = await _unitOfWork.AttendanceMarks.GetAllAttendanceMarksByStudent(studentId, academicYearId);

            var marksList = marksForStudent.ToList();

            var summary = new AttendanceSummary();

            if (!marksList.Any())
            {
                return null;
            }

            foreach (var mark in marksList)
            {
                var meaning = await GetMeaning(mark.Mark);

                switch (meaning)
                {
                    case AttendanceMeaning.Present:
                        summary.Present++;
                        break;
                    case AttendanceMeaning.AuthorisedAbsence:
                        summary.AuthorisedAbsence++;
                        break;
                    case AttendanceMeaning.ApprovedEducationalActivity:
                        summary.ApprovedEdActivity++;
                        break;
                    case AttendanceMeaning.UnauthorisedAbsence:
                        summary.UnauthorisedAbsence++;
                        break;
                    case AttendanceMeaning.AttendanceNotRequired:
                        summary.NotRequired++;
                        break;
                    case AttendanceMeaning.Late:
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

        public async Task<AttendanceWeekDto> GetWeekByDate(int academicYearId, DateTime date)
        {
            var weekBeginning = date.StartOfWeek();

            var selectedWeek = await _unitOfWork.AttendanceWeeks.GetAttendanceWeekByDate(academicYearId, date);

            if (selectedWeek == null)
            {
               throw new ProcessException(ExceptionType.NotFound,"Attendance week not found");
            }

            return Mapper.Map<AttendanceWeek, AttendanceWeekDto>(selectedWeek);
        }

        public async Task<IEnumerable<AttendanceMarkLite>> PrepareLiteMarkList(List<AttendanceMark> marks, bool retrieveMeanings)
        {
            var liteMarks = marks.OrderBy(x => x.Period.StartTime)
                .Select(Mapper.Map<AttendanceMark, AttendanceMarkLite>).ToList();

            if (retrieveMeanings)
            {
                foreach (var mark in liteMarks)
                {
                    var meaning = await GetMeaning(mark.Mark);
                    mark.Meaning = meaning;
                }
            }

            return liteMarks;
        }

        public async Task SaveRegisterMarks(IEnumerable<StudentAttendanceMarkCollection> markCollections)
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

                        _unitOfWork.AttendanceMarks.Add(attMark);
                    }

                    else
                    {
                        var markInDb = await _unitOfWork.AttendanceMarks.GetByIdAsync(mark.Id);

                        if (markInDb == null)
                        {
                            throw new ProcessException(ExceptionType.NotFound,"Attendance mark not found");
                        }

                        markInDb.Mark = mark.Mark;
                    }
                }
            }

            await _unitOfWork.Complete();
        }

        public async Task<IEnumerable<AttendancePeriod>> GetPeriodsByClass(int classId)
        {
            if (!await _unitOfWork.CurriculumClasses.AnyAsync(x => x.Id == classId))
            {
                throw new ProcessException(ExceptionType.NotFound,"Class not found");
            }

            return await _unitOfWork.AttendancePeriods.GetPeriodsByClass(classId);
        }
    }
}