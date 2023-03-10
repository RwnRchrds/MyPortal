using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Models.Search;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Requests.Behaviour.Detentions;

namespace MyPortal.Logic.Interfaces.Services
{
    public interface IDetentionService
    {
        Task<IEnumerable<DetentionModel>> Get(DetentionSearchOptions searchOptions);
        Task<DetentionModel> GetById(Guid detentionId);
        Task<DetentionModel> GetByIncident(Guid incidentId);
        Task Create(params CreateDetentionRequest[] detentionModels);
        Task Update(params UpdateDetentionRequest[] detentionModels);
        Task Delete(params Guid[] detentionIds);
        Task AddStudent(Guid detentionId, Guid incidentId);
        Task RemoveStudent(Guid incidentDetentionId);
    }
}