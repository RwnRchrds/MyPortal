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
        public async Task<IHttpActionResult> AddUserToRole([FromBody] UserRoleModel roleModel)
        {
            try
            {
                await AdminProcesses.AddUserToRole(roleModel, _userManager, _identity);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Content(HttpStatusCode.OK, "User added to role");
        }

        [HttpPost]
        [RequiresPermission("EditUsers")]
        [Route("users/attachPerson", Name = "ApiAdminAttachPersonToUser")]
        public async Task<IHttpActionResult> AttachPersonToUser([FromBody] UserProfile userProfile)
        {
            try
            {
                await AdminProcesses.AttachPersonToUser(userProfile, _userManager, _identity, _context);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Content(HttpStatusCode.OK, "Person attached");
        }

        [HttpPost]
        [RequiresPermission("EditUsers")]
        [Route("users/resetPassword", Name = "ApiAdminChangePassword")]
        public async Task<IHttpActionResult> ChangePassword([FromBody] ChangePasswordModel data)
        {
            try
            {
                await AdminProcesses.ChangePassword(data, _userManager);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Content(HttpStatusCode.OK, "Password changed");
        }

        [HttpDelete]
        [RequiresPermission("EditUsers")]
        [Route("users/delete/{userId}", Name = "ApiAdminDeleteUser")]
        public async Task<IHttpActionResult> DeleteUser(string userId)
        {
            try
            {
                await AdminProcesses.DeleteUser(userId, _userManager);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Content(HttpStatusCode.OK, "User deleted");
        }

        [HttpPost]
        [RequiresPermission("EditUsers")]
        [Route("users/detachPerson", Name = "ApiAdminDetachPersonFromUser")]
        public async Task<IHttpActionResult> DetachPersonFromUser(ApplicationUser user)
        {
            try
            {
                await AdminProcesses.DetachPerson(user, _userManager, _context);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Content(HttpStatusCode.OK, "Person detached");
        }

        [HttpGet]
        [RequiresPermission("EditUsers")]
        [Route("users/get/all", Name = "ApiAdminGetAllUsers")]
        public async Task<IEnumerable<ApplicationUserDto>> GetAllUsers()
        {
            try
            {
                return await AdminProcesses.GetAllUsers(_identity);
            }
            catch (Exception e)
            {
                ThrowException(e);
                return null;
            }
        }

        [HttpPost]
        [RequiresPermission("EditUsers")]
        [Route("users/get/dataGrid/all", Name = "ApiAdminGetAllUsersDataGrid")]
        public async Task<IHttpActionResult> GetAllUsersDataGrid([FromBody] DataManagerRequest dm)
        {
            try
            {
                var users = await AdminProcesses.GetAllUsersDataGrid(_identity);
                return PrepareDataGridObject(users, dm);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("EditUsers")]
        [Route("users/create", Name = "ApiAdminCreateUser")]
        public async Task<IHttpActionResult> CreateUser([FromBody] NewUserViewModel model)
        {
            try
            {
                await AdminProcesses.CreateUser(model, _userManager);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Content(HttpStatusCode.OK, "User created");
        }

        [Route("users/removeFromRole", Name = "ApiAdminRemoveFromRole")]
        [HttpPost]
        [RequiresPermission("EditUsers")]
        public async Task<IHttpActionResult> RemoveFromRole([FromBody] UserRoleModel roleModel)
        {
            try
            {
                await AdminProcesses.RemoveFromRole(roleModel.UserId, roleModel.RoleName, _userManager, _identity);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Content(HttpStatusCode.OK, "User removed from role");
        }

        [RequiresPermission("EditRoles")]
        [Route("roles/create", Name = "ApiAdminCreateRole")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateRole([FromBody] ApplicationRole role)
        {
            try
            {
                await AdminProcesses.CreateRole(role, _roleManager);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Content(HttpStatusCode.OK, "Role created");
        }

        [RequiresPermission("EditRoles")]
        [Route("roles/update", Name = "ApiAdminUpdateRole")]
        [HttpPost]
        public async Task<IHttpActionResult> UpdateRole([FromBody] ApplicationRole role)
        {
            try
            {
                await AdminProcesses.UpdateRole(role, _roleManager);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Content(HttpStatusCode.OK, "Role updated");
        }

        [RequiresPermission("EditRoles")]
        [Route("roles/delete/{roleId}", Name = "ApiAdminDeleteRole")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteRole([FromUri] string roleId)
        {
            try
            {
                await AdminProcesses.DeleteRole(roleId, _roleManager);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Content(HttpStatusCode.OK, "Role deleted");
        }

        [RequiresPermission("EditRoles")]
        [Route("roles/get/byId/{roleId}", Name = "ApiAdminGetRoleById")]
        [HttpGet]
        public async Task<ApplicationRoleDto> GetRoleById([FromUri] string roleId)
        {
            try
            {
                return await AdminProcesses.GetRoleById(roleId, _roleManager);
            }
            catch (Exception e)
            {
                ThrowException(e);
                return null;
            }
        }

        [RequiresPermission("EditRoles")]
        [Route("roles/get/all", Name = "ApiAdminGetAllRoles")]
        [HttpGet]
        public async Task<IEnumerable<ApplicationRoleDto>> GetAllRoles()
        {
            try
            {
                return await AdminProcesses.GetAllRoles(_identity);
            }
            catch (Exception e)
            {
                ThrowException(e);
                return null;
            }
        }

        [RequiresPermission("EditUsers")]
        [Route("roles/get/byUser", Name = "ApiAdminGetRolesByUser")]
        [HttpGet]
        public async Task<IEnumerable<ApplicationRoleDto>> GetRolesByUser([FromUri] string userId)
        {
            try
            {
                return await AdminProcesses.GetRolesByUser(userId, _roleManager);
            }
            catch (Exception e)
            {
                ThrowException(e);
                return null;
            }
        }

        [RequiresPermission("EditUsers")]
        [Route("roles/get/dataGrid/byUser", Name = "ApiAdminGetRolesByUserDataGrid")]
        [HttpPost]
        public async Task<IHttpActionResult> GetRolesByUserDataGrid([FromUri] string userId,
            [FromBody] DataManagerRequest dm)
        {
            try
            {
                var roles = await AdminProcesses.GetRolesByUser(userId, _roleManager);
                return PrepareDataGridObject(roles, dm);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [RequiresPermission("EditRoles")]
        [Route("roles/get/dataGrid/all", Name = "ApiAdminGetAllRolesDataGrid")]
        [HttpPost]
        public async Task<IHttpActionResult> GetAllRolesDataGrid([FromBody] DataManagerRequest dm)
        {
            try
            {
                var roles = await AdminProcesses.GetAllRoles(_identity);
                return PrepareDataGridObject(roles, dm);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [RequiresPermission("EditRoles")]
        [Route("rolePermissions/toggle", Name = "ApiAdminToggleRolePermission")]
        [HttpPost]
        public async Task<IHttpActionResult> ToggleRolePermission([FromBody] RolePermission rolePermission)
        {
            try
            {
                await AdminProcesses.ToggleRolePermission(rolePermission, _roleManager, _identity);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Content(HttpStatusCode.OK, "Permissions updated");
        }

        [RequiresPermission("EditRoles")]
        [Route("rolePermissions/get/byRole/{roleId}", Name = "ApiAdminGetPermissionsByRole")]
        [HttpGet]
        public async Task<IEnumerable<PermissionIndicator>> GetPermissionsByRole([FromUri] string roleId)
        {
            try
            {
                return await AdminProcesses.GetPermissionsByRole(roleId, _roleManager, _identity);
            }
            catch (Exception e)
            {
                ThrowException(e);
                return null;
            }
        }

        [RequiresPermission("EditRoles")]
        [Route("rolePermissions/get/byRole/dataGrid/{roleId}", Name = "ApiAdminGetPermissionsByRoleDataGrid")]
        [HttpPost]
        public async Task<IHttpActionResult> GetPermissionsByRoleDataGrid([FromUri] string roleId, [FromBody] DataManagerRequest dm)
        {
            try
            {
                var permissions = await AdminProcesses.GetPermissionsByRole(roleId, _roleManager, _identity);
                return PrepareDataGridObject(permissions, dm);
            }
            catch (Exception e)
            {
                ThrowException(e);
                return null;
            }
        }
    }
}
