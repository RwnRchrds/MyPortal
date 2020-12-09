using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Database.Permissions;
using MyPortal.Logic.Constants;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.DataGrid;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Requests.Behaviour;
using MyPortal.Logic.Models.Requests.Behaviour.Incidents;
using MyPortalWeb.Controllers.BaseControllers;

namespace MyPortalWeb.Controllers.Api
{
    [Route("api/behaviour/incidents")]
    public class IncidentsController : StudentApiController
    {
        private readonly IIncidentService _incidentService;

        public IncidentsController(IUserService userService, IAcademicYearService academicYearService, IStudentService studentService, IIncidentService incidentService) : base(userService, academicYearService, studentService)
        {
            _incidentService = incidentService;
        }

        [HttpGet]
        [Route("id", Name = "ApiIncidentGetById")]
        [Produces(typeof(IncidentModel))]
        public async Task<IActionResult> GetById(Guid incidentId)
        {
            return await ProcessAsync(async () =>
            {
                var incident = await _incidentService.GetById(incidentId);

                if (await AuthenticateStudent(incident.StudentId))
                {
                    return Ok(incident);
                }

                return Forbid();
            }, Permissions.Behaviour.Incidents.ViewIncidents);
        }

        [HttpGet]
        [Route("student", Name = "ApiIncidentGetByStudent")]
        [Produces(typeof(IEnumerable<IncidentListModel>))]
        public async Task<IActionResult> GetByStudent(Guid studentId)
        {
            return await ProcessAsync(async () =>
            {
                if (await AuthenticateStudent(studentId))
                {
                    var academicYearId = await GetCurrentAcademicYearId();

                    var incidents = await _incidentService.GetByStudent(studentId, academicYearId);

                    return Ok(incidents.Select(x => x.ToListModel()));
                }

                return Forbid();
            }, Permissions.Behaviour.Incidents.ViewIncidents);
        }

        [HttpPost]
        [Authorize(Policy = Policies.UserType.Staff)]
        [Route("create", Name = "ApiIncidentCreate")]
        public async Task<IActionResult> Create([FromBody] CreateIncidentModel model)
        {
            return await ProcessAsync(async () =>
            {
                var user = await UserService.GetUserByPrincipal(User);

                var incident = new IncidentModel
                {
                    AcademicYearId = model.AcademicYearId,
                    StudentId = model.StudentId,
                    BehaviourTypeId = model.BehaviourTypeId,
                    Comments = model.Comments,
                    CreatedDate = DateTime.Now,
                    LocationId = model.LocationId,
                    OutcomeId = model.OutcomeId,
                    Points = model.Points,
                    StatusId = model.StatusId,
                    RecordedById = user.Id
                };

                await _incidentService.Create(incident);

                return Ok("Incident created.");
            }, Permissions.Behaviour.Incidents.EditIncidents);
        }

        [HttpPut]
        [Authorize(Policy = Policies.UserType.Staff)]
        [Route("update", Name = "ApiIncidentUpdate")]
        public async Task<IActionResult> Update([FromBody] UpdateIncidentModel model)
        {
            return await ProcessAsync(async () =>
            {
                var user = await UserService.GetUserByPrincipal(User);

                var incident = new IncidentModel
                {
                    AcademicYearId = model.AcademicYearId,
                    StudentId = model.StudentId,
                    BehaviourTypeId = model.BehaviourTypeId,
                    Comments = model.Comments,
                    LocationId = model.LocationId,
                    OutcomeId = model.OutcomeId,
                    Points = model.Points,
                    StatusId = model.StatusId
                };

                await _incidentService.Update(incident);

                return Ok("Incident updated.");
            }, Permissions.Behaviour.Incidents.EditIncidents);
        }

        [HttpDelete]
        [Authorize(Policy = Policies.UserType.Staff)]
        [Route("delete", Name = "ApiIncidentDelete")]
        public async Task<IActionResult> Delete([FromQuery] Guid incidentId)
        {
            return await ProcessAsync(async () =>
            {
                await _incidentService.Delete(incidentId);

                return Ok("Incident deleted.");
            }, Permissions.Behaviour.Incidents.EditIncidents);
        }

        public override void Dispose()
        {
            _incidentService.Dispose();
        }
    }
}
