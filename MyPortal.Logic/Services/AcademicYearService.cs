using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing.Patterns;
using MyPortal.Database.Constants;
using MyPortal.Database.Interfaces.Repositories;
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
        private readonly IAcademicYearRepository _academicYearRepository;
        private readonly IAttendanceWeekRepository _attendanceWeekRepository;
        private readonly IDiaryEventRepository _diaryEventRepository;

        public AcademicYearService(IAcademicYearRepository academicYearRepository,
            IAttendanceWeekRepository attendanceWeekRepository, IDiaryEventRepository diaryEventRepository)
        {
            _academicYearRepository = academicYearRepository;
            _attendanceWeekRepository = attendanceWeekRepository;
            _diaryEventRepository = diaryEventRepository;
        }

        public async Task<AcademicYearModel> GetCurrent(bool getLatestIfNull = false)
        {
            var acadYear = await _academicYearRepository.GetCurrent();

            if (acadYear == null)
            {
                if (getLatestIfNull)
                {
                    acadYear = await _academicYearRepository.GetLatest();

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

        public async Task<AcademicYearModel> GetById(Guid academicYearId)
        {
            var acadYear = await _academicYearRepository.GetById(academicYearId);

            return BusinessMapper.Map<AcademicYearModel>(acadYear);
        }

        public async Task<IEnumerable<AcademicYearModel>> GetAll()
        {
            var acadYears = await _academicYearRepository.GetAll();

            return acadYears.Select(BusinessMapper.Map<AcademicYearModel>);
        }

        public async Task Create(AcademicYearModel academicYearModel)
        {
            var academicYear = BusinessMapper.Map<AcademicYear>(academicYearModel);

            _academicYearRepository.Create(academicYear);

            await _academicYearRepository.SaveChanges();
        }

        public async Task Create(params CreateAcademicYearModel[] createModels)
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
                        _diaryEventRepository.Create(new DiaryEvent
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

                _academicYearRepository.Create(academicYear);

                await _academicYearRepository.SaveChanges();
                await _diaryEventRepository.SaveChanges();
            }
        }

        public CreateAcademicTermModel[] GenerateAttendanceWeeks(CreateAcademicTermModel[] termModel)
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

        public async Task Update(params AcademicYearModel[] academicYearModels)
        {
            foreach (var academicYearModel in academicYearModels)
            {
                var academicYearInDb = await _academicYearRepository.GetById(academicYearModel.Id);

                academicYearInDb.Locked = academicYearModel.Locked;
            }
        }

        public async Task Delete(params Guid[] academicYearIds)
        {
            foreach (var academicYearId in academicYearIds)
            {
                await _academicYearRepository.Delete(academicYearId);
            }
        }

        public async Task<bool> IsLocked(Guid academicYearId)
        {
            return await _academicYearRepository.IsLocked(academicYearId);
        }

        public override void Dispose()
        {
            _academicYearRepository.Dispose();
        }
    }
}
