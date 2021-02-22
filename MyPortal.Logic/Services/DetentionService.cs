using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.Database.Constants;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Models.Search;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Requests.Behaviour.Detentions;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Services
{
    public class DetentionService : BaseService, IDetentionService
    {
        public DetentionService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<IEnumerable<DetentionModel>> Get(DetentionSearchOptions searchOptions)
        {
            var detentions = await UnitOfWork.Detentions.GetAll(searchOptions);

            return detentions.Select(BusinessMapper.Map<DetentionModel>);
        }

        public async Task<DetentionModel> GetById(Guid detentionId)
        {
            var detention = await UnitOfWork.Detentions.GetById(detentionId);

            return BusinessMapper.Map<DetentionModel>(detention);
        }

        public async Task<DetentionModel> GetByIncident(Guid incidentId)
        {
            var detention = await UnitOfWork.Detentions.GetByIncident(incidentId);

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

                UnitOfWork.Detentions.Create(detention);
            }

            await UnitOfWork.SaveChanges();
        }

        public async Task Update(params UpdateDetentionRequest[] detentionModels)
        {
            foreach (var model in detentionModels)
            {
                var detentionInDb = await UnitOfWork.Detentions.GetByIdForEditing(model.Id);

                detentionInDb.DetentionTypeId = model.DetentionTypeId;
                detentionInDb.Event.StartTime = model.StartTime;
                detentionInDb.Event.EndTime = model.EndTime;
                detentionInDb.Event.RoomId = model.RoomId;
            }

            await UnitOfWork.SaveChanges();
        }

        public async Task Delete(params Guid[] detentionIds)
        {
            foreach (var detentionId in detentionIds)
            {
                await UnitOfWork.Detentions.Delete(detentionId);
            }

            await UnitOfWork.SaveChanges();
        }

        public async Task AddStudent(Guid detentionId, Guid incidentId)
        {
            var incidentDetention = new IncidentDetention
            {
                DetentionId = detentionId,
                IncidentId = incidentId
            };

            UnitOfWork.IncidentDetentions.Create(incidentDetention);

            await UnitOfWork.SaveChanges();
        }

        public async Task RemoveStudent(Guid incidentDetentionId)
        {
            var relatedIncident = await UnitOfWork.IncidentDetentions.GetById(incidentDetentionId);

            if (relatedIncident == null)
            {
                throw new NotFoundException("Detention not found.");
            }

            await UnitOfWork.IncidentDetentions.Delete(relatedIncident.Id);

            await UnitOfWork.SaveChanges();
        }
    }
}