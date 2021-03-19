using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Logic.Caching;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Requests.Curriculum;
using MyPortalWeb.Controllers.BaseControllers;

namespace MyPortalWeb.Controllers.Api
{
    [Authorize]
    [Route("api/academicYears")]
    public class AcademicYearController : BaseApiController
    {
        public AcademicYearController(IAppServiceCollection services,
            IRolePermissionsCache rolePermissionsCache) : base(services, rolePermissionsCache)
        {

        }

        [HttpPost]
        [Route("create")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> CreateAcademicYear([FromBody] CreateAcademicYearModel model)
        {
            return await ProcessAsync(async () =>
            {
                await Services.AcademicYears.CreateAcademicYear(model);

                return Ok();
            });
        }

        [HttpPost]
        [Route("generate")]
        [ProducesResponseType(typeof(CreateAcademicTermModel), 200)]
        public async Task<IActionResult> GenerateAttendanceWeeks([FromBody] CreateAcademicTermModel model)  
        {
            return await ProcessAsync(async () =>
            {
                var generatedModel = Services.AcademicYears.GenerateAttendanceWeeks(model);

                return Ok(generatedModel);
            });
        }
    }
}
