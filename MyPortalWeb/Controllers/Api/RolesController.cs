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
        [Route("create")]
        [Permission(PermissionValue.SystemEditGroups)]
        [ProducesResponseType(typeof(NewEntityResponse), 200)]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleModel model)
        {
            try
            {
                var newId = (await _roleService.Create(model)).FirstOrDefault();

                return Ok(new NewEntityResponse {Id = newId});
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPut]
        [Route("update")]
        [Permission(PermissionValue.SystemEditGroups)]
        [ProducesResponseType(200)]
        public async Task<IActionResult> UpdateRole([FromBody] UpdateRoleModel model)
        {
            try
            {
                await _roleService.Update(model);

                return Ok();
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpDelete]
        [Route("delete/{roleId}")]
        [Permission(PermissionValue.SystemEditGroups)]
        [ProducesResponseType(200)]
        public async Task<IActionResult> DeleteRole([FromRoute] Guid roleId)
        {
            try
            {
                await _roleService.Delete(roleId);

                return Ok();
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet]
        [Route("get")]
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
        [Route("get/id/{roleId}")]
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
        [Route("permissions/role/{roleId}")]
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
