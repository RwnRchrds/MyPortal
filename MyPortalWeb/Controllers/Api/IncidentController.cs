using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Logic.Constants;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Entity;

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
            });
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
            });
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
            });
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
            });
        }

        [HttpDelete]
        [Authorize(Policy = Policies.UserType.Staff)]
        [Route("delete", Name = "ApiIncidentDelete")]
        public async Task<IActionResult> Delete([FromQuery] Guid incidentId)
        {
            await _incidentService.Delete(incidentId);

            return Ok("Incident deleted successfully.");
        }

        public override void Dispose()
        {
            _incidentService.Dispose();
        }
    }
}
