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
using MyPortal.Attributes;
using MyPortal.Attributes.HttpAuthorise;
using MyPortal.Models.Misc;
using MyPortal.Services;
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
                await AdminService.AddUserToRole(roleModel, UserManager, RoleManager);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok( "User added to role");
        }

        [HttpPost]
        [RequiresPermission("EditUsers")]
        [Route("users/attachPerson", Name = "ApiAdminAttachPersonToUser")]
        public async Task<IHttpActionResult> AttachPersonToUser([FromBody] UserProfile userProfile)
        {
            try
            {
                await AdminService.AttachPersonToUser(userProfile, UserManager, Identity, Context);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok( "Person attached");
        }

        [HttpPost]
        [RequiresPermission("EditUsers")]
        [Route("users/resetPassword", Name = "ApiAdminChangePassword")]
        public async Task<IHttpActionResult> ChangePassword([FromBody] ChangePasswordModel data)
        {
            try
            {
                await AdminService.ChangePassword(data, UserManager);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok( "Password changed");
        }

        [HttpDelete]
        [RequiresPermission("EditUsers")]
        [Route("users/delete/{userId}", Name = "ApiAdminDeleteUser")]
        public async Task<IHttpActionResult> DeleteUser(string userId)
        {
            try
            {
                await AdminService.DeleteUser(userId, UserManager);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok( "User deleted");
        }

        [HttpPost]
        [RequiresPermission("EditUsers")]
        [Route("users/detachPerson", Name = "ApiAdminDetachPersonFromUser")]
        public async Task<IHttpActionResult> DetachPersonFromUser(ApplicationUser user)
        {
            try
            {
                await AdminService.DetachPerson(user, UserManager, Context);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok( "Person detached");
        }

        [HttpGet]
        [RequiresPermission("EditUsers")]
        [Route("users/get/all", Name = "ApiAdminGetAllUsers")]
        public async Task<IEnumerable<ApplicationUserDto>> GetAllUsers()
        {
            try
            {
                return await AdminService.GetAllUsers(Identity);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("EditUsers")]
        [Route("users/get/dataGrid/all", Name = "ApiAdminGetAllUsersDataGrid")]
        public async Task<IHttpActionResult> GetAllUsersDataGrid([FromBody] DataManagerRequest dm)
        {
            try
            {
                var users = await AdminService.GetAllUsersDataGrid(Identity);
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
                await AdminService.CreateUser(model, UserManager);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok( "User created");
        }

        [Route("users/removeFromRole", Name = "ApiAdminRemoveFromRole")]
        [HttpPost]
        [RequiresPermission("EditUsers")]
        public async Task<IHttpActionResult> RemoveFromRole([FromBody] UserRoleModel roleModel)
        {
            try
            {
                await AdminService.RemoveFromRole(roleModel.UserId, roleModel.RoleName, UserManager, Identity);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok( "User removed from role");
        }

        [RequiresPermission("EditRoles")]
        [Route("roles/create", Name = "ApiAdminCreateRole")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateRole([FromBody] ApplicationRole role)
        {
            try
            {
                await AdminService.CreateRole(role, RoleManager);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok( "Role created");
        }

        [RequiresPermission("EditRoles")]
        [Route("roles/update", Name = "ApiAdminUpdateRole")]
        [HttpPost]
        public async Task<IHttpActionResult> UpdateRole([FromBody] ApplicationRole role)
        {
            try
            {
                await AdminService.UpdateRole(role, RoleManager);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok( "Role updated");
        }

        [RequiresPermission("EditRoles")]
        [Route("roles/delete/{roleId}", Name = "ApiAdminDeleteRole")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteRole([FromUri] string roleId)
        {
            try
            {
                await AdminService.DeleteRole(roleId, RoleManager);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok( "Role deleted");
        }

        [RequiresPermission("EditRoles")]
        [Route("roles/get/byId/{roleId}", Name = "ApiAdminGetRoleById")]
        [HttpGet]
        public async Task<ApplicationRoleDto> GetRoleById([FromUri] string roleId)
        {
            try
            {
                return await AdminService.GetRoleById(roleId, RoleManager);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [RequiresPermission("EditRoles")]
        [Route("roles/get/all", Name = "ApiAdminGetAllRoles")]
        [HttpGet]
        public async Task<IEnumerable<ApplicationRoleDto>> GetAllRoles()
        {
            try
            {
                return await AdminService.GetAllRoles(Identity);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [RequiresPermission("EditUsers")]
        [Route("roles/get/byUser", Name = "ApiAdminGetRolesByUser")]
        [HttpGet]
        public async Task<IEnumerable<ApplicationRoleDto>> GetRolesByUser([FromUri] string userId)
        {
            try
            {
                return await AdminService.GetRolesByUser(userId, RoleManager);
            }
            catch (Exception e)
            {
                throw GetException(e);
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
                var roles = await AdminService.GetRolesByUser(userId, RoleManager);
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
                var roles = await AdminService.GetAllRoles(Identity);
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
                await AdminService.ToggleRolePermission(rolePermission, RoleManager, Identity);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok( "Permissions updated");
        }

        [RequiresPermission("EditRoles")]
        [Route("rolePermissions/get/byRole/{roleId}", Name = "ApiAdminGetPermissionsByRole")]
        [HttpGet]
        public async Task<IEnumerable<PermissionIndicator>> GetPermissionsByRole([FromUri] string roleId)
        {
            try
            {
                return await AdminService.GetPermissionsByRole(roleId, RoleManager, Identity);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [RequiresPermission("EditRoles")]
        [Route("rolePermissions/get/byRole/dataGrid/{roleId}", Name = "ApiAdminGetPermissionsByRoleDataGrid")]
        [HttpPost]
        public async Task<IHttpActionResult> GetPermissionsByRoleDataGrid([FromUri] string roleId, [FromBody] DataManagerRequest dm)
        {
            try
            {
                var permissions = await AdminService.GetPermissionsByRole(roleId, RoleManager, Identity);
                return PrepareDataGridObject(permissions, dm);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }
    }
}
