using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Logic.Caching;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Requests.Curriculum;
using MyPortalWeb.Controllers.BaseControllers;

namespace MyPortalWeb.Controllers.Api
{
    [Authorize]
    [Route("api/academicYears")]
    public class AcademicYearController : BaseApiController
    {
        public AcademicYearController(IUserService userService, IAcademicYearService academicYearService,
            IRolePermissionsCache rolePermissionsCache) : base(userService, academicYearService, rolePermissionsCache)
        {

        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateAcademicYear([FromBody] CreateAcademicYearModel model)
        {
            return await ProcessAsync(async () =>
            {
                await AcademicYearService.CreateAcademicYear(model);

                return Ok();
            });
        }

        [HttpPost]
        [Route("generate")]
        public async Task<IActionResult> GenerateAttendanceWeeks([FromBody] CreateAcademicTermModel model)  
        {
            return await ProcessAsync(async () =>
            {
                var generatedModel = AcademicYearService.GenerateAttendanceWeeks(model);

                return Ok(generatedModel);
            });
        }
    }
}
