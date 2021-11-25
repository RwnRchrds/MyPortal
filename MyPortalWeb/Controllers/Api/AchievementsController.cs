using System;
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
using MyPortal.Logic.Models.Requests.Behaviour.Achievements;
using MyPortalWeb.Attributes;
using MyPortalWeb.Controllers.BaseControllers;

namespace MyPortalWeb.Controllers.Api
{
    [Authorize]
    [Route("api/behaviour/achievements")]
    public class AchievementsController : PersonalDataController
    {
        private IBehaviourService _behaviourService;
        private IAcademicYearService _academicYearService;

        public AchievementsController(IStudentService studentService, IPersonService personService, IUserService userService,
            IRoleService roleService, IBehaviourService behaviourService, IAcademicYearService academicYearService)
            : base(studentService, personService, userService, roleService)
        {
            _behaviourService = behaviourService;
            _academicYearService = academicYearService;
        }

        [HttpGet]
        [Route("id", Name = "ApiAchievementGetById")]
        [Permission(PermissionValue.BehaviourViewAchievements)]
        [ProducesResponseType(typeof(AchievementModel), 200)]
        public async Task<IActionResult> GetById([FromQuery] Guid achievementId)
        {
            try
            {
                var achievement = await _behaviourService.GetAchievementById(achievementId);

                var student = await StudentService.GetById(achievement.StudentId);

                if (await AuthorisePerson(student.PersonId))
                {
                    return Ok(achievement);
                }

                return Error(403, PermissionMessage);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet]
        [Route("student", Name = "ApiAchievementGetByStudent")]
        [Permission(PermissionValue.BehaviourViewAchievements)]
        [ProducesResponseType(typeof(AchievementCollectionModel), 200)]
        public async Task<IActionResult> GetByStudent([FromQuery] Guid studentId, [FromQuery] Guid? academicYearId)
        {
            try
            {
                var student = await StudentService.GetById(studentId);
                
                if (await AuthorisePerson(student.PersonId))
                {
                    var fromAcademicYearId = academicYearId ?? (await _academicYearService.GetCurrentAcademicYear(true)).Id.Value;

                    var achievements = await _behaviourService.GetAchievementsByStudent(studentId, fromAcademicYearId);

                    return Ok(achievements.Select(x => x.ToListModel()));
                }

                return Error(403, PermissionMessage);
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
        [Route("create", Name = "ApiAchievementCreate")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Create([FromBody] CreateAchievementModel model)
        {
            try
            {
                var user = await UserService.GetUserByPrincipal(User);

                model.CreatedById = user.Id.Value;

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
        [Route("update", Name = "ApiAchievementUpdate")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Update([FromBody] UpdateAchievementModel model)
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
        [Route("delete", Name = "ApiAchievementDelete")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Delete([FromQuery] Guid achievementId)
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
    }
}