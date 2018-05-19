using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MyPortal.Dtos.Identity;
using MyPortal.Models;

namespace MyPortal.Controllers.Api
{
    public class UsersController : ApiController
    {
        private ApplicationDbContext _identity;
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersController()
        {
            _identity = new ApplicationDbContext();
            var store = new UserStore<ApplicationUser>(_identity);
            _userManager = new UserManager<ApplicationUser>(store);
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
                return NotFound();

            //get user's assigned roles
            IList<string> userRoles = await _userManager.GetRolesAsync(userId);

            //check for role to be removed
            var roleToRemove = userRoles.FirstOrDefault(role => role.Equals(roleName, StringComparison.InvariantCultureIgnoreCase));
            if (roleToRemove == null)
                return NotFound();

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
                return NotFound();

            var removePassword = await _userManager.RemovePasswordAsync(data.UserId);

            if (removePassword.Succeeded)
            {
                var addNewPassword = await _userManager.AddPasswordAsync(data.UserId, data.Password);

                if (addNewPassword.Succeeded)
                    return Ok();

                return BadRequest();
            }

            return BadRequest();
        }

    }
}
