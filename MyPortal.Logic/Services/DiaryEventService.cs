using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Repositories;
using MyPortal.Logic.Extensions;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Data;
using MyPortal.Logic.Models.Entity;

namespace MyPortal.Logic.Services
{
    public class DiaryEventService : BaseService, IDiaryEventService
    {
        private readonly IDiaryEventRepository _diaryEventRepository;
        private readonly IDiaryEventTypeRepository _diaryEventTypeRepository;
        private readonly IDetentionRepository _detentionRepository;

        public DiaryEventService(IDiaryEventRepository diaryEventRepository,
            IDiaryEventTypeRepository diaryEventTypeRepository, IDetentionRepository detentionRepository)
        {
            _diaryEventRepository = diaryEventRepository;
            _diaryEventTypeRepository = diaryEventTypeRepository;
            _detentionRepository = detentionRepository;
        }

        public async Task<IEnumerable<DiaryEventTypeModel>> GetEventTypes(bool includeReserved = false)
        {
            var eventTypes = await _diaryEventTypeRepository.GetAll(includeReserved);

            return eventTypes.Select(BusinessMapper.Map<DiaryEventTypeModel>).ToList();
        }

        public override void Dispose()
        {
            _diaryEventRepository.Dispose();
            _detentionRepository.Dispose();
        }
    }
}