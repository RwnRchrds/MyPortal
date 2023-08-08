using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Database.Enums;
using MyPortal.Logic.Attributes;
using MyPortal.Logic.Constants;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Data.Behaviour.Achievements;
using MyPortal.Logic.Models.Data.Students;
using MyPortal.Logic.Models.Requests.Behaviour.Achievements;
using MyPortal.Logic.Models.Requests.Behaviour.Incidents;
using MyPortal.Logic.Models.Summary;
using MyPortalWeb.Controllers.BaseControllers;

namespace MyPortalWeb.Controllers.Api
{
    [Authorize]
    [Route("api/behaviour")]
    public class BehaviourController : PersonalDataController
    {
        private readonly IBehaviourService _behaviourService;
        private readonly IAcademicYearService _academicYearService;

        public BehaviourController(IUserService userService, IPersonService personService,
            IStudentService studentService, IBehaviourService behaviourService,
                IAcademicYearService academicYearService)
            : base(userService, personService, studentService)
        {
            _academicYearService = academicYearService;
            _behaviourService = behaviourService;
        }

        [HttpGet]
        [Route("achievements/{achievementId}")]
        [Permission(PermissionValue.BehaviourViewAchievements)]
        [ProducesResponseType(typeof(AchievementModel), 200)]
        public async Task<IActionResult> GetAchievementById([FromRoute] Guid achievementId)
        {
            try
            {
                var achievement = await _behaviourService.GetStudentAchievementById(achievementId);

                var student = await StudentService.GetStudentById(achievement.StudentId);

                if (await CanAccessPerson(student.PersonId))
                {
                    return Ok(achievement);
                }

                return PermissionError();
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet]
        [Route("students/{studentId}/achievements", Name = "ApiAchievementGetByStudent")]
        [Permission(PermissionValue.BehaviourViewAchievements)]
        [ProducesResponseType(typeof(IEnumerable<StudentAchievementSummaryModel>), 200)]
        public async Task<IActionResult> GetAchievementsByStudent([FromRoute] Guid studentId,
            [FromQuery] Guid? academicYearId)
        {
            try
            {
                var student = await StudentService.GetStudentById(studentId);
                
                if (await CanAccessPerson(student.PersonId))
                {
                    var fromAcademicYearId = academicYearId 
                                             ?? (await _academicYearService.GetCurrentAcademicYear(true)).Id;

                    if (fromAcademicYearId == null)
                    {
                        return Error(HttpStatusCode.BadRequest, "No academic year is currently selected.");
                    }
                    
                    var achievements = 
                        await _behaviourService.GetAchievementsByStudent(studentId, fromAcademicYearId.Value);

                    return Ok(achievements);
                }

                return PermissionError();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpPost]
        [Authorize(Policy = Policies.UserType.Staff)]
        [Permission(PermissionValue.BehaviourEditAchievements)]
        [Route("achievements", Name = "ApiAchievementCreate")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> CreateAchievement([FromBody] AchievementRequestModel model)
        {
            try
            {
                await _behaviourService.CreateAchievement(model);

                return Ok();
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPut]
        [Authorize(Policy = Policies.UserType.Staff)]
        [Permission(PermissionValue.BehaviourEditAchievements)]
        [Route("achievements/{achievementId}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> UpdateAchievement([FromQuery] Guid achievementId, [FromBody] AchievementRequestModel achievement)
        {
            try
            {
                await _behaviourService.UpdateAchievement(achievementId, achievement);

                return Ok();
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpDelete]
        [Authorize(Policy = Policies.UserType.Staff)]
        [Permission(PermissionValue.BehaviourEditAchievements)]
        [Route("achievements/{achievementId}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> DeleteAchievement([FromRoute] Guid achievementId)
        {
            try
            {
                await _behaviourService.DeleteAchievement(achievementId);

                return Ok();
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        
        [HttpGet]
        [Route("incidents/{incidentId}", Name = "ApiIncidentGetById")]
        [Permission(PermissionValue.BehaviourViewIncidents)]
        [ProducesResponseType(typeof(StudentIncidentModel), 200)]
        public async Task<IActionResult> GetIncidentById([FromRoute] Guid incidentId)
        {
            try
            {
                var incident = await _behaviourService.GetIncidentById(incidentId);
                
                var student = await StudentService.GetStudentById(incident.StudentId);

                if (await CanAccessPerson(student.PersonId))
                {
                    return Ok(incident);
                }

                return PermissionError();
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet]
        [Route("students/{studentId}/incidents")]
        [Permission(PermissionValue.BehaviourViewIncidents)]
        [ProducesResponseType(typeof(IEnumerable<StudentIncidentSummaryModel>), 200)]
        public async Task<IActionResult> GetIncidentsByStudent([FromRoute] Guid studentId, [FromQuery] Guid? academicYearId)
        {
            try
            {
                var student = await StudentService.GetStudentById(studentId);
                
                if (await CanAccessPerson(student.PersonId))
                {
                    var fromAcademicYearId = academicYearId 
                                             ?? (await _academicYearService.GetCurrentAcademicYear(true)).Id;

                    if (fromAcademicYearId == null)
                    {
                        return Error(HttpStatusCode.BadRequest, "No academic year is currently selected.");
                    }
                    
                    var incidents = 
                        (await _behaviourService.GetIncidentsByStudent(studentId, fromAcademicYearId.Value)).ToList();

                    return Ok(incidents);
                }

                return PermissionError();
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost]
        [Authorize(Policy = Policies.UserType.Staff)]
        [Permission(PermissionValue.BehaviourEditIncidents)]
        [Route("incidents")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Create([FromBody] IncidentRequestModel model)
        {
            try
            {
                await _behaviourService.CreateIncident(model);

                return Ok();
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost]
        [Authorize(Policy = Policies.UserType.Staff)]
        [Permission(PermissionValue.BehaviourEditIncidents)]
        [Route("incidents/{incidentId}", Name = "ApiIncidentUpdate")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Update([FromRoute] Guid incidentId, [FromBody] IncidentRequestModel requestModel)
        {
            try
            {
                await _behaviourService.UpdateIncident(incidentId, requestModel);

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
        [Route("incidents/{incidentId}", Name = "ApiIncidentDelete")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Delete([FromRoute] Guid incidentId) {
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