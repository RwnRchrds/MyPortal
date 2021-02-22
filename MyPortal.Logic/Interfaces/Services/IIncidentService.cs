using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Requests.Behaviour.Incidents;

namespace MyPortal.Logic.Interfaces.Services
{
    public interface IIncidentService : IService
    {
        Task<IEnumerable<IncidentModel>> GetIncidentsByStudent(Guid studentId, Guid academicYearId);
        Task<IncidentModel> GetIncidentById(Guid incidentId);
        Task<int> GetBehaviourPointsByStudent(Guid studentId, Guid academicYearId);
        Task<int> GetBehaviourCountByStudent(Guid studentId, Guid academicYearId);
        Task CreateIncident(params IncidentModel[] incidents);
        Task UpdateIncident(params UpdateIncidentModel[] incidents);
        Task DeleteIncident(params Guid[] incidentIds);
        Task<IEnumerable<IncidentTypeModel>> GetIncidentTypes();
        Task<IEnumerable<BehaviourOutcomeModel>> GetIncidentOutcomes();
        Task<IEnumerable<BehaviourStatusModel>> GetBehaviourStatus();
    }
}