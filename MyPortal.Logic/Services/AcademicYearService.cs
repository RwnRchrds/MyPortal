using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MyPortal.Database;
using MyPortal.Database.Constants;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Extensions;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Data.Curriculum;

using MyPortal.Logic.Models.Requests.Curriculum;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Services
{
    public class AcademicYearService : BaseUserService, IAcademicYearService
    {
        public AcademicYearService(ICurrentUser user) : base(user)
        {
        }

        public async Task<bool> IsAcademicYearLocked(Guid academicYearId, bool throwException = false)
        {
            await using var unitOfWork = await User.GetConnection();
            if (await unitOfWork.AcademicYears.IsLocked(academicYearId))
            {
                if (throwException)
                {
                    throw new YearLockedException("This academic year is locked and cannot be modified.");
                }
                    
                return true;
            }

            return false;
        }
        
        public async Task<bool> IsAcademicYearLockedByWeek(Guid attendanceWeekId, bool throwException = false)
        {
            await using var unitOfWork = await User.GetConnection();
            if (await unitOfWork.AcademicYears.IsLockedByWeek(attendanceWeekId))
            {
                if (throwException)
                {
                    throw new YearLockedException("This academic year is locked and cannot be modified.");
                }
                    
                return true;
            }

            return false;
        }

        public async Task<AcademicYearModel> GetCurrentAcademicYear(bool getLatestIfNull = false)
        {
            await using var unitOfWork = await User.GetConnection();
            var acadYear = await unitOfWork.AcademicYears.GetCurrent();

            if (acadYear == null)
            {
                if (getLatestIfNull)
                {
                    acadYear = await unitOfWork.AcademicYears.GetLatest();

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

            return new AcademicYearModel(acadYear);
        }

        public async Task<AcademicYearModel> GetAcademicYearById(Guid academicYearId)
        {
            await using var unitOfWork = await User.GetConnection();
            var acadYear = await unitOfWork.AcademicYears.GetById(academicYearId);

            return new AcademicYearModel(acadYear);
        }

        public async Task<IEnumerable<AcademicYearModel>> GetAcademicYears()
        {
            await using var unitOfWork = await User.GetConnection();
            var acadYears = await unitOfWork.AcademicYears.GetAll();

            return acadYears.Select(y => new AcademicYearModel(y));
        }

        public async Task<AcademicYearModel> CreateAcademicYear(AcademicYearRequestModel model)
        {
            Validate(model);
            
            var academicYear = new AcademicYear
            {
                Id = Guid.NewGuid(),
                Name = model.Name
            };
            
            await using var unitOfWork = await User.GetConnection();

            foreach (var termModel in model.AcademicTerms)
            {
                var term = new AcademicTerm
                {
                    Id = Guid.NewGuid(),
                    Name = termModel.Name,
                    StartDate = termModel.StartDate,
                    EndDate = termModel.EndDate
                };

                foreach (var attendanceWeek in termModel.AttendanceWeeks)
                {
                    term.AttendanceWeeks.Add(new AttendanceWeek
                    {
                        Id = Guid.NewGuid(),
                        Beginning = attendanceWeek.WeekBeginning,
                        WeekPatternId = attendanceWeek.WeekPatternId,
                        IsNonTimetable = attendanceWeek.NonTimetable
                    });
                }
                
                foreach (var schoolHoliday in termModel.Holidays)
                {
                    unitOfWork.DiaryEvents.Create(new DiaryEvent
                    {
                        Id = Guid.NewGuid(),
                        Description = "School Holiday",
                        EventTypeId = EventTypes.SchoolHoliday,
                        StartTime = schoolHoliday.Date,
                        EndTime = schoolHoliday.Date,
                        AllDay = true
                    });
                }

                academicYear.AcademicTerms.Add(term);
            }

            unitOfWork.AcademicYears.Create(academicYear);

            await unitOfWork.SaveChangesAsync();

            return new AcademicYearModel(academicYear);
        }

        public AcademicTermRequestModel GenerateAttendanceWeeks(AcademicTermRequestModel model)
        {
            // TODO: This should be moved to the web app
            var attendanceWeeks = new List<AttendanceWeekRequestModel>();
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
                    var daysBeforeStart =
                        DateTimeHelper.GetAllInstances(currentWeekBeginning, model.StartDate.AddDays(-1));

                    schoolHolidays.AddRange(daysBeforeStart);
                }

                if (model.EndDate <= currentWeekBeginning.GetDayOfWeek(DayOfWeek.Sunday) &&
                    model.EndDate >= currentWeekBeginning.GetDayOfWeek(DayOfWeek.Monday) &&
                    model.EndDate.DayOfWeek != DayOfWeek.Sunday)
                {
                    var daysAfterEnd = DateTimeHelper.GetAllInstances(model.EndDate.AddDays(1),
                        currentWeekBeginning.GetDayOfWeek(DayOfWeek.Sunday));

                    schoolHolidays.AddRange(daysAfterEnd);
                }

                attendanceWeeks.Add(new AttendanceWeekRequestModel
                {
                    WeekBeginning = currentWeekBeginning,
                    WeekPatternId = weekPatterns[patternIndex].WeekPatternId
                });

                patternIndex++;

                if (patternIndex >= weekPatterns.Length)
                {
                    patternIndex = 0;
                }

                currentWeekBeginning = currentWeekBeginning.AddDays(7);
            }

            model.AttendanceWeeks = attendanceWeeks.ToArray();
            model.Holidays = schoolHolidays.ToArray();

            return model;
        }

        public async Task UpdateAcademicYear(Guid academicYearId, AcademicYearRequestModel model)
        {
            Validate(model);
            
            await using var unitOfWork = await User.GetConnection();
            var academicYearInDb = await unitOfWork.AcademicYears.GetById(academicYearId);

            academicYearInDb.Name = model.Name;
            academicYearInDb.Locked = model.Locked;

            await unitOfWork.AcademicYears.Update(academicYearInDb);

            await unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAcademicYear(Guid academicYearId)
        {
            await using var unitOfWork = await User.GetConnection();
            
            await unitOfWork.AcademicYears.Delete(academicYearId);

            await unitOfWork.SaveChangesAsync();
        }
    }
}
