using System;
using System.Collections.Generic;
using System.Text;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using MyPortal.Logic.Interfaces;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Services
{
    public class DetentionService : BaseService, IDetentionService
    {
        private readonly IDetentionRepository _detentionRepository;
        private readonly IIncidentRepository _incidentRepository;

        public async Task AddIncidentToDetention(int incidentId, int detentionId)
        {
            var incidentInDb = await _incidentRepository.GetByIdWithTracking(incidentId);
            var detentionInDb = await _detentionRepository.GetByIdWithTracking(detentionId);

            detentionInDb.Event.Attendees.Add(new DiaryEventAttendee
            {
                PersonId = incidentInDb.Student.Person.Id,
                EventId = detentionInDb.EventId,
                Attended = false,
                Required = true
            });

            detentionInDb.Incidents.Add(new IncidentDetention
            {
                DetentionId = detentionInDb.Id,
                IncidentId = incidentInDb.Id
            });

            await _incidentRepository.SaveChanges();
            await _detentionRepository.SaveChanges();
        }
    }
}
