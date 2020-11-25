using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Database.Permissions;
using MyPortal.Logic.Constants;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Entity;
using MyPortalWeb.Controllers.BaseControllers;

namespace MyPortalWeb.Controllers.Api
{
    public class IncidentController : StudentApiController
    {
        private readonly IIncidentService _incidentService;

        public IncidentController(IUserService userService, IAcademicYearService academicYearService, IStudentService studentService, IIncidentService incidentService) : base(userService, academicYearService, studentService)
        {
            _incidentService = incidentService;
        }

        [HttpGet]
        [Route("get", Name = "ApiIncidentGetById")]
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
        [Route("getByStudent", Name = "ApiIncidentGetByStudent")]
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
        public async Task<IActionResult> Create([FromForm] IncidentModel model)
        {
            return await ProcessAsync(async () =>
            {
                var user = await UserService.GetUserByPrincipal(User);

                model.RecordedById = user.Id;

                await _incidentService.Create(model);

                return Ok("Incident created successfully.");
            }, Permissions.Behaviour.Incidents.EditIncidents);
        }

        [HttpPut]
        [Authorize(Policy = Policies.UserType.Staff)]
        [Route("update", Name = "ApiIncidentUpdate")]
        public async Task<IActionResult> Update([FromForm] IncidentModel model)
        {
            return await ProcessAsync(async () =>
            {
                await _incidentService.Update(model);

                return Ok("Incident updated successfully.");
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

                return Ok("Incident deleted successfully.");
            }, Permissions.Behaviour.Incidents.EditIncidents);
        }

        public override void Dispose()
        {
            _incidentService.Dispose();
        }
    }
}
