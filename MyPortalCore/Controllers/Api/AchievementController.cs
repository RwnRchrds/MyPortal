using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Logic.Constants;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Entity;

namespace MyPortalCore.Controllers.Api
{
    [Route("api/student/achievement")]
    public class AchievementController : BaseApiController
    {
        private readonly IAchievementService _achievementService;
        private readonly IStudentService _studentService;
        
        public AchievementController(IApplicationUserService userService, IAchievementService achievementService, IStudentService studentService) : base(userService)
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

                if (await AuthenticateStudentResource(_studentService, achievement.StudentId))
                {
                    return Ok(achievement);
                }

                return Forbid();
            });
        }

        [HttpGet]
        [Route("getByStudent", Name = "ApiAchievementGetByStudent")]
        public async Task<IActionResult> GetByStudent([FromQuery] Guid studentId)
        {
            return await ProcessAsync(async () =>
            {
                if (await AuthenticateStudentResource(_studentService, studentId))
                {
                    var user = await _userService.GetUserByPrincipal(User);

                    var academicYearId = await GetSelectedAcademicYearId();

                    var achievements = await _achievementService.GetByStudent(studentId, academicYearId);

                    return Ok(achievements);
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
                var user = await _userService.GetUserByPrincipal(User);

                if (user.SelectedAcademicYearId == null)
                {
                    return BadRequest("No academic year has been selected.");
                }

                model.RecordedById = user.Id;
                model.AcademicYearId = user.SelectedAcademicYearId.Value;

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
        }
    }
}