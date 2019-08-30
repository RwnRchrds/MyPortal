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
using MyPortal.Models.Attributes;
using MyPortal.Models.Misc;
using MyPortal.Processes;
using MyPortal.ViewModels;
using Syncfusion.EJ2.Base;

namespace MyPortal.Controllers.Api
{
    [RoutePrefix("api/admin")]
    public class AdminController : MyPortalIdentityApiController
    {
        [HttpPost]
        [RequiresPermission("EditUsers")]
        [Route("users/addToRole")]
        public async Task<IHttpActionResult> AddUserToRole([FromBody] UserRoleModel roleModel)
        {
            var result = await AdminProcesses.AddUserToRole(roleModel, _userManager, _identity);
            return PrepareResponse(result);
        }

        [HttpPost]
        [RequiresPermission("EditUsers")]
        [Route("users/attachPerson")]
        public async Task<IHttpActionResult> AttachPerson([FromBody] UserProfile userProfile)
        {
            var result = await AdminProcesses.AttachPersonToUser(userProfile, _userManager, _identity, _context);
            return PrepareResponse(result);
        }

        [HttpPost]
        [RequiresPermission("EditUsers")]
        [Route("users/resetPassword")]
        public async Task<IHttpActionResult> ChangePassword([FromBody] ChangePasswordModel data)
        {
            var result = await AdminProcesses.ChangePassword(data, _userManager, _identity);

            return PrepareResponse(result);
        }

        [HttpDelete]
        [RequiresPermission("EditUsers")]
        [Route("users/delete/{userId}")]
        public async Task<IHttpActionResult> DeleteUser(string userId)
        {
            var result = await AdminProcesses.DeleteUser(userId, _userManager, _identity);

            return PrepareResponse(result);
        }

        [HttpPost]
        [RequiresPermission("EditUsers")]
        [Route("users/detachPerson")]
        public async Task<IHttpActionResult> DetachPerson(ApplicationUser user)
        {
            var result = await AdminProcesses.DetachPerson(user, _userManager, _identity, _context);

            return PrepareResponse(result);
        }

        [HttpGet]
        [RequiresPermission("EditUsers")]
        [Route("users/get/all")]
        public async Task<IEnumerable<ApplicationUserDto>> GetUsers()
        {
            var result = await AdminProcesses.GetAllUsers(_identity);

            return PrepareResponseObject(result);
        }

        [HttpPost]
        [RequiresPermission("EditUsers")]
        [Route("users/create")]
        public async Task<IHttpActionResult> NewUser([FromBody] NewUserViewModel model)
        {
            var result = await AdminProcesses.CreateUser(model, _userManager, _identity);

            return PrepareResponse(result);
        }

        [Route("users/removeFromRole")]
        [HttpPost]
        [RequiresPermission("EditUsers")]
        public async Task<IHttpActionResult> RemoveFromRole([FromBody] UserRoleModel roleModel)
        {
            var result = await AdminProcesses.RemoveFromRole(roleModel.UserId, roleModel.RoleName, _userManager, _identity, _context);

            return PrepareResponse(result);
        }

        [RequiresPermission("EditRoles")]
        [Route("roles/create")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateRole([FromBody] ApplicationRole role)
        {
            var result = await AdminProcesses.CreateRole(role, _roleManager, _identity);

            return PrepareResponse(result);
        }

        [RequiresPermission("EditRoles")]
        [Route("roles/update")]
        [HttpPost]
        public async Task<IHttpActionResult> UpdateRole([FromBody] ApplicationRole role)
        {
            var result = await AdminProcesses.UpdateRole(role, _roleManager, _identity);

            return PrepareResponse(result);
        }

        [RequiresPermission("EditRoles")]
        [Route("roles/delete/{roleId}")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteRole([FromUri] string roleId)
        {
            var result = await AdminProcesses.DeleteRole(roleId, _roleManager, _identity);

            return PrepareResponse(result);
        }

        [RequiresPermission("EditRoles")]
        [Route("roles/get/byId/{roleId}")]
        [HttpGet]
        public async Task<ApplicationRoleDto> GetRoleById([FromUri] string roleId)
        {
            var result = await AdminProcesses.GetRoleById(roleId, _roleManager, _identity);

            return PrepareResponseObject(result);
        }

        [RequiresPermission("EditRoles")]
        [Route("roles/get/all")]
        [HttpGet]
        public async Task<IEnumerable<ApplicationRoleDto>> GetAllRoles()
        {
            var result = await AdminProcesses.GetAllRoles(_identity);

            return PrepareResponseObject(result);
        }

        [RequiresPermission("EditRoles")]
        [Route("roles/get/dataGrid/all")]
        [HttpPost]
        public async Task<IHttpActionResult> GetAllRolesForDataGrid([FromBody] DataManagerRequest dm)
        {
            var result = PrepareResponseObject(await AdminProcesses.GetAllRoles(_identity));

            return PrepareDataGridObject(result, dm);
        }

        [RequiresPermission("EditRoles")]
        [Route("rolePermissions/toggle")]
        [HttpPost]
        public async Task<IHttpActionResult> ToggleRolePermission([FromBody] RolePermission rolePermission)
        {
            var result = await AdminProcesses.ToggleRolePermission(rolePermission, _roleManager, _identity);

            return PrepareResponse(result);
        }

        [RequiresPermission("EditRoles")]
        [Route("rolePermissions/get/byRole/{roleId}")]
        [HttpGet]
        public async Task<IEnumerable<PermissionIndicator>> GetPermissionsByRole([FromUri] string roleId)
        {
            var result = await AdminProcesses.GetPermissionsByRole(roleId, _roleManager, _identity);

            return PrepareResponseObject(result);
        }

        [RequiresPermission("EditRoles")]
        [Route("rolePermissions/get/byRole/dataGrid/{roleId}")]
        [HttpPost]
        public async Task<IHttpActionResult> GetPermissionsByRoleForDataGrid([FromUri] string roleId, [FromBody] DataManagerRequest dm)
        {
            var result = PrepareResponseObject(await AdminProcesses.GetPermissionsByRole(roleId, _roleManager, _identity));

            return PrepareDataGridObject(result, dm);
        }
    }
}
