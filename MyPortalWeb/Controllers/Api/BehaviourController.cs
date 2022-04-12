using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Database.Enums;
using MyPortal.Logic.Constants;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Requests.Behaviour.Achievements;
using MyPortal.Logic.Models.Requests.Behaviour.Incidents;
using MyPortal.Logic.Models.Summary;
using MyPortalWeb.Attributes;
using MyPortalWeb.Controllers.BaseControllers;

namespace MyPortalWeb.Controllers.Api
{
    [Authorize]
    [Route("api/behaviour")]
    public class BehaviourController : PersonalDataController
    {
        private IBehaviourService _behaviourService;
        private IAcademicYearService _academicYearService;

        public BehaviourController(IStudentService studentService, IPersonService personService, IUserService userService,
            IRoleService roleService, IBehaviourService behaviourService, IAcademicYearService academicYearService)
            : base(studentService, personService, userService, roleService)
        {
            _behaviourService = behaviourService;
            _academicYearService = academicYearService;
        }

        [HttpGet]
        [Route("achievements/{achievementId}", Name = "ApiAchievementGetById")]
        [Permission(PermissionValue.BehaviourViewAchievements)]
        [ProducesResponseType(typeof(AchievementModel), 200)]
        public async Task<IActionResult> GetAchievementById([FromRoute] Guid achievementId)
        {
            try
            {
                var achievement = await _behaviourService.GetAchievementById(achievementId);

                var student = await StudentService.GetById(achievement.StudentId);

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
        [Route("achievements/student/{studentId}", Name = "ApiAchievementGetByStudent")]
        [Permission(PermissionValue.BehaviourViewAchievements)]
        [ProducesResponseType(typeof(IEnumerable<StudentAchievementSummaryModel>), 200)]
        public async Task<IActionResult> GetAchievementsByStudent([FromRoute] Guid studentId, [FromQuery] Guid? academicYearId)
        {
            try
            {
                var student = await StudentService.GetById(studentId);
                
                if (await CanAccessPerson(student.PersonId))
                {
                    var fromAcademicYearId = academicYearId ?? (await _academicYearService.GetCurrentAcademicYear(true)).Id.Value;

                    var achievements = await _behaviourService.GetAchievementsByStudent(studentId, fromAcademicYearId);

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
        [Route("achievements/create", Name = "ApiAchievementCreate")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> CreateAchievement([FromBody] CreateAchievementModel model)
        {
            try
            {
                var user = await GetLoggedInUser();
                
                await _behaviourService.CreateAchievement(user.Id.Value, model);

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
        [Route("achievements/update", Name = "ApiAchievementUpdate")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> UpdateAchievement([FromBody] UpdateAchievementModel model)
        {
            try
            {
                await _behaviourService.UpdateAchievement(model);

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
        [Route("achievements/delete/{achievementId}", Name = "ApiAchievementDelete")]
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
                
                var student = await StudentService.GetById(incident.StudentId);

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
        [Route("incidents/student/{studentId}", Name = "ApiIncidentGetByStudent")]
        [Permission(PermissionValue.BehaviourViewIncidents)]
        [ProducesResponseType(typeof(IEnumerable<StudentIncidentSummaryModel>), 200)]
        public async Task<IActionResult> GetIncidentsByStudent([FromRoute] Guid studentId, [FromQuery] Guid? academicYearId)
        {
            try
            {
                var student = await StudentService.GetById(studentId);
                
                if (await CanAccessPerson(student.PersonId))
                {
                    var fromAcademicYearId = academicYearId ?? (await _academicYearService.GetCurrentAcademicYear(true)).Id.Value;

                    var incidents = (await _behaviourService.GetIncidentsByStudent(studentId, fromAcademicYearId)).ToList();

                    return Ok(incidents);
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
        [Route("incidents/create", Name = "ApiIncidentCreate")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Create([FromBody] CreateIncidentModel model)
        {
            try
            {
                var user = await GetLoggedInUser();
                
                await _behaviourService.CreateIncident(user.Id.Value, model);

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
        [Route("incidents/update", Name = "ApiIncidentUpdate")]
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
        [Route("incidents/delete/{incidentId}", Name = "ApiIncidentDelete")]
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