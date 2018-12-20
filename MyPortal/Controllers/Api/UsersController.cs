using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using Microsoft.Ajax.Utilities;
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
        private readonly MyPortalDbContext _context;
        private readonly ApplicationDbContext _identity;
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersController()
        {
            _identity = new ApplicationDbContext();
            var store = new UserStore<ApplicationUser>(_identity);
            _userManager = new UserManager<ApplicationUser>(store);
            _context = new MyPortalDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _identity.Dispose();
            _userManager.Dispose();
            _context.Dispose();
        }

        //GET Users From Database
        [HttpGet]
        [Route("api/users")]
        public IEnumerable<UserDto> GetUsers()
        {
            return _identity.Users.ToList().Select(Mapper.Map<IdentityUser, UserDto>);
        }

        //Remove Users From a Role
        [Route("api/users/removeRole/{userId}/{roleName}")]
        [HttpDelete]
        public async Task<IHttpActionResult> RemoveFromRole(string userId, string roleName)
        {
            var userIsAttached = _context.Students.Any(x => x.UserId == userId) ||
                                 _context.Staff.Any(x => x.UserId == userId);
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

            if (userIsAttached && (roleName == "Staff" || roleName == "Student"))
                return Content(HttpStatusCode.BadRequest,
                    "User cannot be removed from a primary role while attached to a person");

            var result = await _userManager.RemoveFromRoleAsync(userId, roleToRemove);
            if (result.Succeeded)
                return Ok();

            return BadRequest();
        }

        //Change a User's Password
        [HttpPost]
        [Route("api/users/resetPassword")]
        public async Task<IHttpActionResult> ChangePassword([FromBody] ChangePasswordModel data)
        {
            if (data.Password != data.Confirm)
                return Content(HttpStatusCode.BadRequest, "Passwords do not match");

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
        [Route("api/users/addRole")]
        public async Task<IHttpActionResult> AddRole([FromBody] UserRoleModel data)
        {
            var userInDb = _identity.Users.FirstOrDefault(u => u.Id == data.UserId);
            var roleToAdd = _identity.Roles.FirstOrDefault(r => r.Name == data.RoleName);

            if (userInDb == null || roleToAdd == null)
                return Content(HttpStatusCode.BadRequest, "User or role does not exist");

            switch (data.RoleName)
            {
                case "Admin" when !await _userManager.IsInRoleAsync(data.UserId, "SeniorStaff"):
                    return Content(HttpStatusCode.BadRequest, "User must be a member of SeniorStaff");
                case "Finance" when !await _userManager.IsInRoleAsync(data.UserId, "SeniorStaff"):
                    return Content(HttpStatusCode.BadRequest, "User must be a member of SeniorStaff");
            }

            switch (data.RoleName)
            {
                case "SeniorStaff":
                case "Admin":
                    if (await _userManager.IsInRoleAsync(data.UserId, "Student"))
                        return Content(HttpStatusCode.BadRequest, "Students cannot be added to staff roles");
                    break;

                case "Student":
                    if (await _userManager.IsInRoleAsync(data.UserId, "Staff"))
                        return Content(HttpStatusCode.BadRequest, "Staff user cannot be added to student role");

                    if (await _userManager.IsInRoleAsync(data.UserId, "Student"))
                        return Content(HttpStatusCode.BadRequest, "User is already member of student role");

                    break;

                case "Staff":
                    if (await _userManager.IsInRoleAsync(data.UserId, "Student"))
                        return Content(HttpStatusCode.BadRequest, "Student user cannot be added to staff role");

                    if (await _userManager.IsInRoleAsync(data.UserId, "Staff"))
                        return Content(HttpStatusCode.BadRequest, "User is already member of staff role");

                    break;
                default:
                    return Content(HttpStatusCode.NotFound, "Role not found");
            }

            if (await _userManager.IsInRoleAsync(data.UserId, data.RoleName))
                return Content(HttpStatusCode.BadRequest, "User is already in role");

            var result = await _userManager.AddToRoleAsync(data.UserId, data.RoleName);

            if (result.Succeeded)
                return Ok("Role added");

            return BadRequest();
        }

        //Attach User to Personal Profile
        [HttpPost]
        [Route("api/users/attach")]
        public async Task<IHttpActionResult> AttachPerson([FromBody] UserProfile data)
        {
            if (data.RoleName != "Staff" && data.RoleName != "Student")
                return Content(HttpStatusCode.BadRequest, "User can only be assigned student or staff as primary role");

            var userInDb = _identity.Users.FirstOrDefault(u => u.Id == data.UserId);
            var roleToAdd = _identity.Roles.FirstOrDefault(r => r.Name == data.RoleName);
            var userIsAttached = _context.Students.Any(x => x.UserId == data.UserId) ||
                                 _context.Staff.Any(x => x.UserId == data.UserId);

            if (userInDb == null)
                return Content(HttpStatusCode.BadRequest, "User not found");

            if (roleToAdd == null)
                return Content(HttpStatusCode.BadRequest, "Role not found");

            if (userIsAttached)
                return Content(HttpStatusCode.BadRequest, "User is already attached to a person");

            if (data.RoleName == "Staff")
            {
                var roles = await _userManager.GetRolesAsync(data.UserId);
                await _userManager.RemoveFromRolesAsync(data.UserId, roles.ToArray());
                await _userManager.AddToRoleAsync(data.UserId, "Staff");
                var personInDb = _context.Staff.Single(x => x.Id == data.PersonId);
                if (personInDb.UserId != null)
                    return Content(HttpStatusCode.BadRequest, "Another user is already attached to this person");
                personInDb.UserId = userInDb.Id;
                _context.SaveChanges();
                return Ok("User assigned to person");
            }

            if (data.RoleName == "Student")
            {
                var roles = await _userManager.GetRolesAsync(data.UserId);
                await _userManager.RemoveFromRolesAsync(data.UserId, roles.ToArray());
                await _userManager.AddToRoleAsync(data.UserId, "Student");
                var personInDb = _context.Students.Single(x => x.Id == data.PersonId);
                if (personInDb.UserId != null)
                    return Content(HttpStatusCode.BadRequest, "Another user is already attached to this person");
                personInDb.UserId = userInDb.Id;
                _context.SaveChanges();
                return Ok("User assigned to person");
            }

            return BadRequest();
        }

        //Detach User from Personal Profile
        [HttpPost]
        [Route("api/users/detach")]
        public async Task<IHttpActionResult> DetachPerson(UserDto user)
        {
            var userIsAttached = _context.Students.Any(x => x.UserId == user.Id) ||
                                 _context.Staff.Any(x => x.UserId == user.Id);

            if (!userIsAttached)
                return Content(HttpStatusCode.BadRequest, "User is not attached to a person");

            if (await _userManager.IsInRoleAsync(user.Id, "Staff"))
            {
                var personInDb = _context.Staff.Single(x => x.UserId == user.Id);
                personInDb.UserId = null;
                _context.SaveChanges();

                var roles = await _userManager.GetRolesAsync(user.Id);
                var result = await _userManager.RemoveFromRolesAsync(user.Id, roles.ToArray());

                if (result.Succeeded)
                    return Ok("User detached from person");
            }

            if (await _userManager.IsInRoleAsync(user.Id, "Student"))
            {
                var personInDb = _context.Students.Single(x => x.UserId == user.Id);
                personInDb.UserId = null;
                _context.SaveChanges();

                var roles = await _userManager.GetRolesAsync(user.Id);
                var result = await _userManager.RemoveFromRolesAsync(user.Id, roles.ToArray());

                if (result.Succeeded)
                    return Ok("User detached from person");
            }

            return BadRequest();
        }

        //New user
        [HttpPost]
        [Route("api/users/new")]
        public async Task<IHttpActionResult> NewUser([FromBody] NewUserViewModel data)
        {
            data.Id = Guid.NewGuid().ToString();

            if (data.Username.IsNullOrWhiteSpace() || data.Password.IsNullOrWhiteSpace())
                return Content(HttpStatusCode.BadRequest, "Invalid Data");

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
        [Route("api/users/delete/{userId}")]
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