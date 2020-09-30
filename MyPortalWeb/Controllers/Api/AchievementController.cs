using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Logic.Constants;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Entity;

namespace MyPortalWeb.Controllers.Api
{
    [Authorize]
    [Route("api/behaviour/achievement")]
    public class AchievementController : BaseApiController
    {
        private readonly IAchievementService _achievementService;
        private IStudentService _studentService;

        public AchievementController(IUserService userService, IAcademicYearService academicYearService, IAchievementService achievementService, IStudentService studentService) : base(
            userService, academicYearService)
        {
            _achievementService = achievementService;
            _studentService = studentService;
        }
        
        [HttpGet]
        [Route("get", Name = "ApiAchievementGetById")]
        public async Task<IActionResult> GetById([FromQuery] Guid achievementId)
        {
            return await ProcessAsync(async () =>
            {
                var achievement = await _achievementService.GetById(achievementId);

                if (await AuthenticateStudent(_studentService, achievement.StudentId))
                {
                    return Ok(achievement);
                }

                return Forbid();
            });
        }

        [HttpGet]
        [Route("getByStudent", Name = "ApiAchievementGetByStudent")]
        public async Task<IActionResult> GetByStudent([FromQuery] Guid studentId, [FromQuery] Guid? academicYearId)
        {
            return await ProcessAsync(async () =>
            {
                if (await AuthenticateStudent(_studentService, studentId))
                {
                    var fromAcademicYearId = academicYearId ?? await GetCurrentAcademicYearId();

                    var achievements = await _achievementService.GetByStudent(studentId, fromAcademicYearId);

                    return Ok(achievements.Select(x => x.ToListModel()));
                }

                return Forbid();
            });
        }

        [HttpPost]
        [Authorize(Policy = Policies.UserType.Staff)]
        [Route("create", Name = "ApiAchievementCreate")]
        public async Task<IActionResult> Create([FromForm] AchievementModel model)
        {
            return await ProcessAsync(async () =>
            {
                var user = await UserService.GetUserByPrincipal(User);

                model.RecordedById = user.Id;

                await _achievementService.Create(model);

                return Ok("Achievement created successfully.");
            });
        }

        [HttpPut]
        [Authorize(Policy = Policies.UserType.Staff)]
        [Route("update", Name = "ApiAchievementUpdate")]
        public async Task<IActionResult> Update([FromForm] AchievementModel model)
        {
            return await ProcessAsync(async () =>
            {
                await _achievementService.Update(model);

                return Ok("Achievement updated successfully.");
            });
        }

        [HttpDelete]
        [Authorize(Policy = Policies.UserType.Staff)]
        [Route("delete", Name = "ApiAchievementDelete")]
        public async Task<IActionResult> Delete([FromQuery] Guid achievementId)
        {
            return await ProcessAsync(async () =>
            {
                await _achievementService.Delete(achievementId);

                return Ok("Achievement deleted successfully.");
            });
        }

        public override void Dispose()
        {
            _achievementService.Dispose();
            _studentService.Dispose();
            
            base.Dispose();
        }
    }
}