using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MyPortal.Database.Enums;
using MyPortal.Logic.Extensions;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Requests.Settings.Users;
using MyPortalWeb.Attributes;
using MyPortalWeb.Controllers.BaseControllers;
using MyPortalWeb.Models;
using MyPortalWeb.Models.Response;

namespace MyPortalWeb.Controllers.Api
{ 
    [Authorize]
    [Route("api/users")]
    public class UsersController : BaseApiController
    {
        public UsersController(IUserService userService, IRoleService roleService) : base(userService, roleService)
        {
        }

        private async Task<bool> AuthoriseUser(Guid requestedUserId, bool requireEdit = false)
        {
            // Users do not require extra permission to access resources related to themselves
            return await UserHasPermission(requireEdit
                       ? PermissionValue.SystemEditUsers
                       : PermissionValue.SystemViewUsers) ||
                   User.GetUserId() == requestedUserId;
        }

        [HttpPost]
        [Route("")]
        [Permission(PermissionValue.SystemEditUsers)]
        [ProducesResponseType(typeof(NewEntityResponseModel), 200)]
        public async Task<IActionResult> CreateUser([FromBody] UserRequestModel model)
        {
            try
            {
                var userId = (await UserService.CreateUser(model)).FirstOrDefault();

                return Ok(new NewEntityResponseModel {Id = userId});
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
                var users = await UserService.GetUsers(username);

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
                var user = await UserService.GetUserById(userId);

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
                if (await AuthoriseUser(userId))
                {
                    var roles = await UserService.GetUserRoles(userId);

                    return Ok(roles);
                }

                return PermissionError();
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
                await UserService.UpdateUser(userId, model);

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
                await UserService.DeleteUser(userId);

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
        public async Task<IActionResult> SetPassword([FromRoute] Guid userId, [FromBody] SetPasswordRequestModel request)
        {
            try
            {
                await UserService.SetPassword(userId, request.NewPassword);

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
        public async Task<IActionResult> SetEnabled([FromRoute] Guid userId, [FromBody] SetUserEnabledRequestModel model)
        {
            try
            {
                await UserService.SetUserEnabled(userId, model.Enabled);

                return Ok(model.Enabled);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}
