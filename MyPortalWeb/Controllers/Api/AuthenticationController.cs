using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        [ProducesResponseType(typeof(UserInfoResponseModel), 200)]
        public async Task<IActionResult> GetUserInfo()
        {
            try
            {
                var user = await GetLoggedInUser();

                var userInfo = await UserService.GetUserInfo(user.Id.Value);

                return Ok(userInfo);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}