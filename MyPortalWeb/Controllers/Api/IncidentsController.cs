using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Database.Enums;
using MyPortal.Logic.Constants;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Collection;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Requests.Behaviour.Incidents;
using MyPortalWeb.Attributes;
using MyPortalWeb.Controllers.BaseControllers;

namespace MyPortalWeb.Controllers.Api
{
    [Route("api/behaviour/incidents")]
    public class IncidentsController : StudentDataController
    {
        private IAcademicYearService _academicYearService;
        private IBehaviourService _behaviourService;

        public IncidentsController(IStudentService studentService, IUserService userService, IRoleService roleService,
            IAcademicYearService academicYearService, IBehaviourService behaviourService) : base(studentService,
            userService, roleService)
        {
            _academicYearService = academicYearService;
            _behaviourService = behaviourService;
        }

        [HttpGet]
        [Route("id", Name = "ApiIncidentGetById")]
        [Permission(PermissionValue.BehaviourViewIncidents)]
        [ProducesResponseType(typeof(IncidentModel), 200)]
        public async Task<IActionResult> GetById(Guid incidentId)
        {
            try
            {
                var incident = await _behaviourService.GetIncidentById(incidentId);

                if (await AuthoriseStudent(incident.StudentId))
                {
                    return Ok(incident);
                }

                return Forbid();
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet]
        [Route("student", Name = "ApiIncidentGetByStudent")]
        [Permission(PermissionValue.BehaviourViewIncidents)]
        [ProducesResponseType(typeof(IEnumerable<IncidentCollectionModel>), 200)]
        public async Task<IActionResult> GetByStudent([FromQuery] Guid studentId, [FromQuery] Guid? academicYearId)
        {
            try
            {
                if (await AuthoriseStudent(studentId))
                {
                    var fromAcademicYearId = academicYearId ?? (await _academicYearService.GetCurrentAcademicYear(true)).Id.Value;

                    var incidents = await _behaviourService.GetIncidentsByStudent(studentId, fromAcademicYearId);

                    return Ok(incidents.Select(x => x.ToListModel()));
                }

                return Forbid();
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost]
        [Authorize(Policy = Policies.UserType.Staff)]
        [Permission(PermissionValue.BehaviourEditIncidents)]
        [Route("create", Name = "ApiIncidentCreate")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Create([FromBody] CreateIncidentModel model)
        {
            try
            {
                var user = await UserService.GetUserByPrincipal(User);

                model.CreatedById = user.Id.Value;

                await _behaviourService.CreateIncident(model);

                return Ok();
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPut]
        [Authorize(Policy = Policies.UserType.Staff)]
        [Permission(PermissionValue.BehaviourEditIncidents)]
        [Route("update", Name = "ApiIncidentUpdate")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Update([FromBody] UpdateIncidentModel model)
        {
            try
            {
                await _behaviourService.UpdateIncident(model);

                return Ok();
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpDelete]
        [Authorize(Policy = Policies.UserType.Staff)]
        [Permission(PermissionValue.BehaviourEditIncidents)]
        [Route("delete", Name = "ApiIncidentDelete")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Delete([FromQuery] Guid incidentId)
        {
            try
            {
                await _behaviourService.DeleteIncident(incidentId);
                return Ok();
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}
