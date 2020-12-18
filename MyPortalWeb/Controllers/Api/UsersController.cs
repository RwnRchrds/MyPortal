using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Database.Permissions;
using MyPortal.Logic.Caching;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Requests.Admin.Users;
using MyPortalWeb.Controllers.BaseControllers;

namespace MyPortalWeb.Controllers.Api
{ 
    [Authorize]
    [Route("api/user")]
    public class UsersController : BaseApiController
    {
        public UsersController(IUserService userService, IAcademicYearService academicYearService,
            IRolePermissionsCache rolePermissionsCache) : base(userService, academicYearService, rolePermissionsCache)
        {
        }

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
        public async Task<IActionResult> CreateUser([FromBody] CreateUserModel model)
        {
            return await ProcessAsync(async () =>
            {
                await UserService.CreateUser(model);

                return Ok("User created.");
            }, Permissions.System.Users.EditUsers);
        }

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> GetUsers([FromQuery] string username)
        {
            return await ProcessAsync(async () =>
            {
                var users = await UserService.GetUsers(username);

                return Ok(users);
            }, Permissions.System.Users.ViewUsers);
        }

        [HttpGet]
        [Route("get/id/{userId}")]
        public async Task<IActionResult> GetUserById([FromRoute] Guid userId)
        {
            return await ProcessAsync(async () =>
            {
                if (await AuthoriseUser(userId))
                {
                    var user = await UserService.GetUserById(userId);

                    return Ok(user);
                }

                return Forbid();
            });
        }

        [HttpGet]
        [Route("roles/get/{userId}")]
        public async Task<IActionResult> GetUserRoles([FromRoute] Guid userId)
        {
            return await ProcessAsync(async () =>
            {
                if (await AuthoriseUser(userId))
                {
                    var roles = await UserService.GetUserRoles(userId);

                    return Ok(roles);
                }

                return Forbid();
            });
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserModel model)
        {
            return await ProcessAsync(async () =>
            {
                await UserService.UpdateUser(model);

                return Ok("User updated.");
            }, Permissions.System.Users.EditUsers);
        }

        [HttpDelete]
        [Route("delete/{userId}")]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid userId)
        {
            return await ProcessAsync(async () =>
            {
                await UserService.DeleteUser(userId);

                return Ok("User deleted.");
            }, Permissions.System.Users.EditUsers);
        }

        [HttpPut]
        [Route("setPassword")]
        public async Task<IActionResult> SetPassword(SetPasswordModel request)
        {
            return await ProcessAsync(async () =>
            {
                if (await AuthoriseUser(request.UserId, true))
                {
                    await UserService.SetPassword(request.UserId, request.NewPassword);

                    return Ok("Password updated.");
                }

                return Forbid();
            });
        }

        [HttpPut]
        [Route("setEnabled")]
        public async Task<IActionResult> SetEnabled(SetUserEnabledModel model)
        {
            return await ProcessAsync(async () =>
            {
                await UserService.SetUserEnabled(model.UserId, model.Enabled);

                string responseMessage = model.Enabled ? "User enabled." : "User disabled.";

                return Ok(responseMessage);
            }, Permissions.System.Users.EditUsers);
        }
    }
}
