using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Database.Permissions;
using MyPortal.Logic.Caching;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Requests.Admin;
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

        /// <summary>
        /// Create a new user.
        /// </summary>
        /// <response code="200">The user was created successfully.</response>
        /// <response code="401">The current user is not authenticated.</response>
        /// <response code="403">The current user does not have permission to edit users.</response>
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateUser(CreateUserModel model)
        {
            return await ProcessAsync(async () =>
            {
                await UserService.CreateUser(model);

                return Ok("User created.");
            }, Permissions.System.Users.EditUsers);
        }

        /// <summary>
        /// Delete a user.
        /// </summary>
        /// <response code="200">The user was deleted successfully.</response>
        /// <response code="401">The current user is not authenticated.</response>
        /// <response code="403">The current user does not have permission to edit users.</response>
        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteUser(Guid userId)
        {
            return await ProcessAsync(async () =>
            {
                await UserService.DeleteUser(userId);

                return Ok("User deleted.");
            }, Permissions.System.Users.EditUsers);
        }

        /// <summary>
        /// Set a user's password.
        /// </summary>
        /// <response code="200">The user's password was updated successfully.</response>
        /// <response code="401">The current user is not authenticated.</response>
        /// <response code="403">The current user does not have permission to edit users.</response>
        [HttpPut]
        [Route("setPassword")]
        public async Task<IActionResult> SetPassword(SetPasswordModel request)
        {
            return await ProcessAsync(async () =>
            {
                await UserService.SetPassword(request.UserId, request.NewPassword);

                return Ok("Password updated.");
            }, Permissions.System.Users.EditUsers);
        }

        /// <summary>
        /// Set whether a user is enabled/disabled.
        /// </summary>
        /// <response code="200">The user was enabled/disabled successfully.</response>
        /// <response code="401">The current user is not authenticated.</response>
        /// <response code="403">The current user does not have permission to edit users.</response>
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
