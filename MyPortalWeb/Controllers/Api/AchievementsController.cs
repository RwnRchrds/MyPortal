using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Database.Enums;
using MyPortal.Logic.Constants;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.DataGrid;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Requests.Behaviour.Achievements;
using MyPortalWeb.Controllers.BaseControllers;

namespace MyPortalWeb.Controllers.Api
{
    [Authorize]
    [Route("api/behaviour/achievements")]
    public class AchievementsController : StudentApiController
    {
        public AchievementsController(IAppServiceCollection services) : base(services)
        {
        }

        [HttpGet]
        [Route("id", Name = "ApiAchievementGetById")]
        [ProducesResponseType(typeof(AchievementModel), 200)]
        public async Task<IActionResult> GetById([FromQuery] Guid achievementId)
        {
            return await ProcessAsync(async () =>
            {
                var achievement = await Services.Achievements.GetAchievementById(achievementId);

                if (await AuthoriseStudent(achievement.StudentId))
                {
                    return Ok(achievement);
                }

                return Forbid();
            }, PermissionValue.BehaviourViewAchievements);
        }

        [HttpGet]
        [Route("student", Name = "ApiAchievementGetByStudent")]
        [ProducesResponseType(typeof(AchievementDataGridModel), 200)]
        public async Task<IActionResult> GetByStudent([FromQuery] Guid studentId, [FromQuery] Guid? academicYearId)
        {
            return await ProcessAsync(async () =>
            {
                if (await AuthoriseStudent(studentId))
                {
                    var fromAcademicYearId = academicYearId ?? (await Services.AcademicYears.GetCurrentAcademicYear(true)).Id;

                    var achievements = await Services.Achievements.GetAchievementsByStudent(studentId, fromAcademicYearId);

                    return Ok(achievements.Select(x => x.ToListModel()));
                }

                return Forbid();
            }, PermissionValue.BehaviourViewAchievements);
        }

        [HttpPost]
        [Authorize(Policy = Policies.UserType.Staff)]
        [Route("create", Name = "ApiAchievementCreate")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Create([FromBody] CreateAchievementModel model)
        {
            return await ProcessAsync(async () =>
            {
                var user = await Services.Users.GetUserByPrincipal(User);

               var request = new AchievementModel(model, user.Id);

               await Services.Achievements.CreateAchievement(request);

                return Ok();
            }, PermissionValue.BehaviourEditAchievements);
        }

        [HttpPut]
        [Authorize(Policy = Policies.UserType.Staff)]
        [Route("update", Name = "ApiAchievementUpdate")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Update([FromBody] UpdateAchievementModel model)
        {
            return await ProcessAsync(async () =>
            {
                await Services.Achievements.UpdateAchievement(model);

                return Ok();
            }, PermissionValue.BehaviourEditAchievements);
        }

        [HttpDelete]
        [Authorize(Policy = Policies.UserType.Staff)]
        [Route("delete", Name = "ApiAchievementDelete")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Delete([FromQuery] Guid achievementId)
        {
            return await ProcessAsync(async () =>
            {
                await Services.Achievements.DeleteAchievement(achievementId);

                return Ok();
            }, PermissionValue.BehaviourEditAchievements);
        }
    }
}