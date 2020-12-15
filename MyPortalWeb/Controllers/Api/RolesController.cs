using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Logic.Caching;
using MyPortal.Logic.Interfaces.Services;
using MyPortalWeb.Controllers.BaseControllers;

namespace MyPortalWeb.Controllers.Api
{
    [Route("api/roles")]
    public class RolesController : BaseApiController
    {
        private readonly IRoleService _roleService;

        public RolesController(IUserService userService, IAcademicYearService academicYearService,
            IRolePermissionsCache rolePermissionsCache, IRoleService roleService) : base(userService,
            academicYearService, rolePermissionsCache)
        {
            _roleService = roleService;
        }

        [HttpGet]
        [Route("permissions/role/{roleId}")]
        public async Task<IActionResult> GetPermissionsTree([FromRoute] Guid roleId)
        {
            return await ProcessAsync(async () =>
            {
                var permissionsTree = await _roleService.GetPermissionsTree(roleId);

                return Ok(permissionsTree);
            });
        }
    }
}
