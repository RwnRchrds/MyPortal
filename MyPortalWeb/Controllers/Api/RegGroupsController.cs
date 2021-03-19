using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Logic.Caching;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Entity;
using MyPortalWeb.Controllers.BaseControllers;

namespace MyPortalWeb.Controllers.Api
{
    [Authorize]
    [Route("api/regGroups")]
    public class RegGroupsController : BaseApiController
    {
        
        [HttpGet]
        [Route("get")]
        [ProducesResponseType(typeof(IEnumerable<RegGroupModel>), 200)]
        public async Task<IActionResult> GetRegGroups()
        {
            return await ProcessAsync(async () =>
            {
                var regGroups = await Services.RegGroups.GetRegGroups();

                return Ok(regGroups);
            });
        }

        public RegGroupsController(IAppServiceCollection services, IRolePermissionsCache rolePermissionsCache) : base(services, rolePermissionsCache)
        {
        }
    }
}
