﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.Database.Constants;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Extensions;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Requests.Curriculum;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Services
{
    public class AcademicYearService : BaseService, IAcademicYearService
    {
        public AcademicYearService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public async Task<AcademicYearModel> GetCurrentAcademicYear(bool getLatestIfNull = false)
        {
            var acadYear = await UnitOfWork.AcademicYears.GetCurrent();

            if (acadYear == null)
            {
                if (getLatestIfNull)
                {
                    acadYear = await UnitOfWork.AcademicYears.GetLatest();

                    if (acadYear == null)
                    {
                        throw new NotFoundException("No academic years are defined.");
                    }
                }
                else
                {
                    throw new NotFoundException("There is no academic year defined for the current date.");
                }
            }

            return BusinessMapper.Map<AcademicYearModel>(acadYear);
        }

        public async Task<AcademicYearModel> GetAcademicYearById(Guid academicYearId)
        {
            var acadYear = await UnitOfWork.AcademicYears.GetById(academicYearId);

            return BusinessMapper.Map<AcademicYearModel>(acadYear);
        }

        public async Task<IEnumerable<AcademicYearModel>> GetAcademicYears()
        {
            var acadYears = await UnitOfWork.AcademicYears.GetAll();

            return acadYears.Select(BusinessMapper.Map<AcademicYearModel>);
        }

        public async Task CreateAcademicYear(params CreateAcademicYearModel[] createModels)
        {
            foreach (var model in createModels)
            {
                var academicYear = new AcademicYear
                {
                    Name = model.Name
                };

                foreach (var termModel in model.AcademicTerms)
                {
                    var term = new AcademicTerm
                    {
                        Name = termModel.Name,
                        StartDate = termModel.StartDate,
                        EndDate = termModel.EndDate
                    };

                    foreach (var attendanceWeek in termModel.AttendanceWeeks)
                    {
                        term.AttendanceWeeks.Add(new AttendanceWeek
                        {
                            Beginning = attendanceWeek.WeekBeginning,
                            WeekPatternId = attendanceWeek.WeekPatternId,
                            IsNonTimetable = attendanceWeek.NonTimetable
                        });
                    }

                    foreach (var schoolHoliday in termModel.Holidays)
                    {
                        UnitOfWork.DiaryEvents.Create(new DiaryEvent
                        {
                            Description = "School Holiday",
                            EventTypeId = EventTypes.SchoolHoliday,
                            IsAllDay = true,
                            StartTime = schoolHoliday.Date,
                            EndTime = schoolHoliday.Date
                        });
                    }

                    academicYear.AcademicTerms.Add(term);
                }

                UnitOfWork.AcademicYears.Create(academicYear);

                await UnitOfWork.SaveChanges();
            }
        }

        public CreateAcademicTermModel[] GenerateAttendanceWeeks(params CreateAcademicTermModel[] termModel)
        {
            foreach (var model in termModel)
            {
                var attendanceWeeks = new List<CreateAttendanceWeekModel>();
                var schoolHolidays = new List<DateTime>();
                var weekPatterns = model.WeekPatterns.OrderBy(p => p.Order).ToArray();
                int patternIndex = 0;
                DateTime currentWeekBeginning = model.StartDate.GetDayOfWeek(DayOfWeek.Monday);

                while (currentWeekBeginning <= model.EndDate.GetDayOfWeek(DayOfWeek.Monday))
                {
                    if (model.StartDate >= currentWeekBeginning &&
                        model.StartDate < currentWeekBeginning.GetDayOfWeek(DayOfWeek.Sunday) &&
                        model.StartDate.DayOfWeek != DayOfWeek.Monday)
                    {
                        var daysBeforeStart = DateTimeExtensions.GetAllDates(currentWeekBeginning, model.StartDate.AddDays(-1));

                        schoolHolidays.AddRange(daysBeforeStart);
                    }

                    if (model.EndDate <= currentWeekBeginning.GetDayOfWeek(DayOfWeek.Sunday) &&
                        model.EndDate >= currentWeekBeginning.GetDayOfWeek(DayOfWeek.Monday) &&
                        model.EndDate.DayOfWeek != DayOfWeek.Sunday)
                    {
                        var daysAfterEnd = DateTimeExtensions.GetAllDates(model.EndDate.AddDays(1),
                            currentWeekBeginning.GetDayOfWeek(DayOfWeek.Sunday));

                        schoolHolidays.AddRange(daysAfterEnd);
                    }

                    attendanceWeeks.Add(new CreateAttendanceWeekModel
                    {
                        WeekBeginning = currentWeekBeginning,
                        WeekPatternId = weekPatterns[patternIndex].WeekPatternId
                    });

                    if (patternIndex == weekPatterns.Length - 1)
                    {
                        patternIndex = 0;
                    }
                    else
                    {
                        patternIndex++;
                    }

                    currentWeekBeginning = currentWeekBeginning.AddDays(7);
                }

                model.AttendanceWeeks = attendanceWeeks.ToArray();
                model.Holidays = schoolHolidays.ToArray();
            }

            return termModel;
        }

        public async Task UpdateAcademicYear(params AcademicYearModel[] academicYearModels)
        {
            foreach (var academicYearModel in academicYearModels)
            {
                var academicYearInDb = await UnitOfWork.AcademicYears.GetById(academicYearModel.Id);

                academicYearInDb.Locked = academicYearModel.Locked;
            }
        }

        public async Task DeleteAcademicYear(params Guid[] academicYearIds)
        {
            foreach (var academicYearId in academicYearIds)
            {
                await UnitOfWork.AcademicYears.Delete(academicYearId);
            }
        }

        public async Task<bool> IsAcademicYearLocked(Guid academicYearId)
        {
            return await UnitOfWork.AcademicYears.IsLocked(academicYearId);
        }

        public override void Dispose()
        {
            UnitOfWork.Dispose();
        }
    }
}
