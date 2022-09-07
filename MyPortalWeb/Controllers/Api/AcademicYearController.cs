using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Database.Enums;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Requests.Curriculum;
using MyPortalWeb.Attributes;
using MyPortalWeb.Controllers.BaseControllers;

namespace MyPortalWeb.Controllers.Api
{
    [Authorize]
    [Route("api/academicYears")]
    public class AcademicYearController : BaseApiController
    {
        private IAcademicYearService _academicYearService;

        public AcademicYearController(IUserService userService, IRoleService roleService, IAcademicYearService academicYearService) : base(userService, roleService)
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
