using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Requests.Behaviour.Incidents;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Services
{
    public class IncidentService : BaseService, IIncidentService
    {
        public IncidentService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<IEnumerable<IncidentModel>> GetIncidentsByStudent(Guid studentId, Guid academicYearId)
        {
            var incidents = await UnitOfWork.Incidents.GetByStudent(studentId, academicYearId);

            return incidents.Select(BusinessMapper.Map<IncidentModel>);
        }

        public async Task<IncidentModel> GetIncidentById(Guid incidentId)
        {
            var incident = await UnitOfWork.Incidents.GetById(incidentId);

            return BusinessMapper.Map<IncidentModel>(incident);
        }

        public async Task<int> GetBehaviourPointsByStudent(Guid studentId, Guid academicYearId)
        {
            var points = await UnitOfWork.Incidents.GetPointsByStudent(studentId, academicYearId);

            return points;
        }

        public async Task<int> GetBehaviourCountByStudent(Guid studentId, Guid academicYearId)
        {
            var count = await UnitOfWork.Incidents.GetCountByStudent(studentId, academicYearId);

            return count;
        }

        public async Task CreateIncident(params IncidentModel[] incidents)
        {
            foreach (var incidentModel in incidents)
            {
                var incident = new Incident
                {
                    Points = incidentModel.Points,
                    CreatedDate = DateTime.Now,
                    BehaviourTypeId = incidentModel.BehaviourTypeId,
                    LocationId = incidentModel.LocationId,
                    OutcomeId = incidentModel.OutcomeId,
                    StatusId = incidentModel.StatusId,
                    RecordedById = incidentModel.RecordedById,
                    StudentId = incidentModel.StudentId,
                    Comments = incidentModel.Comments,
                    AcademicYearId = incidentModel.AcademicYearId
                };
                
                UnitOfWork.Incidents.Create(incident);
            }

            await UnitOfWork.SaveChanges();
        }

        public async Task UpdateIncident(params UpdateIncidentModel[] incidents)
        {
            foreach (var incidentModel in incidents)
            {
                var incidentInDb = await UnitOfWork.Incidents.GetByIdForEditing(incidentModel.Id);

                incidentInDb.Points = incidentModel.Points;
                incidentInDb.BehaviourTypeId = incidentModel.BehaviourTypeId;
                incidentInDb.LocationId = incidentModel.LocationId;
                incidentInDb.OutcomeId = incidentModel.OutcomeId;
                incidentInDb.StatusId = incidentModel.StatusId;
                incidentInDb.Comments = incidentModel.Comments;
            }

            await UnitOfWork.SaveChanges();
        }

        public async Task DeleteIncident(params Guid[] incidentIds)
        {
            foreach (var incidentId in incidentIds)
            {
                await UnitOfWork.Incidents.Delete(incidentId);
            }

            await UnitOfWork.SaveChanges();
        }

        public async Task<IEnumerable<IncidentTypeModel>> GetIncidentTypes()
        {
            var types = await UnitOfWork.IncidentTypes.GetAll();

            return types.Select(BusinessMapper.Map<IncidentTypeModel>).ToList();
        }

        public async Task<IEnumerable<BehaviourOutcomeModel>> GetIncidentOutcomes()
        {
            var outcomes = await UnitOfWork.BehaviourOutcomes.GetAll();

            return outcomes.Select(BusinessMapper.Map<BehaviourOutcomeModel>).ToList();
        }

        public async Task<IEnumerable<BehaviourStatusModel>> GetBehaviourStatus()
        {
            var status = await UnitOfWork.BehaviourStatus.GetAll();

            return status.Select(BusinessMapper.Map<BehaviourStatusModel>).ToList();
        }
    }
}