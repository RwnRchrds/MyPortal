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
using MyPortal.Logic.Models.Entity;
using MyPortalWeb.Controllers.BaseControllers;

namespace MyPortalWeb.Controllers.Api
{
    [Authorize]
    [Route("api/yearGroups")]
    public class YearGroupsController : BaseApiController
    {
        public YearGroupsController(IAppServiceCollection services, IRolePermissionsCache rolePermissionsCache) : base(services, rolePermissionsCache)
        {
        }

        [HttpGet]
        [Route("get")]
        [ProducesResponseType(typeof(IEnumerable<YearGroupModel>), 200)]
        public async Task<IActionResult> GetYearGroups()
        {
            return await ProcessAsync(async () =>
            {
                var yearGroups = await Services.YearGroups.GetYearGroups();

                return Ok(yearGroups);
            });
        }
    }
}
