using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Logic.Authorisation.Attributes;
using MyPortal.Logic.Constants;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Summary;

namespace MyPortalCore.Controllers.Api
{ 
    [Route("api/userManagement/role")]
    public class ApplicationRoleController : BaseApiController
    {
        private readonly IApplicationRoleService _service;

        public ApplicationRoleController(IApplicationRoleService service, IApplicationUserService userService) : base(userService)
        {
            _service = service;
        }

        [Authorize(Policy = Policies.UserType.Staff)]
        [RequiresPermission(Permissions.System.Roles.Edit)]
        [Route("Search", Name = "ApiApplicationRoleSearch")]
        public async Task<IActionResult> Search([FromQuery] string roleName)
        {
            return await Process(async () =>
            {
                var roles = await _service.Get(roleName);

                var result = roles.Select(_dTMapper.Map<ApplicationRoleSummary>);

                return Ok(result);
            });
        }
    }
}