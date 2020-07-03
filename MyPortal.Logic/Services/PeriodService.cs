using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Business;

namespace MyPortal.Logic.Services
{
    public class PeriodService : BaseService, IPeriodService
    {
        private readonly IPeriodRepository _periodRepository;

        public PeriodService(IPeriodRepository periodRepository) : base("Period")
        {
            _periodRepository = periodRepository;
        }

        public async Task<PeriodModel> GetById(Guid periodId)
        {
            var period = await _periodRepository.GetById(periodId);

            if (period == null)
            {
                throw NotFound();
            }

            return BusinessMapper.Map<PeriodModel>(period);
        }

        public override void Dispose()
        {
            _periodRepository.Dispose();
        }
    }
}
