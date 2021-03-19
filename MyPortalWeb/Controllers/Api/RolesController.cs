using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Database.Permissions;
using MyPortal.Logic.Caching;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Data;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Requests.Admin.Roles;
using MyPortalWeb.Controllers.BaseControllers;
using MyPortalWeb.Models;

namespace MyPortalWeb.Controllers.Api
{
    [Authorize]
    [Route("api/roles")]
    public class RolesController : BaseApiController
    {
        
        [HttpPost]
        [Route("create")]
        [ProducesResponseType(typeof(NewEntityResponse), 200)]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleModel model)
        {
            return await ProcessAsync(async () =>
            {
                var newId = (await Services.Roles.Create(model)).FirstOrDefault();

                return Ok(new NewEntityResponse {Id = newId});
            }, Permissions.System.Groups.EditGroups);
        }

        [HttpPut]
        [Route("update")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> UpdateRole([FromBody] UpdateRoleModel model)
        {
            return await ProcessAsync(async () =>
            {
                await Services.Roles.Update(model);

                return Ok();
            }, Permissions.System.Groups.EditGroups);
        }

        [HttpDelete]
        [Route("delete/{roleId}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> DeleteRole([FromRoute] Guid roleId)
        {
            return await ProcessAsync(async () =>
            {
                await Services.Roles.Delete(roleId);

                return Ok();
            }, Permissions.System.Groups.EditGroups);
        }

        [HttpGet]
        [Route("get")]
        [ProducesResponseType(typeof(IEnumerable<RoleModel>), 200)]
        public async Task<IActionResult> GetRoles([FromQuery] string roleName)
        {
            return await ProcessAsync(async () =>
            {
                IEnumerable<RoleModel> roles = await Services.Roles.GetRoles(roleName);

                return Ok(roles);
            });
        }

        [HttpGet]
        [Route("get/id/{roleId}")]
        [ProducesResponseType(typeof(RoleModel), 200)]
        public async Task<IActionResult> GetRoleById([FromRoute] Guid roleId)
        {
            return await ProcessAsync(async () =>
            {
                var role = await Services.Roles.GetRoleById(roleId);

                return Ok(role);
            }, Permissions.System.Groups.ViewGroups);
        }

        [HttpGet]
        [Route("permissions/role/{roleId}")]
        [ProducesResponseType(typeof(TreeNode), 200)]
        public async Task<IActionResult> GetPermissionsTree([FromRoute] Guid roleId)
        {
            return await ProcessAsync(async () =>
            {
                var permissionsTree = await Services.Roles.GetPermissionsTree(roleId);

                return Ok(permissionsTree);
            }, Permissions.System.Groups.ViewGroups);
        }

        public RolesController(IAppServiceCollection services, IRolePermissionsCache rolePermissionsCache) : base(services, rolePermissionsCache)
        {
        }
    }
}
