using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Database.Permissions;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Requests.Admin;
using MyPortalWeb.Controllers.BaseControllers;

namespace MyPortalWeb.Controllers.Api
{
    [Authorize]
    public class UserController : BaseApiController
    {
        public UserController(IUserService userService, IAcademicYearService academicYearService) : base(userService, academicYearService)
        {

        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateUser(CreateUserRequest request)
        {
            return await ProcessAsync(async () =>
            {
                await UserService.CreateUser(request);

                return Ok("User created.");
            }, Permissions.System.Users.EditUsers);
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteUser(Guid userId)
        {
            return await ProcessAsync(async () =>
            {
                await UserService.DeleteUser(userId);

                return Ok("User deleted.");
            });
        }

        [HttpPut]
        [Route("setPassword")]
        public async Task<IActionResult> SetPassword(SetPasswordRequest request)
        {
            return await ProcessAsync(async () =>
            {
                if (request.ConfirmPassword.Equals(request.NewPassword, StringComparison.InvariantCultureIgnoreCase))
                {
                    await UserService.SetPassword(request.UserId, request.NewPassword);

                    return Ok("Password updated.");
                }

                return BadRequest("The passwords you entered do not match.");
            }, Permissions.System.Users.EditUsers);
        }
    }
}
