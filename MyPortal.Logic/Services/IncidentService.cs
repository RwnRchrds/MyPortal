using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Logic.Extensions;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Data;
using MyPortal.Logic.Models.Entity;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Services
{
    public class IncidentService : BaseService, IIncidentService
    {
        private readonly IIncidentRepository _incidentRepository;
        private readonly IBehaviourOutcomeRepository _outcomeRepository;
        private readonly IBehaviourStatusRepository _statusRepository;
        private readonly IIncidentTypeRepository _incidentTypeRepository;

        public IncidentService(IIncidentRepository incidentRepository, IBehaviourOutcomeRepository outcomeRepository, IBehaviourStatusRepository statusRepository, IIncidentTypeRepository incidentTypeRepository) : base("Incident")
        {
            _incidentRepository = incidentRepository;
            _outcomeRepository = outcomeRepository;
            _statusRepository = statusRepository;
            _incidentTypeRepository = incidentTypeRepository;
        }

        public override void Dispose()
        {
            _incidentRepository.Dispose();
        }

        public async Task<IEnumerable<IncidentModel>> GetByStudent(Guid studentId, Guid academicYearId)
        {
            var incidents = await _incidentRepository.GetByStudent(studentId, academicYearId);

            return incidents.Select(BusinessMapper.Map<IncidentModel>);
        }

        public async Task<IncidentModel> GetById(Guid incidentId)
        {
            var incident = await _incidentRepository.GetById(incidentId);

            return BusinessMapper.Map<IncidentModel>(incident);
        }

        public async Task<int> GetPointsByStudent(Guid studentId, Guid academicYearId)
        {
            var points = await _incidentRepository.GetPointsByStudent(studentId, academicYearId);

            return points;
        }

        public async Task<int> GetCountByStudent(Guid studentId, Guid academicYearId)
        {
            var count = await _incidentRepository.GetCountByStudent(studentId, academicYearId);

            return count;
        }

        public async Task Create(params IncidentModel[] incidents)
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
                
                _incidentRepository.Create(incident);
            }

            await _incidentRepository.SaveChanges();
        }

        public async Task Update(params IncidentModel[] incidents)
        {
            foreach (var incidentModel in incidents)
            {
                var incidentInDb = await _incidentRepository.GetByIdWithTracking(incidentModel.Id);

                incidentInDb.Points = incidentModel.Points;
                incidentInDb.BehaviourTypeId = incidentModel.BehaviourTypeId;
                incidentInDb.LocationId = incidentModel.LocationId;
                incidentInDb.OutcomeId = incidentModel.OutcomeId;
                incidentInDb.StatusId = incidentModel.StatusId;
                incidentInDb.Comments = incidentModel.Comments;
            }

            await _incidentRepository.SaveChanges();
        }

        public async Task Delete(params Guid[] incidentIds)
        {
            foreach (var incidentId in incidentIds)
            {
                await _incidentRepository.Delete(incidentId);
            }

            await _incidentRepository.SaveChanges();
        }

        public async Task<Lookup> GetTypes()
        {
            var types = await _incidentTypeRepository.GetAll();

            return types.ToLookup();
        }

        public async Task<Lookup> GetOutcomes()
        {
            var outcomes = await _outcomeRepository.GetAll();

            return outcomes.ToLookup();
        }

        public async Task<Lookup> GetStatus()
        {
            var status = await _statusRepository.GetAll();

            return status.ToLookup();
        }
    }
}