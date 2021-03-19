using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Requests.Behaviour.Incidents;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Services
{
    public class IncidentService : BaseService, IIncidentService
    {
        public async Task<IEnumerable<IncidentModel>> GetIncidentsByStudent(Guid studentId, Guid academicYearId)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var incidents = await unitOfWork.Incidents.GetByStudent(studentId, academicYearId);

                return incidents.Select(BusinessMapper.Map<IncidentModel>);
            }
        }

        public async Task<IncidentModel> GetIncidentById(Guid incidentId)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var incident = await unitOfWork.Incidents.GetById(incidentId);

                return BusinessMapper.Map<IncidentModel>(incident);
            }
        }

        public async Task<int> GetBehaviourPointsByStudent(Guid studentId, Guid academicYearId)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var points = await unitOfWork.Incidents.GetPointsByStudent(studentId, academicYearId);

                return points;
            }
        }

        public async Task<int> GetBehaviourCountByStudent(Guid studentId, Guid academicYearId)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var count = await unitOfWork.Incidents.GetCountByStudent(studentId, academicYearId);

                return count;
            }
        }

        public async Task CreateIncident(params IncidentModel[] incidents)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
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

                    unitOfWork.Incidents.Create(incident);
                }

                await unitOfWork.SaveChangesAsync();
            }
        }

        public async Task UpdateIncident(params UpdateIncidentModel[] incidents)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                foreach (var incidentModel in incidents)
                {
                    var incidentInDb = await unitOfWork.Incidents.GetByIdForEditing(incidentModel.Id);

                    incidentInDb.Points = incidentModel.Points;
                    incidentInDb.BehaviourTypeId = incidentModel.BehaviourTypeId;
                    incidentInDb.LocationId = incidentModel.LocationId;
                    incidentInDb.OutcomeId = incidentModel.OutcomeId;
                    incidentInDb.StatusId = incidentModel.StatusId;
                    incidentInDb.Comments = incidentModel.Comments;
                }

                await unitOfWork.SaveChangesAsync();
            }
        }

        public async Task DeleteIncident(params Guid[] incidentIds)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                foreach (var incidentId in incidentIds)
                {
                    await unitOfWork.Incidents.Delete(incidentId);
                }

                await unitOfWork.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<IncidentTypeModel>> GetIncidentTypes()
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var types = await unitOfWork.IncidentTypes.GetAll();

                return types.Select(BusinessMapper.Map<IncidentTypeModel>).ToList();
            }
        }

        public async Task<IEnumerable<BehaviourOutcomeModel>> GetIncidentOutcomes()
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var outcomes = await unitOfWork.BehaviourOutcomes.GetAll();

                return outcomes.Select(BusinessMapper.Map<BehaviourOutcomeModel>).ToList();
            }
        }

        public async Task<IEnumerable<BehaviourStatusModel>> GetBehaviourStatus()
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var status = await unitOfWork.BehaviourStatus.GetAll();

                return status.Select(BusinessMapper.Map<BehaviourStatusModel>).ToList();
            }
        }
    }
}