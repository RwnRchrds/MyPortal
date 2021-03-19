using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MyPortal.Database.Permissions;
using MyPortal.Logic.Caching;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Requests.Admin.Users;
using MyPortalWeb.Controllers.BaseControllers;
using MyPortalWeb.Models;

namespace MyPortalWeb.Controllers.Api
{ 
    [Authorize]
    [Route("api/user")]
    public class UsersController : BaseApiController
    {
        

        private async Task<bool> AuthoriseUser(Guid requestedUserId, bool requireEdit = false)
        {
            // Users do not require extra permission to access resources related to themselves
            return await UserHasPermission(requireEdit
                       ? Permissions.System.Users.EditUsers
                       : Permissions.System.Users.ViewUsers) ||
                   (await GetLoggedInUser()).Id == requestedUserId;
        }

        [HttpPost]
        [Route("create")]
        [ProducesResponseType(typeof(NewEntityResponse), 200)]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserModel model)
        {
            return await ProcessAsync(async () =>
            {
                var userId = (await Services.Users.CreateUser(model)).FirstOrDefault();

                return Ok(new NewEntityResponse {Id = userId});
            }, Permissions.System.Users.EditUsers);
        }

        [HttpGet]
        [Route("get")]
        [ProducesResponseType(typeof(IEnumerable<UserModel>), 200)]
        public async Task<IActionResult> GetUsers([FromQuery] string username)
        {
            return await ProcessAsync(async () =>
            {
                var users = await Services.Users.GetUsers(username);

                return Ok(users);
            }, Permissions.System.Users.ViewUsers);
        }

        [HttpGet]
        [Route("get/id/{userId}")]
        [ProducesResponseType(typeof(UserModel), 200)]
        public async Task<IActionResult> GetUserById([FromRoute] Guid userId)
        {
            return await ProcessAsync(async () =>
            {
                if (await AuthoriseUser(userId))
                {
                    var user = await Services.Users.GetUserById(userId);

                    return Ok(user);
                }

                return Forbid();
            });
        }

        [HttpGet]
        [Route("roles/get/{userId}")]
        [ProducesResponseType(typeof(IEnumerable<RoleModel>), 200)]
        public async Task<IActionResult> GetUserRoles([FromRoute] Guid userId)
        {
            return await ProcessAsync(async () =>
            {
                if (await AuthoriseUser(userId))
                {
                    var roles = await Services.Users.GetUserRoles(userId);

                    return Ok(roles);
                }

                return Forbid();
            });
        }

        [HttpPut]
        [Route("update")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserModel model)
        {
            return await ProcessAsync(async () =>
            {
                await Services.Users.UpdateUser(model);

                return Ok();
            }, Permissions.System.Users.EditUsers);
        }

        [HttpDelete]
        [Route("delete/{userId}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid userId)
        {
            return await ProcessAsync(async () =>
            {
                await Services.Users.DeleteUser(userId);

                return Ok();
            }, Permissions.System.Users.EditUsers);
        }

        [HttpPut]
        [Route("setPassword")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> SetPassword(SetPasswordModel request)
        {
            return await ProcessAsync(async () =>
            {
                if (await AuthoriseUser(request.UserId, true))
                {
                    await Services.Users.SetPassword(request.UserId, request.NewPassword);

                    return Ok();
                }

                return Forbid();
            });
        }

        [HttpPut]
        [Route("setEnabled")]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<IActionResult> SetEnabled(SetUserEnabledModel model)
        {
            return await ProcessAsync(async () =>
            {
                await Services.Users.SetUserEnabled(model.UserId, model.Enabled);

                return Ok(model.Enabled);
            }, Permissions.System.Users.EditUsers);
        }

        public UsersController(IAppServiceCollection services, IRolePermissionsCache rolePermissionsCache) : base(services, rolePermissionsCache)
        {
        }
    }
}
