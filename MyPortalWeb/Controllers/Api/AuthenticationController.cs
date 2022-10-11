using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Logic.Extensions;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Response.Users;
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
        [Route("userInfo")]
        [ProducesResponseType(typeof(UserInfoModel), 200)]
        public async Task<IActionResult> GetUserInfo()
        {
            try
            {
                var userId = User.GetUserId();

                var userInfo = await UserService.GetUserInfo(userId);

                return Ok(userInfo);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}