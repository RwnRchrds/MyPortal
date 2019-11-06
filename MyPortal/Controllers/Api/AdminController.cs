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
using MyPortal.Areas.Staff.ViewModels;
using MyPortal.Dtos.Identity;
using MyPortal.Models;
using MyPortal.Attributes;
using MyPortal.Attributes.HttpAuthorise;
using MyPortal.Dtos.GridDtos;
using MyPortal.Interfaces;
using MyPortal.Models.Misc;
using MyPortal.Services;
using Syncfusion.EJ2.Base;

namespace MyPortal.Controllers.Api
{
    [RoutePrefix("api/admin")]
    [Authorize]
    public class AdminController : MyPortalApiController
    {
        private readonly AdminService _service;

        public AdminController()
        {
            _service = new AdminService(UnitOfWork);
        }

        [HttpPost]
        [RequiresPermission("EditUsers")]
        [Route("users/addToRole", Name = "ApiAddUserToRole")]
        public async Task<IHttpActionResult> AddUserToRole([FromBody] UserRoleModel roleModel)
        {
            try
            {
                await _service.AddUserToRole(roleModel);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok( "User added to role");
        }

        [HttpPost]
        [RequiresPermission("EditUsers")]
        [Route("users/attachPerson", Name = "ApiAttachPersonToUser")]
        public async Task<IHttpActionResult> AttachPersonToUser([FromBody] UserProfile userProfile)
        {
            try
            {
                await _service.AttachPersonToUser(userProfile);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok( "Person attached");
        }

        [HttpPost]
        [RequiresPermission("EditUsers")]
        [Route("users/resetPassword", Name = "ApiChangePassword")]
        public async Task<IHttpActionResult> ChangePassword([FromBody] ChangePasswordModel data)
        {
            try
            {
                await _service.ChangePassword(data);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok( "Password changed");
        }

        [HttpDelete]
        [RequiresPermission("EditUsers")]
        [Route("users/delete/{userId}", Name = "ApiDeleteUser")]
        public async Task<IHttpActionResult> DeleteUser(string userId)
        {
            try
            {
                await _service.DeleteUser(userId);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok( "User deleted");
        }

        [HttpPost]
        [RequiresPermission("EditUsers")]
        [Route("users/detachPerson", Name = "ApiDetachPersonFromUser")]
        public async Task<IHttpActionResult> DetachPersonFromUser(ApplicationUser user)
        {
            try
            {
                await _service.DetachPerson(user);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok( "Person detached");
        }

        [HttpGet]
        [RequiresPermission("EditUsers")]
        [Route("users/get/all", Name = "ApiGetAllUsers")]
        public async Task<IEnumerable<ApplicationUserDto>> GetAllUsers()
        {
            try
            {
                var users = await _service.GetAllUsers();

                return users.Select(Mapper.Map<ApplicationUser, ApplicationUserDto>);
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

                var list = users.Select(Mapper.Map<ApplicationUser, GridApplicationUserDto>);

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
        public async Task<IHttpActionResult> CreateUser([FromBody] NewUserViewModel model)
        {
            try
            {
                await _service.CreateUser(model);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok( "User created");
        }

        [Route("users/removeFromRole", Name = "ApiRemoveFromRole")]
        [HttpPost]
        [RequiresPermission("EditUsers")]
        public async Task<IHttpActionResult> RemoveFromRole([FromBody] UserRoleModel roleModel)
        {
            try
            {
                await _service.RemoveFromRole(roleModel.UserId, roleModel.RoleName);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok( "User removed from role");
        }

        [RequiresPermission("EditRoles")]
        [Route("roles/create", Name = "ApiCreateRole")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateRole([FromBody] ApplicationRole role)
        {
            try
            {
                await _service.CreateRole(role);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok( "Role created");
        }

        [RequiresPermission("EditRoles")]
        [Route("roles/update", Name = "ApiUpdateRole")]
        [HttpPost]
        public async Task<IHttpActionResult> UpdateRole([FromBody] ApplicationRole role)
        {
            try
            {
                await _service.UpdateRole(role);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok( "Role updated");
        }

        [RequiresPermission("EditRoles")]
        [Route("roles/delete/{roleId}", Name = "ApiDeleteRole")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteRole([FromUri] string roleId)
        {
            try
            {
                await _service.DeleteRole(roleId);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok( "Role deleted");
        }

        [RequiresPermission("EditRoles")]
        [Route("roles/get/byId/{roleId}", Name = "ApiGetRoleById")]
        [HttpGet]
        public async Task<ApplicationRoleDto> GetRoleById([FromUri] string roleId)
        {
            try
            {
                var role = await _service.GetRoleById(roleId);

                return Mapper.Map<ApplicationRole, ApplicationRoleDto>(role);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [RequiresPermission("EditRoles")]
        [Route("roles/get/all", Name = "ApiGetAllRoles")]
        [HttpGet]
        public async Task<IEnumerable<ApplicationRoleDto>> GetAllRoles()
        {
            try
            {
                var roles = await _service.GetAllRoles();

                return roles.Select(Mapper.Map<ApplicationRole, ApplicationRoleDto>);
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

                return roles.Select(Mapper.Map<ApplicationRole, ApplicationRoleDto>);
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

                var list = roles.Select(Mapper.Map<ApplicationRole, ApplicationRoleDto>);

                return PrepareDataGridObject(list, dm);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [RequiresPermission("EditRoles")]
        [Route("roles/get/dataGrid/all", Name = "ApiGetAllRolesDataGrid")]
        [HttpPost]
        public async Task<IHttpActionResult> GetAllRolesDataGrid([FromBody] DataManagerRequest dm)
        {
            try
            {
                var roles = await _service.GetAllRoles();

                var list = roles.Select(Mapper.Map<ApplicationRole, ApplicationRoleDto>);

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
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok( "Permissions updated");
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
