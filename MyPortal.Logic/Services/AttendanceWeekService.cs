using System;
using System.Threading.Tasks;
using MyPortal.Database;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Entity;

namespace MyPortal.Logic.Services
{
    public class AttendanceWeekService : BaseService, IAttendanceWeekService
    {
        public async Task<AttendanceWeekModel> GetById(Guid attendanceWeekId)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var attendanceWeek = await unitOfWork.AttendanceWeeks.GetById(attendanceWeekId);

                if (attendanceWeek == null)
                {
                    throw new NotFoundException("Attendance week not found.");
                }

                return BusinessMapper.Map<AttendanceWeekModel>(attendanceWeek);
            }
        }

        public async Task<AttendanceWeekModel> GetByDate(DateTime date, bool throwIfNotFound = true)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var week = await unitOfWork.AttendanceWeeks.GetByDate(date);

                if (week == null && throwIfNotFound)
                {
                    throw new NotFoundException("Attendance week not found.");
                }

                return BusinessMapper.Map<AttendanceWeekModel>(week);
            }
        }
    }
}
