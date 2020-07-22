using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Entity;

namespace MyPortal.Logic.Services
{
    public class AttendancePeriodService : BaseService, IPeriodService
    {
        private readonly IAttendancePeriodRepository _periodRepository;

        public AttendancePeriodService(IAttendancePeriodRepository periodRepository) : base("Attendance Period")
        {
            _periodRepository = periodRepository;
        }

        public async Task<AttendancePeriodModel> GetById(Guid periodId)
        {
            var period = await _periodRepository.GetById(periodId);

            if (period == null)
            {
                throw NotFound();
            }

            return BusinessMapper.Map<AttendancePeriodModel>(period);
        }

        public override void Dispose()
        {
            _periodRepository.Dispose();
        }
    }
}
