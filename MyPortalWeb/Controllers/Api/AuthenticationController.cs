using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Requests.Auth;
using MyPortalWeb.Controllers.BaseControllers;

namespace MyPortalWeb.Controllers.Api
{
    [Route("api/auth")]
    public class AuthenticationController : BaseApiController
    {
        public AuthenticationController(IUserService userService, IRoleService roleService) : base(userService,
            roleService)
        {
        }

        [HttpGet]
        [Authorize]
        [Route("permissions")]
        [ProducesResponseType(typeof(IEnumerable<int>), 200)]
        public async Task<IActionResult> GetEffectivePermissions()
        {
            try
            {
                var user = await GetLoggedInUser();

                var effectivePermissions = await UserService.GetPermissionValues(user.Id.Value);

                return Ok(effectivePermissions);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}