using System;
using System.Threading.Tasks;
using MyPortal.Database.Interfaces;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Entity;

namespace MyPortal.Logic.Services
{
    public class AttendanceWeekService : BaseService, IAttendanceWeekService
    {
        public AttendanceWeekService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<AttendanceWeekModel> GetById(Guid attendanceWeekId)
        {
            var attendanceWeek = await UnitOfWork.AttendanceWeeks.GetById(attendanceWeekId);

            if (attendanceWeek == null)
            {
                throw new NotFoundException("Attendance week not found.");
            }

            return BusinessMapper.Map<AttendanceWeekModel>(attendanceWeek);
        }

        public async Task<AttendanceWeekModel> GetByDate(DateTime date, bool throwIfNotFound = true)
        {
            var week = await UnitOfWork.AttendanceWeeks.GetByDate(date);

            if (week == null && throwIfNotFound)
            {
                throw new NotFoundException("Attendance week not found.");
            }

            return BusinessMapper.Map<AttendanceWeekModel>(week);
        }

        public override void Dispose()
        {
            UnitOfWork.AttendanceWeeks.Dispose();
        }
    }
}
