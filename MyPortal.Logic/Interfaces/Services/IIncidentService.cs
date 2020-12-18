using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Requests.Behaviour.Incidents;

namespace MyPortal.Logic.Interfaces.Services
{
    public interface IIncidentService : IService
    {
        Task<IEnumerable<IncidentModel>> GetByStudent(Guid studentId, Guid academicYearId);
        Task<IncidentModel> GetById(Guid incidentId);
        Task<int> GetPointsByStudent(Guid studentId, Guid academicYearId);
        Task<int> GetCountByStudent(Guid studentId, Guid academicYearId);
        Task Create(params IncidentModel[] incidents);
        Task Update(params UpdateIncidentModel[] incidents);
        Task Delete(params Guid[] incidentIds);
        Task<IEnumerable<IncidentTypeModel>> GetTypes();
        Task<IEnumerable<BehaviourOutcomeModel>> GetOutcomes();
        Task<IEnumerable<BehaviourStatusModel>> GetStatus();
    }
}