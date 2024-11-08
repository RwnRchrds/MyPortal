using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Logic.Extensions;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Data.Settings;
using MyPortal.Logic.Models.Requests.Settings.Users;
using MyPortalWeb.Controllers.BaseControllers;

namespace MyPortalWeb.Controllers.Api
{
    [Route("api/auth")]
    public class AuthenticationController : BaseApiController
    {
        private readonly IUserService _userService;
        
        public AuthenticationController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Authorize]
        [Route("userInfo")]
        [ProducesResponseType(typeof(UserInfoModel), 200)]
        public async Task<IActionResult> GetUserInfo()
        {
            try
            {
                var userInfo = await _userService.GetUserInfo();

                return Ok(userInfo);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPut]
        [Authorize]
        [Route("password")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequestModel model)
        {
            try
            {
                var userId = User.GetUserId();

                if (userId != null)
                {
                    await _userService.ChangePassword(userId.Value, model.CurrentPassword, model.NewPassword);
                    return Ok();
                }

                return Unauthorized();
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}