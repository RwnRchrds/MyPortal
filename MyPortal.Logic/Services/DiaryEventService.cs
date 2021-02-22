using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.Database.Interfaces;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Data;
using MyPortal.Logic.Models.Entity;

namespace MyPortal.Logic.Services
{
    public class DiaryEventService : BaseService, IDiaryEventService
    {
        public DiaryEventService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<IEnumerable<DiaryEventTypeModel>> GetEventTypes(bool includeReserved = false)
        {
            var eventTypes = await UnitOfWork.DiaryEventTypes.GetAll(includeReserved);

            return eventTypes.Select(BusinessMapper.Map<DiaryEventTypeModel>).ToList();
        }

        public async Task<IEnumerable<DiaryEventModel>> GetDiaryEventsByStudent(Guid studentId, DateRange dateRange)
        {
            throw new NotImplementedException();
        }
    }
}