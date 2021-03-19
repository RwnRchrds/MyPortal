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
    public class AttendancePeriodService : BaseService, IAttendancePeriodService
    {
        public async Task<AttendancePeriodModel> GetById(Guid periodId)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var period = await unitOfWork.AttendancePeriods.GetById(periodId);

                if (period == null)
                {
                    throw new NotFoundException("Period not found.");
                }

                return BusinessMapper.Map<AttendancePeriodModel>(period);
            }
        }
    }
}
