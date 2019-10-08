using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Principal;
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
    [Authorize]
    public class AdminController : MyPortalIdentityApiController
    {
        [HttpPost]
        [RequiresPermission("EditUsers")]
        [Route("users/addToRole", Name = "ApiAdminAddUserToRole")]
        public IHttpActionResult AddUserToRole([FromBody] UserRoleModel roleModel)
        {
            try
            {
                AdminProcesses.AddUserToRole(roleModel, _userManager, _identity);
            }
            catch (Exception e)
            {
                HandleException(e);
            }

            return Content(HttpStatusCode.OK, "User added to role");
        }

        [HttpPost]
        [RequiresPermission("EditUsers")]
        [Route("users/attachPerson", Name = "ApiAdminAttachPersonToUser")]
        public async Task<IHttpActionResult> AttachPersonToUser([FromBody] UserProfile userProfile)
        {
            var result = await AdminProcesses.AttachPersonToUser(userProfile, _userManager, _identity, _context);
            return PrepareResponse(result);
        }

        [HttpPost]
        [RequiresPermission("EditUsers")]
        [Route("users/resetPassword", Name = "ApiAdminChangePassword")]
        public async Task<IHttpActionResult> ChangePassword([FromBody] ChangePasswordModel data)
        {
            var result = await AdminProcesses.ChangePassword(data, _userManager, _identity);

            return PrepareResponse(result);
        }

        [HttpGet]
        [AllowAnonymous]
        //TODO: Generate random 12 char string for each client
        [Route("5wf0wxy08maf/createSystemUser")]
        public async Task<IHttpActionResult> Diagnostic_CreateSystemUser()
        {
            await UtilityProcesses.CreateSystemUser(_userManager);

            return Json(true);
        }

        [HttpDelete]
        [RequiresPermission("EditUsers")]
        [Route("users/delete/{userId}", Name = "ApiAdminDeleteUser")]
        public async Task<IHttpActionResult> DeleteUser(string userId)
        {
            var result = await AdminProcesses.DeleteUser(userId, _userManager, _identity);

            return PrepareResponse(result);
        }

        [HttpPost]
        [RequiresPermission("EditUsers")]
        [Route("users/detachPerson", Name = "ApiAdminDetachPersonFromUser")]
        public async Task<IHttpActionResult> DetachPersonFromUser(ApplicationUser user)
        {
            var result = await AdminProcesses.DetachPerson(user, _userManager, _identity, _context);

            return PrepareResponse(result);
        }

        [HttpGet]
        [RequiresPermission("EditUsers")]
        [Route("users/get/all", Name = "ApiAdminGetAllUsers")]
        public async Task<IEnumerable<ApplicationUserDto>> GetAllUsers()
        {
            var result = await AdminProcesses.GetAllUsers(_identity);

            return PrepareResponseObject(result);
        }

        [HttpPost]
        [RequiresPermission("EditUsers")]
        [Route("users/get/dataGrid/all", Name = "ApiAdminGetAllUsersDataGrid")]
        public async Task<IHttpActionResult> GetAllUsersDataGrid([FromBody] DataManagerRequest dm)
        {
            var result = PrepareResponseObject(await AdminProcesses.GetAllUsers_DataGrid(_identity));

            return PrepareDataGridObject(result, dm);
        }

        [HttpPost]
        [RequiresPermission("EditUsers")]
        [Route("users/create", Name = "ApiAdminCreateUser")]
        public async Task<IHttpActionResult> CreateUser([FromBody] NewUserViewModel model)
        {
            var result = await AdminProcesses.CreateUser(model, _userManager, _identity);

            return PrepareResponse(result);
        }

        [Route("users/removeFromRole", Name = "ApiAdminRemoveFromRole")]
        [HttpPost]
        [RequiresPermission("EditUsers")]
        public async Task<IHttpActionResult> RemoveFromRole([FromBody] UserRoleModel roleModel)
        {
            var result = await AdminProcesses.RemoveFromRole(roleModel.UserId, roleModel.RoleName, _userManager, _identity, _context);

            return PrepareResponse(result);
        }

        [RequiresPermission("EditRoles")]
        [Route("roles/create", Name = "ApiAdminCreateRole")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateRole([FromBody] ApplicationRole role)
        {
            var result = await AdminProcesses.CreateRole(role, _roleManager, _identity);

            return PrepareResponse(result);
        }

        [RequiresPermission("EditRoles")]
        [Route("roles/update", Name = "ApiAdminUpdateRole")]
        [HttpPost]
        public async Task<IHttpActionResult> UpdateRole([FromBody] ApplicationRole role)
        {
            var result = await AdminProcesses.UpdateRole(role, _roleManager, _identity);

            return PrepareResponse(result);
        }

        [RequiresPermission("EditRoles")]
        [Route("roles/delete/{roleId}", Name = "ApiAdminDeleteRole")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteRole([FromUri] string roleId)
        {
            var result = await AdminProcesses.DeleteRole(roleId, _roleManager, _identity);

            return PrepareResponse(result);
        }

        [RequiresPermission("EditRoles")]
        [Route("roles/get/byId/{roleId}", Name = "ApiAdminGetRoleById")]
        [HttpGet]
        public async Task<ApplicationRoleDto> GetRoleById([FromUri] string roleId)
        {
            var result = await AdminProcesses.GetRoleById(roleId, _roleManager, _identity);

            return PrepareResponseObject(result);
        }

        [RequiresPermission("EditRoles")]
        [Route("roles/get/all", Name = "ApiAdminGetAllRoles")]
        [HttpGet]
        public async Task<IEnumerable<ApplicationRoleDto>> GetAllRoles()
        {
            var result = await AdminProcesses.GetAllRoles(_identity);

            return PrepareResponseObject(result);
        }

        [RequiresPermission("EditUsers")]
        [Route("roles/get/byUser", Name = "ApiAdminGetRolesByUser")]
        [HttpGet]
        public async Task<IEnumerable<ApplicationRoleDto>> GetRolesByUser([FromUri] string userId)
        {
            var result = await AdminProcesses.GetRolesByUser(userId, _roleManager);

            return PrepareResponseObject(result);
        }

        [RequiresPermission("EditUsers")]
        [Route("roles/get/dataGrid/byUser", Name = "ApiAdminGetRolesByUserDataGrid")]
        [HttpPost]
        public async Task<IHttpActionResult> GetRolesByUserDataGrid([FromUri] string userId,
            [FromBody] DataManagerRequest dm)
        {
            var result = PrepareResponseObject(await AdminProcesses.GetRolesByUser(userId, _roleManager));

            return PrepareDataGridObject(result, dm);
        }

        [RequiresPermission("EditRoles")]
        [Route("roles/get/dataGrid/all", Name = "ApiAdminGetAllRolesDataGrid")]
        [HttpPost]
        public async Task<IHttpActionResult> GetAllRolesDataGrid([FromBody] DataManagerRequest dm)
        {
            var result = PrepareResponseObject(await AdminProcesses.GetAllRoles(_identity));

            return PrepareDataGridObject(result, dm);
        }

        [RequiresPermission("EditRoles")]
        [Route("rolePermissions/toggle", Name = "ApiAdminToggleRolePermission")]
        [HttpPost]
        public async Task<IHttpActionResult> ToggleRolePermission([FromBody] RolePermission rolePermission)
        {
            var result = await AdminProcesses.ToggleRolePermission(rolePermission, _roleManager, _identity);

            return PrepareResponse(result);
        }

        [RequiresPermission("EditRoles")]
        [Route("rolePermissions/get/byRole/{roleId}", Name = "ApiAdminGetPermissionsByRole")]
        [HttpGet]
        public async Task<IEnumerable<PermissionIndicator>> GetPermissionsByRole([FromUri] string roleId)
        {
            var result = await AdminProcesses.GetPermissionsByRole(roleId, _roleManager, _identity);

            return PrepareResponseObject(result);
        }

        [RequiresPermission("EditRoles")]
        [Route("rolePermissions/get/byRole/dataGrid/{roleId}", Name = "ApiAdminGetPermissionsByRoleDataGrid")]
        [HttpPost]
        public async Task<IHttpActionResult> GetPermissionsByRoleDataGrid([FromUri] string roleId, [FromBody] DataManagerRequest dm)
        {
            var result = PrepareResponseObject(await AdminProcesses.GetPermissionsByRole(roleId, _roleManager, _identity));

            return PrepareDataGridObject(result, dm);
        }
    }
}
