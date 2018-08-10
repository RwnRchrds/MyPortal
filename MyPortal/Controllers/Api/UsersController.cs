using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MyPortal.Dtos.Identity;
using MyPortal.Models;
using MyPortal.Models.Misc;
using MyPortal.ViewModels;

namespace MyPortal.Controllers.Api
{
    [Authorize]
    public class UsersController : ApiController
    {
        private readonly ApplicationDbContext _identity;
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersController()
        {
            _identity = new ApplicationDbContext();
            var store = new UserStore<ApplicationUser>(_identity);
            _userManager = new UserManager<ApplicationUser>(store);
        }

        protected override void Dispose(bool disposing)
        {
            _identity.Dispose();
            _userManager.Dispose();
        }

        //GET Users From Database
        [HttpGet]
        [Route("api/users")]
        public IEnumerable<UserDto> GetUsers()
        {
            return _identity.Users.ToList().Select(Mapper.Map<IdentityUser, UserDto>);
        }

        //Remove Users From a Role
        [Route("api/users/{userId}/roles/{roleName}")]
        [HttpDelete]
        public async Task<IHttpActionResult> RemoveFromRole(string userId, string roleName)
        {
            var userInDb = _identity.Users.FirstOrDefault(user => user.Id == userId);
            if (userInDb == null)
                return Content(HttpStatusCode.NotFound, "User not found");

            //get user's assigned roles
            var userRoles = await _userManager.GetRolesAsync(userId);

            //check for role to be removed
            var roleToRemove =
                userRoles.FirstOrDefault(role => role.Equals(roleName, StringComparison.InvariantCultureIgnoreCase));
            if (roleToRemove == null)
                return Content(HttpStatusCode.NotFound, "User is not in role");

            var result = await _userManager.RemoveFromRoleAsync(userId, roleToRemove);
            if (result.Succeeded)
                return Ok();

            return BadRequest();
        }

        //Change a User's Password
        [HttpPost]
        [Route("api/users/resetpassword")]
        public async Task<IHttpActionResult> ChangePassword([FromBody] ChangePasswordModel data)
        {
            var userInDb = _identity.Users.FirstOrDefault(user => user.Id == data.UserId);
            if (userInDb == null)
                return Content(HttpStatusCode.NotFound, "User not found");

            var removePassword = await _userManager.RemovePasswordAsync(data.UserId);

            if (removePassword.Succeeded)
            {
                var addNewPassword = await _userManager.AddPasswordAsync(data.UserId, data.Password);

                if (addNewPassword.Succeeded)
                    return Ok("Password reset");

                return BadRequest();
            }

            return BadRequest();
        }

        //Add a role to a user
        [HttpPost]
        [Route("api/users/addrole")]
        public async Task<IHttpActionResult> AddRole([FromBody] UserRoleModel data)
        {
            var userInDb = _identity.Users.FirstOrDefault(u => u.Id == data.UserId);
            var roleToAdd = _identity.Roles.FirstOrDefault(r => r.Name == data.RoleName);

            switch (data.RoleName)
            {
                case "Admin":
                    if (!await _userManager.IsInRoleAsync(data.UserId, "SeniorStaff"))
                        return Content(HttpStatusCode.BadRequest, "User must be a member of SeniorStaff");
                    break;
                case "Finance":
                    if (!await _userManager.IsInRoleAsync(data.UserId, "SeniorStaff"))
                        return Content(HttpStatusCode.BadRequest, "User must be a member of SeniorStaff");
                    break;
            }

            switch (data.RoleName)
            {
                case "Staff":
                case "SeniorStaff":
                case "Admin":
                    if (await _userManager.IsInRoleAsync(data.UserId, "Student"))
                        return Content(HttpStatusCode.BadRequest, "Students cannot be added to staff groups");
                    break;
                case "Student":
                    if (await _userManager.IsInRoleAsync(data.UserId, "Staff"))
                        return Content(HttpStatusCode.BadRequest, "Staff cannot be added to student groups");

                    if (await _userManager.IsInRoleAsync(data.UserId, "SeniorStaff"))
                        return Content(HttpStatusCode.BadRequest, "Staff cannot be added to student groups");
                    break;
            }

            if (await _userManager.IsInRoleAsync(data.UserId, data.RoleName))
                return Content(HttpStatusCode.BadRequest, "User is already in role");

            var result = await _userManager.AddToRoleAsync(data.UserId, data.RoleName);

            if (result.Succeeded)
                return Ok("Role added");

            return BadRequest();
        }

        //New user
        [HttpPost]
        [Route("api/users/new")]
        public async Task<IHttpActionResult> NewUser([FromBody] NewUserViewModel data)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var user = new ApplicationUser
            {
                Id = data.Id,
                UserName = data.Username
            };


            var result = await _userManager.CreateAsync(user, data.Password);

            if (result.Succeeded)
                return Ok("User added");

            return BadRequest();
        }

        //Delete user
        [HttpDelete]
        [Route("api/users/{userId}/delete")]
        public async Task<IHttpActionResult> DeleteUser(string userId)
        {
            var userInDb = _identity.Users.FirstOrDefault(x => x.Id == userId);

            if (userInDb == null)
                return Content(HttpStatusCode.NotFound, "User not found");

            var userRoles = await _userManager.GetRolesAsync(userId);

            foreach (var role in userRoles) await _userManager.RemoveFromRoleAsync(userId, role);

            var result = await _userManager.DeleteAsync(userInDb);

            if (result.Succeeded)
                return Ok("User deleted");

            return BadRequest();
        }
    }
}