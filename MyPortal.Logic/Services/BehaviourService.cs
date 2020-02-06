using System;
using System.Linq;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Exceptions;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Services
{
    public class BehaviourService : BaseService, IBehaviourService
    {
        private readonly IAchievementRepository _achievementRepository;
        private readonly IAchievementTypeRepository _achievementTypeRepository;
        private readonly IDetentionRepository _detentionRepository;
        private readonly IDetentionTypeRepository _detentionTypeRepository;
        private readonly IIncidentDetentionRepository _incidentDetentionRepository;
        private readonly IIncidentRepository _incidentRepository;
        private readonly IIncidentTypeRepository _incidentTypeRepository;

        public BehaviourService(IAchievementRepository achievementRepository,
            IAchievementTypeRepository achievementTypeRepository,
            IIncidentDetentionRepository incidentDetentionRepository, IDetentionRepository detentionRepository,
            IIncidentRepository incidentRepository, IIncidentTypeRepository incidentTypeRepository,
            IDetentionTypeRepository detentionTypeRepository)
        {
            _achievementRepository = achievementRepository;
            _achievementTypeRepository = achievementTypeRepository;
            _incidentDetentionRepository = incidentDetentionRepository;
            _detentionRepository = detentionRepository;
            _incidentRepository = incidentRepository;
            _incidentTypeRepository = incidentTypeRepository;
            _detentionTypeRepository = detentionTypeRepository;
        }

        public async Task AddIncidentToDetention(Guid incidentId, Guid detentionId)
        {
            var incidentInDb = await _incidentRepository.GetByIdWithTracking(incidentId);
            var detentionInDb = await _detentionRepository.GetByIdWithTracking(detentionId);

            if (detentionInDb.Incidents.Any(x => x.Incident.StudentId == incidentInDb.StudentId))
                throw new ServiceException(ExceptionType.BadRequest, "Student is already in this detention.");

            detentionInDb.Incidents.Add(new IncidentDetention
            {
                DetentionId = detentionInDb.Id
            });

            detentionInDb.Event.Attendees.Add(new DiaryEventAttendee
            {
                PersonId = incidentInDb.Student.PersonId,
                Attended = false,
                Required = true
            });

            await _incidentRepository.SaveChanges();
            await _detentionRepository.SaveChanges();
        }

        public async Task RemoveIncidentFromDetention(Guid incidentId, Guid detentionId)
        {
            var incidentInDb = await _incidentRepository.GetByIdWithTracking(incidentId);
            var detentionInDb = await _detentionRepository.GetByIdWithTracking(detentionId);

            var incidentDetention = incidentInDb.Detentions.SingleOrDefault(x => x.DetentionId == detentionInDb.Id);

            if (incidentDetention == null)
                throw new ServiceException(ExceptionType.NotFound, "Student is not in this detention.");

            detentionInDb.Incidents.Remove(incidentDetention);

            var attendee =
                detentionInDb.Event.Attendees.SingleOrDefault(x => x.PersonId == incidentInDb.Student.PersonId);

            if (attendee == null) throw new ServiceException(ExceptionType.NotFound, "Event attendee not found.");

            detentionInDb.Event.Attendees.Remove(attendee);

            await _detentionRepository.SaveChanges();
            await _incidentRepository.SaveChanges();
        }
    }
}