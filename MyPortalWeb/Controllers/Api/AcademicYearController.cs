using System;
using System.Threading.Tasks;
using Duende.IdentityServer.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Database.Enums;
using MyPortal.Logic.Attributes;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Requests.Curriculum;
using MyPortalWeb.Controllers.BaseControllers;

namespace MyPortalWeb.Controllers.Api
{
    [Authorize]
    [Route("api/academicYears")]
    public sealed class AcademicYearController : BaseApiController
    {
        private readonly IAcademicYearService _academicYearService;

        public AcademicYearController(IUserService userService, IAcademicYearService academicYearService) : base(userService)
        {
            _academicYearService = academicYearService;
        }

        [HttpPost]
        [Route("")]
        [Permission(PermissionValue.CurriculumAcademicStructure)]
        [ProducesResponseType(200)]
        public async Task<IActionResult> CreateAcademicYear([FromBody] AcademicYearRequestModel requestModel)
        {
            try
            {
                await _academicYearService.CreateAcademicYear(requestModel);
                
                return Ok();
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}
