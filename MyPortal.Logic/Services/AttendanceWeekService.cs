using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Database.Interfaces;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Business;

namespace MyPortal.Logic.Services
{
    public class AttendanceWeekService : BaseService, IAttendanceWeekService
    {
        private readonly IAttendanceWeekRepository _attendanceWeekRepository;

        public AttendanceWeekService(IAttendanceWeekRepository attendanceWeekRepository) : base("Attendance week")
        {
            _attendanceWeekRepository = attendanceWeekRepository;
        }

        public async Task<AttendanceWeekModel> GetById(Guid attendanceWeekId)
        {
            var attendanceWeek = await _attendanceWeekRepository.GetById(attendanceWeekId);

            if (attendanceWeek == null)
            {
                throw NotFound();
            }

            return BusinessMapper.Map<AttendanceWeekModel>(attendanceWeek);
        }

        public override void Dispose()
        {
            _attendanceWeekRepository.Dispose();
        }
    }
}
