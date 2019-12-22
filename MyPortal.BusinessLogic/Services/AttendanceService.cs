using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MyPortal.BusinessLogic.Dtos;
using MyPortal.BusinessLogic.Dtos.Lite;
using MyPortal.BusinessLogic.Exceptions;
using MyPortal.BusinessLogic.Extensions;
using MyPortal.BusinessLogic.Models;
using MyPortal.BusinessLogic.Models.Data;
using MyPortal.Data.Interfaces;
using MyPortal.Data.Models;

namespace MyPortal.BusinessLogic.Services
{
    public class AttendanceService : MyPortalService
    {
        public AttendanceService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public AttendanceService() : base()
        {

        }

        public async Task CreateAttendanceWeeksForAcademicYear(int academicYearId)
        {
            using (var curriculumService = new CurriculumService())
            {
                var academicYear = await curriculumService.GetAcademicYearById(academicYearId);

                var pointer = academicYear.FirstDate;
                while (pointer < academicYear.LastDate)
                {
                    var weekBeginning = pointer.StartOfWeek();

                    if (!await UnitOfWork.AttendanceWeeks.Any(x => x.Beginning == weekBeginning))
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

        public async Task<IEnumerable<PeriodDto>> GetAllPeriods()
        {
            return (await UnitOfWork.Periods.GetAll(x => x.Weekday, x => x.StartTime)).Select(Mapping.Map<PeriodDto>);
        }

        public async Task<IEnumerable<AttendanceCodeDto>> GetAllAttendanceCodes()
        {
            return (await UnitOfWork.AttendanceCodes.GetAll(x => x.Code)).Select(Mapping.Map<AttendanceCodeDto>);
        }

        public async Task<IEnumerable<AttendanceCodeDto>> GetUsableAttendanceCodes()
        {
            return (await UnitOfWork.AttendanceCodes.GetUsable()).Select(Mapping.Map<AttendanceCodeDto>);
        }

        public async Task<AttendanceMarkDto> GetAttendanceMark(int attendanceWeekId, int periodId, int studentId)
        {
            var mark = Mapping.Map<AttendanceMarkDto>(await UnitOfWork.AttendanceMarks.Get(studentId, attendanceWeekId, periodId)) ?? new AttendanceMarkDto
            {
                Mark = "-",
                MinutesLate = 0,
                WeekId = attendanceWeekId,
                PeriodId = periodId,
                StudentId = studentId,
            };

            return mark;
        }

        public async Task<AttendanceWeekDto> GetAttendanceWeekById(int attendanceWeekId)
        {
            var week = await UnitOfWork.AttendanceWeeks.GetById(attendanceWeekId);

            if (week == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Attendance week not found.");
            }

            return Mapping.Map<AttendanceWeekDto>(week);
        }

        public async Task<AttendanceCodeDto> GetAttendanceCode(string mark)
        {
            var codeInDb = await UnitOfWork.AttendanceCodes.Get(mark);

            if (codeInDb == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Attendance code not found.");
            }

            return Mapping.Map<AttendanceCodeDto>(codeInDb);
        }

        public async Task<AttendanceCodeMeaningDto> GetMeaning(string mark)
        {
            var codeInDb = await GetAttendanceCode(mark);

            return Mapping.Map<AttendanceCodeMeaningDto>(codeInDb.CodeMeaning);
        }

        public async Task<PeriodDto> GetPeriodById(int periodId)
        {
            var period = await UnitOfWork.Periods.GetById(periodId);

            if (period == null)
            {
                throw new ServiceException(ExceptionType.NotFound,"Period not found.");
            }

            return Mapping.Map<PeriodDto>(period);
        }

        public async Task<DateTime> GetAttendancePeriodDate(int weekId, int periodId)
        {
            var week = await GetAttendanceWeekById(weekId);

            var period = await GetPeriodById(periodId);

            return week.Beginning.GetDayOfWeek(period.Weekday);
        }

        public async Task<IEnumerable<PeriodDto>> GetPeriodsByDayOfWeek(DayOfWeek dayOfWeek)
        {
            return (await UnitOfWork.Periods.GetByDayOfWeek(dayOfWeek)).Select(Mapping.Map<PeriodDto>);
        }

        public async Task<IEnumerable<StudentAttendanceMarkCollection>> GetRegisterMarks(int weekId,
            int sessionId)
        {

            if (!await UnitOfWork.AttendanceWeeks.Any(x => x.Id == weekId))
            {
                throw new ServiceException(ExceptionType.NotFound,"Attendance week not found.");
            }

            var session = await UnitOfWork.Sessions.GetById(sessionId);

            if (session == null)
            {
                throw new ServiceException(ExceptionType.NotFound,"Session not found.");
            }

            var markList = new List<StudentAttendanceMarkCollection>();

            foreach (var enrolment in session.Class.Enrolments)
            {
                var markObject = new StudentAttendanceMarkCollection();
                markObject.StudentName = enrolment.Student.GetDisplayName();
                var marks = new List<AttendanceMarkDto>();

                var periodsInDay = await GetPeriodsByDayOfWeek(session.Period.Weekday);

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
            var marksForStudent = await UnitOfWork.AttendanceMarks.GetByStudent(studentId, academicYearId);

            var marksList = marksForStudent.ToList();

            var summary = new AttendanceSummary();

            if (!marksList.Any())
            {
                return null;
            }

            foreach (var mark in marksList)
            {
                var meaning = await GetMeaning(mark.Mark);

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
                    case "U":
                        summary.UnauthorisedAbsence++;
                        break;
                    case "ANR":
                        summary.NotRequired++;
                        break;
                    case "L":
                        summary.Late++;
                        break;
                    default:
                        continue;
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

            var selectedWeek = await UnitOfWork.AttendanceWeeks.GetByWeekBeginning(academicYearId, date);

            if (selectedWeek == null)
            {
               throw new ServiceException(ExceptionType.NotFound,"Attendance week not found.");
            }

            return Mapping.Map<AttendanceWeekDto>(selectedWeek);
        }

        public async Task<IEnumerable<AttendanceMarkLiteDto>> PrepareLiteMarkList(List<AttendanceMarkDto> marks, bool retrieveMeanings)
        {
            var liteMarks = marks.Select(Mapping.Map<AttendanceMarkLiteDto>).ToList();

            if (retrieveMeanings)
            {
                foreach (AttendanceMarkLiteDto mark in liteMarks)
                {
                    AttendanceCodeMeaningDto meaning = await GetMeaning(mark.Mark);
                    mark.Meaning = meaning;
                }
            }

            return liteMarks;
        }

        public async Task SaveRegisterMarks(IEnumerable<AttendanceMarkLiteDto> marks, bool replaceBlanks)
        {
            foreach (var mark in marks)
            {
                if (replaceBlanks && mark.Mark == "-")
                {
                    mark.Mark = "N";
                }

                var markInDb = await UnitOfWork.AttendanceMarks.Get(mark.StudentId, mark.WeekId, mark.PeriodId);

                if (markInDb != null)
                {
                    //Update attendance mark (delete if '-')
                    if (mark.Mark == "-")
                    {
                        UnitOfWork.AttendanceMarks.Remove(markInDb);
                    }
                    else
                    {
                        markInDb.Mark = mark.Mark;
                        markInDb.MinutesLate = mark.MinutesLate ?? 0;
                        markInDb.Comments = string.IsNullOrWhiteSpace(mark.Comments) ? null : mark.Comments;
                    }
                }

                else
                {
                    //Insert new mark
                    var newMark = new AttendanceMark
                    {
                        Mark = mark.Mark,
                        Comments = string.IsNullOrWhiteSpace(mark.Comments) ? null : mark.Comments,
                        MinutesLate = mark.MinutesLate ?? 0,
                        PeriodId = mark.PeriodId,
                        WeekId = mark.WeekId,
                        StudentId = mark.StudentId
                    };

                    UnitOfWork.AttendanceMarks.Add(newMark);
                }
            }

            await UnitOfWork.Complete();
        }

        public async Task<IEnumerable<PeriodDto>> GetPeriodsByClass(int classId)
        {
            return (await UnitOfWork.Periods.GetByClass(classId)).Select(Mapping.Map<PeriodDto>);
        }
    }
}