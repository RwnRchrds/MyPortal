using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.Database.Constants;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Models.Search;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Requests.Behaviour.Detentions;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Services
{
    public class DetentionService : BaseService, IDetentionService
    {
        public async Task<IEnumerable<DetentionModel>> Get(DetentionSearchOptions searchOptions)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var detentions = await unitOfWork.Detentions.GetAll(searchOptions);

                return detentions.Select(BusinessMapper.Map<DetentionModel>);
            }
        }

        public async Task<DetentionModel> GetById(Guid detentionId)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var detention = await unitOfWork.Detentions.GetById(detentionId);

                return BusinessMapper.Map<DetentionModel>(detention);
            }
        }

        public async Task<DetentionModel> GetByIncident(Guid incidentId)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var detention = await unitOfWork.Detentions.GetByIncident(incidentId);

                return BusinessMapper.Map<DetentionModel>(detention);
            }
        }

        public async Task Create(params CreateDetentionRequest[] detentionModels)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
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

                    unitOfWork.Detentions.Create(detention);
                }

                await unitOfWork.SaveChangesAsync();
            }
        }

        public async Task Update(params UpdateDetentionRequest[] detentionModels)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                foreach (var model in detentionModels)
                {
                    var detentionInDb = await unitOfWork.Detentions.GetById(model.Id);

                    detentionInDb.DetentionTypeId = model.DetentionTypeId;
                    detentionInDb.Event.StartTime = model.StartTime;
                    detentionInDb.Event.EndTime = model.EndTime;
                    detentionInDb.Event.RoomId = model.RoomId;

                    await unitOfWork.Detentions.Update(detentionInDb);
                }

                await unitOfWork.SaveChangesAsync();
            }
        }

        public async Task Delete(params Guid[] detentionIds)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                foreach (var detentionId in detentionIds)
                {
                    await unitOfWork.Detentions.Delete(detentionId);
                }

                await unitOfWork.SaveChangesAsync();
            }
        }

        public async Task AddStudent(Guid detentionId, Guid incidentId)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var incidentDetention = new IncidentDetention
                {
                    DetentionId = detentionId,
                    IncidentId = incidentId
                };

                unitOfWork.IncidentDetentions.Create(incidentDetention);

                await unitOfWork.SaveChangesAsync();
            }
        }

        public async Task RemoveStudent(Guid incidentDetentionId)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var relatedIncident = await unitOfWork.IncidentDetentions.GetById(incidentDetentionId);

                if (relatedIncident == null)
                {
                    throw new NotFoundException("Detention not found.");
                }

                await unitOfWork.IncidentDetentions.Delete(relatedIncident.Id);

                await unitOfWork.SaveChangesAsync();
            }
        }
    }
}