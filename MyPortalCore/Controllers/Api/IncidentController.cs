using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Logic.Constants;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Entity;

namespace MyPortalCore.Controllers.Api
{
    [Microsoft.AspNetCore.Components.Route("api/student/incident")]
    public class IncidentController : BaseApiController
    {
        private readonly IIncidentService _incidentService;
        private readonly IStudentService _studentService;
        
        public IncidentController(IApplicationUserService userService, IIncidentService incidentService, IStudentService studentService) : base(userService)
        {
            _incidentService = incidentService;
            _studentService = studentService;
        }

        [HttpGet]
        [Route("get", Name = "ApiIncidentGetById")]
        public async Task<IActionResult> GetById(Guid incidentId)
        {
            return await ProcessAsync(async () =>
            {
                var incident = await _incidentService.GetById(incidentId);

                if (await AuthenticateStudentResource(_studentService, incident.StudentId))
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
                if (await AuthenticateStudentResource(_studentService, studentId))
                {
                    var academicYearId = await GetSelectedAcademicYearId();

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
                var user = await _userService.GetUserByPrincipal(User);

                if (user.SelectedAcademicYearId == null)
                {
                    return BadRequest("No academic year has been selected.");
                }

                model.RecordedById = user.Id;
                model.AcademicYearId = user.SelectedAcademicYearId.Value;

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