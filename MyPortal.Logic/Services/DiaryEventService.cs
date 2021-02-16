using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Repositories;
using MyPortal.Logic.Exceptions;
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
        private readonly IStudentRepository _studentRepository;

        public DiaryEventService(IDiaryEventRepository diaryEventRepository,
            IDiaryEventTypeRepository diaryEventTypeRepository, IDetentionRepository detentionRepository, IStudentRepository studentRepository)
        {
            _diaryEventRepository = diaryEventRepository;
            _diaryEventTypeRepository = diaryEventTypeRepository;
            _detentionRepository = detentionRepository;
            _studentRepository = studentRepository;
        }

        public async Task<IEnumerable<DiaryEventTypeModel>> GetEventTypes(bool includeReserved = false)
        {
            var eventTypes = await _diaryEventTypeRepository.GetAll(includeReserved);

            return eventTypes.Select(BusinessMapper.Map<DiaryEventTypeModel>).ToList();
        }

        public async Task<IEnumerable<DiaryEventModel>> GetDiaryEventsByStudent(Guid studentId, DateRange dateRange)
        {
            throw new NotImplementedException();
        }

        public override void Dispose()
        {
            _diaryEventRepository.Dispose();
            _detentionRepository.Dispose();
        }
    }
}