using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Database.Enums;
using MyPortal.Logic.Attributes;
using MyPortal.Logic.Extensions;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Data.Settings;
using MyPortal.Logic.Models.Requests.Settings.Users;
using MyPortalWeb.Controllers.BaseControllers;
using MyPortalWeb.Models.Response;

namespace MyPortalWeb.Controllers.Api
{
    [Authorize]
    [Route("api/users")]
    public class UsersController : BaseApiController
    {
        private readonly IUserService _userService;
        
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        

        [HttpPost]
        [Route("")]
        [Permission(PermissionValue.SystemEditUsers)]
        [ProducesResponseType(typeof(NewEntityResponseModel), 200)]
        public async Task<IActionResult> CreateUser([FromBody] UserRequestModel model)
        {
            try
            {
                var userId = (await _userService.CreateUser(model)).FirstOrDefault();

                return Ok(new NewEntityResponseModel { Id = userId });
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet]
        [Route("")]
        [Permission(PermissionValue.SystemViewUsers)]
        [ProducesResponseType(typeof(IEnumerable<UserModel>), 200)]
        public async Task<IActionResult> GetUsers([FromQuery] string username)
        {
            try
            {
                var users = await _userService.GetUsers(username);

                return Ok(users);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet]
        [Route("{userId}")]
        [Permission(PermissionValue.SystemViewUsers)]
        [ProducesResponseType(typeof(UserModel), 200)]
        public async Task<IActionResult> GetUserById([FromRoute] Guid userId)
        {
            try
            {
                var user = await _userService.GetUserById(userId);

                return Ok(user);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet]
        [Route("{userId}/roles")]
        [ProducesResponseType(typeof(IEnumerable<RoleModel>), 200)]
        public async Task<IActionResult> GetUserRoles([FromRoute] Guid userId)
        {
            try
            {
                var roles = await _userService.GetUserRoles(userId);

                return Ok(roles);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPut]
        [Route("{userId}")]
        [Permission(PermissionValue.SystemEditUsers)]
        [ProducesResponseType(200)]
        public async Task<IActionResult> UpdateUser([FromRoute] Guid userId, [FromBody] UserRequestModel model)
        {
            try
            {
                await _userService.UpdateUser(userId, model);

                return Ok();
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpDelete]
        [Route("{userId}")]
        [Permission(PermissionValue.SystemEditUsers)]
        [ProducesResponseType(200)]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid userId)
        {
            try
            {
                await _userService.DeleteUser(userId);

                return Ok();
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPut]
        [Route("{userId}/password")]
        [Permission(PermissionValue.SystemEditUsers)]
        [ProducesResponseType(200)]
        public async Task<IActionResult> SetPassword([FromRoute] Guid userId,
            [FromBody] SetPasswordRequestModel request)
        {
            try
            {
                await _userService.SetPassword(userId, request.NewPassword);

                return Ok();
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPut]
        [Route("{userId}/enabled")]
        [Permission(PermissionValue.SystemEditUsers)]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<IActionResult> SetEnabled([FromRoute] Guid userId,
            [FromBody] SetUserEnabledRequestModel model)
        {
            try
            {
                await _userService.SetUserEnabled(userId, model.Enabled);

                return Ok(model.Enabled);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}