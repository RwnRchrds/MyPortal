using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MyPortal.Dtos.LiteDtos;
using MyPortal.Exceptions;
using MyPortal.Extensions;
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
            using (var curriculumService = new CurriculumService(UnitOfWork))
            {
                var academicYear = await curriculumService.GetAcademicYearById(academicYearId);

                var pointer = academicYear.FirstDate;
                while (pointer < academicYear.LastDate)
                {
                    var weekBeginning = pointer.StartOfWeek();

                    if (!await UnitOfWork.AttendanceWeeks.AnyAsync(x => x.Beginning == weekBeginning))
                    {
                        var attendanceWeek = new AttendanceWeek()
                        {
                            AcademicYearId = academicYear.Id,
                            Beginning = weekBeginning
                        };

                        UnitOfWork.AttendanceWeeks.Add(attendanceWeek);
                    }

                    pointer = weekBeginning.AddDays(7);
                }

                await UnitOfWork.Complete();
            }
        }

        public async Task<IEnumerable<AttendancePeriod>> GetAllPeriods()
        {
            var attendancePeriods = await UnitOfWork.AttendancePeriods.GetAllPeriods();

            return attendancePeriods;
        }

        public async Task<AttendanceMark> GetAttendanceMark(int attendanceWeekId, int periodId, int studentId)
        {
            var mark = await UnitOfWork.AttendanceMarks.GetAttendanceMark(studentId, attendanceWeekId, periodId) ?? new AttendanceMark
            {
                Mark = "-",
                WeekId = attendanceWeekId,
                PeriodId = periodId,
                StudentId = studentId,
            };

            return mark;
        }

        public async Task<AttendanceWeek> GetAttendanceWeekById(int attendanceWeekId)
        {
            var week = await UnitOfWork.AttendanceWeeks.GetByIdAsync(attendanceWeekId);

            if (week == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Attendance week not found");
            }

            return week;
        }

        public async Task<AttendanceCode> GetAttendanceCode(string mark)
        {
            var codeInDb = await UnitOfWork.AttendanceCodes.GetAttendanceCode(mark);

            if (codeInDb == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Attendance code not found");
            }

            return codeInDb;
        }

        public async Task<AttendanceMeaning> GetMeaning(string mark)
        {
            var codeInDb = await GetAttendanceCode(mark);

            return codeInDb.Meaning;
        }

        public async Task<AttendancePeriod> GetPeriodById(int periodId)
        {
            var period = await UnitOfWork.AttendancePeriods.GetByIdAsync(periodId);

            if (period == null)
            {
                throw new ServiceException(ExceptionType.NotFound,"Period not found");
            }

            return period;
        }

        public async Task<DateTime> GetAttendancePeriodDate(int weekId, int periodId)
        {
            var week = await GetAttendanceWeekById(weekId);

            var period = await GetPeriodById(periodId);

            return week.Beginning.GetDayOfWeek(period.Weekday);
        }

        public async Task<IEnumerable<StudentAttendanceMarkCollection>> GetRegisterMarks(int weekId,
            int sessionId)
        {

            if (!await UnitOfWork.AttendanceWeeks.AnyAsync(x => x.Id == weekId))
            {
                throw new ServiceException(ExceptionType.NotFound,"Attendance week not found");
            }

            var session = await UnitOfWork.CurriculumSessions.GetByIdAsync(sessionId);

            if (session == null)
            {
                throw new ServiceException(ExceptionType.NotFound,"Session not found");
            }

            var markList = new List<StudentAttendanceMarkCollection>();

            foreach (var enrolment in session.Class.Enrolments)
            {
                var markObject = new StudentAttendanceMarkCollection();
                markObject.StudentName = enrolment.Student.GetDisplayName();
                var marks = new List<AttendanceMark>();

                var periodsInDay = await UnitOfWork.AttendancePeriods.GetPeriodsByDayOfWeek(session.Period.Weekday);

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
            var marksForStudent = await UnitOfWork.AttendanceMarks.GetAllAttendanceMarksByStudent(studentId, academicYearId);

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

        public async Task<AttendanceWeek> GetWeekByDate(int academicYearId, DateTime date)
        {
            var weekBeginning = date.StartOfWeek();

            var selectedWeek = await UnitOfWork.AttendanceWeeks.GetAttendanceWeekByDate(academicYearId, date);

            if (selectedWeek == null)
            {
               throw new ServiceException(ExceptionType.NotFound,"Attendance week not found");
            }

            return selectedWeek;
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

                        UnitOfWork.AttendanceMarks.Add(attMark);
                    }

                    else
                    {
                        var markInDb = await UnitOfWork.AttendanceMarks.GetByIdAsync(mark.Id);

                        if (markInDb == null)
                        {
                            throw new ServiceException(ExceptionType.NotFound,"Attendance mark not found");
                        }

                        markInDb.Mark = mark.Mark;
                    }
                }
            }

            await UnitOfWork.Complete();
        }

        public async Task<IEnumerable<AttendancePeriod>> GetPeriodsByClass(int classId)
        {
            if (!await UnitOfWork.CurriculumClasses.AnyAsync(x => x.Id == classId))
            {
                throw new ServiceException(ExceptionType.NotFound,"Class not found");
            }

            return await UnitOfWork.AttendancePeriods.GetPeriodsByClass(classId);
        }
    }
}