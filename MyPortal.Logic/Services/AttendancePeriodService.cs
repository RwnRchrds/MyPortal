using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Repositories;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Entity;

namespace MyPortal.Logic.Services
{
    public class AttendancePeriodService : BaseService, IPeriodService
    {
        private readonly IAttendancePeriodRepository _periodRepository;

        public AttendancePeriodService(ApplicationDbContext context)
        {
            _periodRepository = new AttendancePeriodRepository(context);
        }

        public async Task<AttendancePeriodModel> GetById(Guid periodId)
        {
            var period = await _periodRepository.GetById(periodId);

            if (period == null)
            {
                throw new NotFoundException("Period not found.");
            }

            return BusinessMapper.Map<AttendancePeriodModel>(period);
        }
        
        

        public override void Dispose()
        {
            _periodRepository.Dispose();
        }
    }
}
