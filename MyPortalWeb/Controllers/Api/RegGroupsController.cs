using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Entity;
using MyPortalWeb.Controllers.BaseControllers;

namespace MyPortalWeb.Controllers.Api
{
    [Authorize]
    [Route("api/regGroups")]
    public class RegGroupsController : BaseApiController
    {
        public RegGroupsController(IAppServiceCollection services) : base(services)
        {
        }

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
    }
}
