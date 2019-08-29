using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity.EntityFramework;
using MyPortal.Dtos.Identity;
using MyPortal.Models;
using MyPortal.Models.Misc;
using MyPortal.Processes;
using MyPortal.ViewModels;

namespace MyPortal.Controllers.Api
{
    public class AdminController : MyPortalIdentityApiController
    {
        #region Users
        //Add a role to a user
        [HttpPost]
        [Route("api/users/addRole")]
        public async Task<IHttpActionResult> AddRole([FromBody] UserRoleModel roleModel)
        {
            var result = await AdminProcesses.AddUserToRole(roleModel, _userManager, _identity);
            return PrepareResponse(result);
        }

        //Attach User to Personal Profile
        [HttpPost]
        [Route("api/users/attach")]
        public async Task<IHttpActionResult> AttachPerson([FromBody] UserProfile userProfile)
        {
            var result = await AdminProcesses.AttachPersonToUser(userProfile, _userManager, _identity, _context);
            return PrepareResponse(result);
        }

        //Change a User's Password
        [HttpPost]
        [Route("api/users/resetPassword")]
        public async Task<IHttpActionResult> ChangePassword([FromBody] ChangePasswordModel data)
        {
            var result = await AdminProcesses.ChangePassword(data, _userManager, _identity);

            return PrepareResponse(result);
        }

        //Delete user
        [HttpDelete]
        [Route("api/users/delete/{userId}")]
        public async Task<IHttpActionResult> DeleteUser(string userId)
        {
            var result = await AdminProcesses.DeleteUser(userId, _userManager, _identity);

            return PrepareResponse(result);
        }

        //Detach User from Personal Profile
        [HttpPost]
        [Route("api/users/detach")]
        public async Task<IHttpActionResult> DetachPerson(ApplicationUser user)
        {
            var result = await AdminProcesses.DetachPerson(user, _userManager, _identity, _context);

            return PrepareResponse(result);
        }

        //GET Users From Database
        [HttpGet]
        [Route("api/users")]
        public IEnumerable<UserDto> GetUsers()
        {
            return _identity.Users.ToList().Select(Mapper.Map<IdentityUser, UserDto>);
        }

        //New user
        [HttpPost]
        [Route("api/users/new")]
        public async Task<IHttpActionResult> NewUser([FromBody] NewUserViewModel data)
        {
            data.Id = Guid.NewGuid().ToString();

            if (data.Username.IsNullOrWhiteSpace() || data.Password.IsNullOrWhiteSpace())
            {
                return Content(HttpStatusCode.BadRequest, "Invalid Data");
            }


            var user = new ApplicationUser
            {
                Id = data.Id,
                UserName = data.Username
            };


            var result = await _userManager.CreateAsync(user, data.Password);

            if (result.Succeeded)
            {
                return Ok("User added");
            }

            return BadRequest();
        }

        //Remove Users From a Role
        [Route("api/users/removeRole/{userId}/{roleName}")]
        [HttpDelete]
        public async Task<IHttpActionResult> RemoveFromRole(string userId, string roleName)
        {
            var userIsAttached = _context.Students.Any(x => x.Person.UserId == userId) ||
                                 _context.StaffMembers.Any(x => x.Person.UserId == userId);
            var userInDb = _identity.Users.FirstOrDefault(user => user.Id == userId);
            if (userInDb == null)
            {
                return Content(HttpStatusCode.NotFound, "User not found");
            }

            //get user's assigned roles
            var userRoles = await _userManager.GetRolesAsync(userId);

            //check for role to be removed
            var roleToRemove =
                userRoles.FirstOrDefault(role => role.Equals(roleName, StringComparison.InvariantCultureIgnoreCase));
            if (roleToRemove == null)
            {
                return Content(HttpStatusCode.NotFound, "User is not in role");
            }

            if (userIsAttached && (roleName == "Staff" || roleName == "Student"))
            {
                return Content(HttpStatusCode.BadRequest,
                    "User cannot be removed from a primary role while attached to a person");
            }

            var result = await _userManager.RemoveFromRoleAsync(userId, roleToRemove);

            if (result.Succeeded)
            {
                return Ok();
            }

            return BadRequest();
        }

        #endregion

        #region Roles
        
        
        
        
        [HttpGet]
        [Route("api/roles")]
        public IEnumerable<RoleDto> GetRoles()
        {
            return _identity.Roles.ToList().Select(Mapper.Map<IdentityRole, RoleDto>);
        }
        #endregion
    }
}
