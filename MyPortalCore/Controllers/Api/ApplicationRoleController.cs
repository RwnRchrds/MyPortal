using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Database.Constants;
using MyPortal.Logic.Constants;
using MyPortal.Logic.Interfaces;

namespace MyPortalCore.Controllers.Api
{ 
    [Route("api/userManagement/role")]
    public class ApplicationRoleController : BaseApiController
    {
        private readonly IApplicationRoleService _applicationRoleService;

        public ApplicationRoleController(IApplicationRoleService applicationRoleService, IApplicationUserService userService) : base(userService)
        {
            _applicationRoleService = applicationRoleService;
        }

        [Authorize(Policy = Policies.UserType.Staff)]
        [Route("Search", Name = "ApiApplicationRoleSearch")]
        public async Task<IActionResult> Search([FromQuery] string roleName)
        {
            return await ProcessAsync(async () =>
            {
                var roles = await _applicationRoleService.Get(roleName);

                var result = roles;

                return Ok(result);
            });
        }

        public override void Dispose()
        {
            _applicationRoleService.Dispose();
        }
    }
}