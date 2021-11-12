using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MyPortal.Database.Enums;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Requests.Admin.Users;
using MyPortalWeb.Attributes;
using MyPortalWeb.Controllers.BaseControllers;
using MyPortalWeb.Models;
using MyPortalWeb.Models.Response;

namespace MyPortalWeb.Controllers.Api
{ 
    [Authorize]
    [Route("api/user")]
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
                   (await GetLoggedInUser()).Id == requestedUserId;
        }

        [HttpPost]
        [Route("create")]
        [Permission(PermissionValue.SystemEditUsers)]
        [ProducesResponseType(typeof(NewEntityResponseModel), 200)]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserModel model)
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
        [Route("get")]
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
        [Route("get/id/{userId}")]
        [Permission(PermissionValue.SystemViewUsers)]
        [ProducesResponseType(typeof(UserModel), 200)]
        public async Task<IActionResult> GetUserById([FromRoute] Guid userId)
        {
            try
            {
                if (await AuthoriseUser(userId))
                {
                    var user = await UserService.GetUserById(userId);

                    return Ok(user);
                }

                return Forbid();
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet]
        [Route("roles/get/{userId}")]
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

                return Forbid();
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPut]
        [Route("update")]
        [Permission(PermissionValue.SystemEditUsers)]
        [ProducesResponseType(200)]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserModel model)
        {
            try
            {
                await UserService.UpdateUser(model);

                return Ok();
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpDelete]
        [Route("delete/{userId}")]
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
        [Route("setPassword")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> SetPassword(SetPasswordModel request)
        {
            try
            {
                if (await AuthoriseUser(request.UserId, true))
                {
                    await UserService.SetPassword(request.UserId, request.NewPassword);

                    return Ok();
                }

                return Forbid();
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPut]
        [Route("setEnabled")]
        [Permission(PermissionValue.SystemEditUsers)]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<IActionResult> SetEnabled(SetUserEnabledModel model)
        {
            try
            {
                await UserService.SetUserEnabled(model.UserId, model.Enabled);

                return Ok(model.Enabled);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}
