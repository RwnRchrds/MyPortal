using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using MyPortal.Areas.Staff.ViewModels;
using MyPortal.Models;
using MyPortal.Attributes;
using MyPortal.Attributes.Filters;
using MyPortal.Attributes.HttpAuthorise;
using MyPortal.BusinessLogic.Dtos.DataGrid;
using MyPortal.BusinessLogic.Dtos.Identity;
using MyPortal.BusinessLogic.Models;
using MyPortal.BusinessLogic.Models.Data;
using MyPortal.Models.Identity;
using MyPortal.Services;
using Syncfusion.EJ2.Base;

namespace MyPortal.Controllers.Api
{
    [ValidateModel]
    [RoutePrefix("api/admin")]
    [Authorize]
    public class AdminController : MyPortalApiController
    {
        private readonly AdminService _service;

        public AdminController()
        {
            _service = new AdminService();
        }

        protected override void Dispose(bool disposing)
        {
            _service.Dispose();
        }

        [HttpPost]
        [RequiresPermission("EditUsers")]
        [Route("users/addToRole", Name = "ApiAddUserToRole")]
        public async Task<IHttpActionResult> AddUserToRole([FromBody] UserRoleModel roleModel)
        {
            try
            {
                await _service.AddUserToRole(roleModel);

                return Ok("User added to role");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("EditUsers")]
        [Route("users/attachPerson", Name = "ApiAttachPersonToUser")]
        public async Task<IHttpActionResult> AttachPersonToUser([FromBody] UserProfile userProfile)
        {
            try
            {
                await _service.AttachPersonToUser(userProfile);

                return Ok("Person attached");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("EditUsers")]
        [Route("users/resetPassword", Name = "ApiChangePassword")]
        public async Task<IHttpActionResult> ChangePassword([FromBody] ChangePasswordModel data)
        {
            try
            {
                await _service.ChangePassword(data);

                return Ok("Password changed");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpDelete]
        [RequiresPermission("EditUsers")]
        [Route("users/delete/{userId}", Name = "ApiDeleteUser")]
        public async Task<IHttpActionResult> DeleteUser(string userId)
        {
            try
            {
                await _service.DeleteUser(userId);
                
                return Ok("User deleted");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("EditUsers")]
        [Route("users/detachPerson", Name = "ApiDetachPersonFromUser")]
        public async Task<IHttpActionResult> DetachPersonFromUser(ApplicationUser user)
        {
            try
            {
                await _service.DetachPerson(user);

                return Ok("Person detached");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet]
        [RequiresPermission("EditUsers")]
        [Route("users/get/all", Name = "ApiGetAllUsers")]
        public async Task<IEnumerable<ApplicationUserDto>> GetAllUsers()
        {
            try
            {
                var users = await _service.GetAllUsers();

                return users.Select(_mapping.Map<ApplicationUserDto>);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("EditUsers")]
        [Route("users/get/dataGrid/all", Name = "ApiGetAllUsersDataGrid")]
        public async Task<IHttpActionResult> GetAllUsersDataGrid([FromBody] DataManagerRequest dm)
        {
            try
            {
                var users = await _service.GetAllUsers();

                var list = users.Select(_mapping.Map<DataGridApplicationUserDto>);

                return PrepareDataGridObject(list, dm);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("EditUsers")]
        [Route("users/create", Name = "ApiCreateUser")]
        public async Task<IHttpActionResult> CreateUser([FromBody] NewUserModel model)
        {
            try
            {
                var result = await _service.CreateUser(model);

                return Ok(result);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("EditUsers")]
        [Route("users/changeType", Name = "ApiChangeUserType")]
        public async Task<IHttpActionResult> ChangeUserType([FromBody] ApplicationUser user)
        {
            try
            {
                await _service.ChangeUserType(user.Id, user.UserType);

                return Ok("User type changed");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [Route("users/removeFromRole", Name = "ApiRemoveUserFromRole")]
        [HttpPost]
        [RequiresPermission("EditUsers")]
        public async Task<IHttpActionResult> RemoveUserFromRole([FromBody] UserRoleModel roleModel)
        {
            try
            {
                await _service.RemoveFromRole(roleModel.UserId, roleModel.RoleName);

                return Ok("User removed from role");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [RequiresPermission("EditRoles")]
        [Route("roles/create", Name = "ApiCreateRole")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateRole([FromBody] ApplicationRole role)
        {
            try
            {
                await _service.CreateRole(role);

                return Ok("Role created");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [RequiresPermission("EditRoles")]
        [Route("roles/update", Name = "ApiUpdateRole")]
        [HttpPost]
        public async Task<IHttpActionResult> UpdateRole([FromBody] ApplicationRole role)
        {
            try
            {
                await _service.UpdateRole(role);

                return Ok("Role updated");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [RequiresPermission("EditRoles")]
        [Route("roles/delete/{roleId}", Name = "ApiDeleteRole")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteRole([FromUri] string roleId)
        {
            try
            {
                await _service.DeleteRole(roleId);

                return Ok("Role deleted");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [RequiresPermission("EditRoles")]
        [Route("roles/get/byId/{roleId}", Name = "ApiGetRoleById")]
        [HttpGet]
        public async Task<ApplicationRoleDto> GetRoleById([FromUri] string roleId)
        {
            try
            {
                var role = await _service.GetRoleById(roleId);

                return _mapping.Map<ApplicationRoleDto>(role);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [RequiresPermission("EditRoles")]
        [Route("roles/get/userDefined", Name = "ApiGetUserDefinedRoles")]
        [HttpGet]
        public async Task<IEnumerable<ApplicationRoleDto>> GetUserDefinedRoles()
        {
            try
            {
                var roles = await _service.GetUserDefinedRoles();

                return roles.Select(_mapping.Map<ApplicationRoleDto>);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [RequiresPermission("EditUsers")]
        [Route("roles/get/byUser", Name = "ApiGetRolesByUser")]
        [HttpGet]
        public async Task<IEnumerable<ApplicationRoleDto>> GetRolesByUser([FromUri] string userId)
        {
            try
            {
                var roles = await _service.GetRolesByUser(userId);

                return roles.Select(_mapping.Map<ApplicationRoleDto>);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [RequiresPermission("EditUsers")]
        [Route("roles/get/dataGrid/byUser", Name = "ApiGetRolesByUserDataGrid")]
        [HttpPost]
        public async Task<IHttpActionResult> GetRolesByUserDataGrid([FromUri] string userId,
            [FromBody] DataManagerRequest dm)
        {
            try
            {
                var roles = await _service.GetRolesByUser(userId);

                var list = roles.Select(_mapping.Map<ApplicationRoleDto>);

                return PrepareDataGridObject(list, dm);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [RequiresPermission("EditRoles")]
        [Route("roles/get/dataGrid/userDefined", Name = "ApiGetUserDefinedRolesDataGrid")]
        [HttpPost]
        public async Task<IHttpActionResult> GetUserDefinedRolesDataGrid([FromBody] DataManagerRequest dm)
        {
            try
            {
                var roles = await _service.GetUserDefinedRoles();

                var list = roles.Select(_mapping.Map<ApplicationRoleDto>);

                return PrepareDataGridObject(list, dm);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [RequiresPermission("EditRoles")]
        [Route("rolePermissions/toggle", Name = "ApiToggleRolePermission")]
        [HttpPost]
        public async Task<IHttpActionResult> ToggleRolePermission([FromBody] RolePermission rolePermission)
        {
            try
            {
                await _service.ToggleRolePermission(rolePermission);

                return Ok("Permissions updated");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [RequiresPermission("EditRoles")]
        [Route("rolePermissions/get/byRole/{roleId}", Name = "ApiGetPermissionsByRole")]
        [HttpGet]
        public async Task<IEnumerable<PermissionIndicator>> GetPermissionsByRole([FromUri] string roleId)
        {
            try
            {
                return await _service.GetPermissionsByRole(roleId);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [RequiresPermission("EditRoles")]
        [Route("rolePermissions/get/byRole/dataGrid/{roleId}", Name = "ApiGetPermissionsByRoleDataGrid")]
        [HttpPost]
        public async Task<IHttpActionResult> GetPermissionsByRoleDataGrid([FromUri] string roleId, [FromBody] DataManagerRequest dm)
        {
            try
            {
                var permissions = await _service.GetPermissionsByRole(roleId);

                return PrepareDataGridObject(permissions, dm);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }
    }
}
