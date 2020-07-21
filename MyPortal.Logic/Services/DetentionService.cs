using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.Database.Constants;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Search;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Requests.Behaviour;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Services
{
    public class DetentionService : BaseService, IDetentionService
    {
        private readonly IDetentionRepository _detentionRepository;
        private readonly IIncidentRepository _incidentRepository;
        private readonly IIncidentDetentionRepository _incidentDetentionRepository;
        private readonly IDiaryEventAttendeeRepository _attendeeRepository;
        private readonly IStudentRepository _studentRepository;

        public DetentionService(IDetentionRepository detentionRepository, IIncidentRepository incidentRepository, IIncidentDetentionRepository incidentDetentionRepository, IDiaryEventAttendeeRepository attendeeRepository, IStudentRepository studentRepository) : base("Detention")
        {
            _detentionRepository = detentionRepository;
            _incidentRepository = incidentRepository;
            _incidentDetentionRepository = incidentDetentionRepository;
            _attendeeRepository = attendeeRepository;
            _studentRepository = studentRepository;
        }

        public async Task<IEnumerable<DetentionModel>> Get(DetentionSearchOptions searchOptions)
        {
            var detentions = await _detentionRepository.GetAll(searchOptions);

            return detentions.Select(BusinessMapper.Map<DetentionModel>);
        }

        public async Task Create(params CreateDetentionModel[] detentionModels)
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

                _detentionRepository.Create(detention);
            }

            await _detentionRepository.SaveChanges();
        }

        public async Task Update(params UpdateDetentionModel[] detentionModels)
        {
            foreach (var model in detentionModels)
            {
                var detentionInDb = await _detentionRepository.GetByIdWithTracking(model.Id);

                detentionInDb.DetentionTypeId = model.DetentionTypeId;
                detentionInDb.Event.StartTime = model.StartTime;
                detentionInDb.Event.EndTime = model.EndTime;
                detentionInDb.Event.RoomId = model.RoomId;
            }

            await _detentionRepository.SaveChanges();
        }

        public async Task Delete(params Guid[] detentionIds)
        {
            foreach (var detentionId in detentionIds)
            {
                await _detentionRepository.Delete(detentionId);
            }

            await _detentionRepository.SaveChanges();
        }

        public async Task AddStudent(Guid detentionId, Guid studentId)
        {
            var detentionInDb = await _detentionRepository.GetById(detentionId);

            if (detentionInDb == null)
            {
                throw NotFound("Detention not found.");
            }

            var student = await _studentRepository.GetById(studentId);

            if (student == null)
            {
                throw NotFound("Student not found.");
            }

            var attendees = await _attendeeRepository.GetByEvent(detentionInDb.EventId);

            if (attendees.Any(x => x.PersonId == student.PersonId))
            {
                throw BadRequest("Student is already added to this detention.");
            }

            var attendee = new DiaryEventAttendee
            {
                PersonId = student.PersonId,
                EventId = detentionInDb.EventId,
                Required = true,
                ResponseId = AttendeeResponses.Accepted
            };

            _attendeeRepository.Create(attendee);

            await _attendeeRepository.SaveChanges();
        }

        public async Task AddStudent(Guid detentionId, Guid studentId, Guid incidentId)
        {
            await AddStudent(detentionId, studentId);

            var incidentDetention = new IncidentDetention
            {
                DetentionId = detentionId,
                IncidentId = incidentId
            };

            _incidentDetentionRepository.Create(incidentDetention);

            await _incidentDetentionRepository.SaveChanges();
        }

        public override void Dispose()
        {
            _detentionRepository.Dispose();
        }
    }
}