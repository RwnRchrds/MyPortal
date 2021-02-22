using System;
using System.Threading.Tasks;
using MyPortal.Database.Interfaces;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Entity;

namespace MyPortal.Logic.Services
{
    public class AttendancePeriodService : BaseService, IPeriodService
    { 
        public AttendancePeriodService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<AttendancePeriodModel> GetById(Guid periodId)
        {
            var period = await UnitOfWork.AttendancePeriods.GetById(periodId);

            if (period == null)
            {
                throw new NotFoundException("Period not found.");
            }

            return BusinessMapper.Map<AttendancePeriodModel>(period);
        }
    }
}
