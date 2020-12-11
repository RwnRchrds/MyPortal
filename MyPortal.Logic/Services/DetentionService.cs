using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MyPortal.Database.Constants;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Models.Search;
using MyPortal.Database.Repositories;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Requests.Behaviour;
using MyPortal.Logic.Models.Requests.Behaviour.Detentions;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Services
{
    public class DetentionService : BaseService, IDetentionService
    {
        private readonly IDetentionRepository _detentionRepository;
        private readonly IIncidentRepository _incidentRepository;
        private readonly IIncidentDetentionRepository _incidentDetentionRepository;
        private readonly IStudentRepository _studentRepository;

        public DetentionService(IDetentionRepository detentionRepository, IIncidentRepository incidentRepository,
            IIncidentDetentionRepository incidentDetentionRepository, IStudentRepository studentRepository)
        {
            _detentionRepository = detentionRepository;
            _incidentRepository = incidentRepository;
            _incidentDetentionRepository = incidentDetentionRepository;
            _studentRepository = studentRepository;
        }

        public async Task<IEnumerable<DetentionModel>> Get(DetentionSearchOptions searchOptions)
        {
            var detentions = await _detentionRepository.GetAll(searchOptions);

            return detentions.Select(BusinessMapper.Map<DetentionModel>);
        }

        public async Task<DetentionModel> GetById(Guid detentionId)
        {
            var detention = await _detentionRepository.GetById(detentionId);

            return BusinessMapper.Map<DetentionModel>(detention);
        }

        public async Task<DetentionModel> GetByIncident(Guid incidentId)
        {
            var detention = await _detentionRepository.GetByIncident(incidentId);

            return BusinessMapper.Map<DetentionModel>(detention);
        }

        public async Task Create(params CreateDetentionRequest[] detentionModels)
        {
            foreach (var model in detentionModels)
            {
                var detention = new Detention
                {
                    DetentionTypeId = model.DetentionTypeId,
                    SupervisorId = model.SupervisorId,
                    Event = new DiaryEvent
                    {
                        StartTime = model.StartTime,
                        EndTime = model.EndTime,
                        RoomId = model.RoomId,
                        EventTypeId = EventTypes.Detention,
                        Subject = "Detention"
                    }
                };

                _detentionRepository.Create(detention);
            }

            await _detentionRepository.SaveChanges();
        }

        public async Task Update(params UpdateDetentionRequest[] detentionModels)
        {
            foreach (var model in detentionModels)
            {
                var detentionInDb = await _detentionRepository.GetByIdWithTracking(model.Id);

                detentionInDb.DetentionTypeId = model.DetentionTypeId;
                detentionInDb.Event.StartTime = model.StartTime;
                detentionInDb.Event.EndTime = model.EndTime;
                detentionInDb.Event.RoomId = model.RoomId;
            }

            await _detentionRepository.SaveChanges();
        }

        public async Task Delete(params Guid[] detentionIds)
        {
            foreach (var detentionId in detentionIds)
            {
                await _detentionRepository.Delete(detentionId);
            }

            await _detentionRepository.SaveChanges();
        }

        public async Task AddStudent(Guid detentionId, Guid incidentId)
        {
            var incidentDetention = new IncidentDetention
            {
                DetentionId = detentionId,
                IncidentId = incidentId
            };

            _incidentDetentionRepository.Create(incidentDetention);

            await _incidentDetentionRepository.SaveChanges();
        }

        public async Task RemoveStudent(Guid incidentDetentionId)
        {
            var relatedIncident = await _incidentDetentionRepository.GetById(incidentDetentionId);

            if (relatedIncident == null)
            {
                throw new NotFoundException("Detention not found.");
            }

            await _incidentDetentionRepository.Delete(relatedIncident.Id);

            await _incidentDetentionRepository.SaveChanges();
        }

        public override void Dispose()
        {
            _detentionRepository.Dispose();
            _incidentRepository.Dispose();
            _incidentDetentionRepository.Dispose();
            _studentRepository.Dispose();
        }
    }
}