using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;
using MyPortalWeb.Controllers.BaseControllers;

namespace MyPortalWeb.Controllers.Api
{
    [Route("api/schools")]
    public class SchoolsController : BaseApiController
    {
        private ISchoolService _schoolService;

        public SchoolsController(IUserService userService, IRoleService roleService, ISchoolService schoolService) :
            base(userService, roleService)
        {
            _schoolService = schoolService;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("local/name")]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> GetLocalSchoolName()
        {
            try
            {
                var schoolName = await _schoolService.GetLocalSchoolName();

                return Ok(schoolName);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}
