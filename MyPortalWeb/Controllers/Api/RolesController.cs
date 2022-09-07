using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Database.Enums;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Data;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Requests.Admin.Roles;
using MyPortalWeb.Attributes;
using MyPortalWeb.Controllers.BaseControllers;
using MyPortalWeb.Models;
using MyPortalWeb.Models.Response;

namespace MyPortalWeb.Controllers.Api
{
    [Authorize]
    [Route("api/roles")]
    public class RolesController : BaseApiController
    {
        private IRoleService _roleService;

        public RolesController(IUserService userService, IRoleService roleService) : base(userService, roleService)
        {
            _roleService = roleService;
        }

        [HttpPost]
        [Route("")]
        [Permission(PermissionValue.SystemEditGroups)]
        [ProducesResponseType(typeof(NewEntityResponseModel), 200)]
        public async Task<IActionResult> CreateRole([FromBody] RoleRequestModel model)
        {
            try
            {
                var newId = (await _roleService.CreateRole(model)).FirstOrDefault();

                return Ok(new NewEntityResponseModel {Id = newId});
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost]
        [Route("{roleId}")]
        [Permission(PermissionValue.SystemEditGroups)]
        [ProducesResponseType(200)]
        public async Task<IActionResult> UpdateRole([FromRoute] Guid roleId, [FromBody] RoleRequestModel model)
        {
            try
            {
                await _roleService.UpdateRole(roleId, model);

                return Ok();
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpDelete]
        [Route("{roleId}")]
        [Permission(PermissionValue.SystemEditGroups)]
        [ProducesResponseType(200)]
        public async Task<IActionResult> DeleteRole([FromRoute] Guid roleId)
        {
            try
            {
                await _roleService.DeleteRole(roleId);

                return Ok();
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet]
        [Route("")]
        [Permission(PermissionValue.SystemViewGroups)]
        [ProducesResponseType(typeof(IEnumerable<RoleModel>), 200)]
        public async Task<IActionResult> GetRoles([FromQuery] string roleName)
        {
            try
            {
                IEnumerable<RoleModel> roles = await _roleService.GetRoles(roleName);

                return Ok(roles);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet]
        [Route("{roleId}")]
        [Permission(PermissionValue.SystemViewGroups)]
        [ProducesResponseType(typeof(RoleModel), 200)]
        public async Task<IActionResult> GetRoleById([FromRoute] Guid roleId)
        {
            try
            {
                var role = await _roleService.GetRoleById(roleId);

                return Ok(role);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet]
        [Route("{roleId}/permissions")]
        [Permission(PermissionValue.SystemViewGroups)]
        [ProducesResponseType(typeof(TreeNode), 200)]
        public async Task<IActionResult> GetPermissionsTree([FromRoute] Guid roleId)
        {
            try
            {
                var permissionsTree = await _roleService.GetPermissionsTree(roleId);

                return Ok(permissionsTree);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}
