using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Database.Enums;
using MyPortal.Logic.Constants;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.DataGrid;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Requests.Behaviour.Incidents;
using MyPortalWeb.Controllers.BaseControllers;

namespace MyPortalWeb.Controllers.Api
{
    [Route("api/behaviour/incidents")]
    public class IncidentsController : StudentApiController
    {
        public IncidentsController(IAppServiceCollection services) : base(services)
        {
        }

        [HttpGet]
        [Route("id", Name = "ApiIncidentGetById")]
        [ProducesResponseType(typeof(IncidentModel), 200)]
        public async Task<IActionResult> GetById(Guid incidentId)
        {
            return await ProcessAsync(async () =>
            {
                var incident = await Services.Incidents.GetIncidentById(incidentId);

                if (await AuthoriseStudent(incident.StudentId))
                {
                    return Ok(incident);
                }

                return Forbid();
            }, PermissionValue.BehaviourViewIncidents);
        }

        [HttpGet]
        [Route("student", Name = "ApiIncidentGetByStudent")]
        [ProducesResponseType(typeof(IEnumerable<IncidentListModel>), 200)]
        public async Task<IActionResult> GetByStudent([FromQuery] Guid studentId, [FromQuery] Guid? academicYearId)
        {
            return await ProcessAsync(async () =>
            {
                if (await AuthoriseStudent(studentId))
                {
                    var fromAcademicYearId = academicYearId ?? (await Services.AcademicYears.GetCurrentAcademicYear(true)).Id;

                    var incidents = await Services.Incidents.GetIncidentsByStudent(studentId, fromAcademicYearId);

                    return Ok(incidents.Select(x => x.ToListModel()));
                }

                return Forbid();
            }, PermissionValue.BehaviourViewIncidents);
        }

        [HttpPost]
        [Authorize(Policy = Policies.UserType.Staff)]
        [Route("create", Name = "ApiIncidentCreate")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Create([FromBody] CreateIncidentModel model)
        {
            return await ProcessAsync(async () =>
            {
                var user = await Services.Users.GetUserByPrincipal(User);

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

                await Services.Incidents.CreateIncident(incident);

                return Ok();
            }, PermissionValue.BehaviourEditIncidents);
        }

        [HttpPut]
        [Authorize(Policy = Policies.UserType.Staff)]
        [Route("update", Name = "ApiIncidentUpdate")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Update([FromBody] UpdateIncidentModel model)
        {
            return await ProcessAsync(async () =>
            {
                var user = await Services.Users.GetUserByPrincipal(User);

                await Services.Incidents.UpdateIncident(model);

                return Ok();
            }, PermissionValue.BehaviourEditIncidents);
        }

        [HttpDelete]
        [Authorize(Policy = Policies.UserType.Staff)]
        [Route("delete", Name = "ApiIncidentDelete")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Delete([FromQuery] Guid incidentId)
        {
            return await ProcessAsync(async () =>
            {
                await Services.Incidents.DeleteIncident(incidentId);

                return Ok();
            }, PermissionValue.BehaviourEditIncidents);
        }
    }
}
