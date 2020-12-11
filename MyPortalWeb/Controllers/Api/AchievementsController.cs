using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Database.Permissions;
using MyPortal.Logic.Caching;
using MyPortal.Logic.Constants;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.DataGrid;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Requests.Behaviour;
using MyPortal.Logic.Models.Requests.Behaviour.Achievements;
using MyPortalWeb.Controllers.BaseControllers;

namespace MyPortalWeb.Controllers.Api
{
    [Authorize]
    [Route("api/behaviour/achievements")]
    public class AchievementsController : StudentApiController
    {
        private readonly IAchievementService _achievementService;

        public AchievementsController(IUserService userService, IAcademicYearService academicYearService,
            IRolePermissionsCache rolePermissionsCache, IStudentService studentService,
            IAchievementService achievementService) : base(userService, academicYearService, rolePermissionsCache,
            studentService)
        {
            _achievementService = achievementService;
        }

        [HttpGet]
        [Route("id", Name = "ApiAchievementGetById")]
        [Produces(typeof(AchievementModel))]
        public async Task<IActionResult> GetById([FromQuery] Guid achievementId)
        {
            return await ProcessAsync(async () =>
            {
                var achievement = await _achievementService.GetById(achievementId);

                if (await AuthenticateStudent(achievement.StudentId))
                {
                    return Ok(achievement);
                }

                return Forbid();
            }, Permissions.Behaviour.Achievements.ViewAchievements);
        }

        [HttpGet]
        [Route("student", Name = "ApiAchievementGetByStudent")]
        [Produces(typeof(AchievementDataGridModel))]
        public async Task<IActionResult> GetByStudent([FromQuery] Guid studentId, [FromQuery] Guid? academicYearId)
        {
            return await ProcessAsync(async () =>
            {
                if (await AuthenticateStudent(studentId))
                {
                    var fromAcademicYearId = academicYearId ?? await GetCurrentAcademicYearId();

                    var achievements = await _achievementService.GetByStudent(studentId, fromAcademicYearId);

                    return Ok(achievements.Select(x => x.ToListModel()));
                }

                return Forbid();
            }, Permissions.Behaviour.Achievements.ViewAchievements);
        }

        [HttpPost]
        [Authorize(Policy = Policies.UserType.Staff)]
        [Route("create", Name = "ApiAchievementCreate")]
        public async Task<IActionResult> Create([FromBody] CreateAchievementModel model)
        {
            return await ProcessAsync(async () =>
            {
                var user = await UserService.GetUserByPrincipal(User);

               var request = new AchievementModel(model, user.Id);

               await _achievementService.Create(request);

                return Ok("Achievement created.");
            }, Permissions.Behaviour.Achievements.EditAchievements);
        }

        [HttpPut]
        [Authorize(Policy = Policies.UserType.Staff)]
        [Route("update", Name = "ApiAchievementUpdate")]
        public async Task<IActionResult> Update([FromBody] UpdateAchievementModel model)
        {
            return await ProcessAsync(async () =>
            {
                var user = await UserService.GetUserByPrincipal(User);

                var request = new AchievementModel(model, user.Id);

                await _achievementService.Update(request);

                return Ok("Achievement updated.");
            }, Permissions.Behaviour.Achievements.EditAchievements);
        }

        [HttpDelete]
        [Authorize(Policy = Policies.UserType.Staff)]
        [Route("delete", Name = "ApiAchievementDelete")]
        public async Task<IActionResult> Delete([FromQuery] Guid achievementId)
        {
            return await ProcessAsync(async () =>
            {
                await _achievementService.Delete(achievementId);

                return Ok("Achievement deleted.");
            }, Permissions.Behaviour.Achievements.EditAchievements);
        }

        public override void Dispose()
        {
            _achievementService.Dispose();

            base.Dispose();
        }
    }
}