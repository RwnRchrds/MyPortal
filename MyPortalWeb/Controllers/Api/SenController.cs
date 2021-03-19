using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Logic.Caching;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Entity;
using MyPortalWeb.Controllers.BaseControllers;

namespace MyPortalWeb.Controllers.Api
{
    [Route("api/SEND")]
    public class SenController : BaseApiController
    {
        public SenController(IAppServiceCollection services, IRolePermissionsCache rolePermissionsCache) : base(services, rolePermissionsCache)
        {
        }

        [HttpGet]
        [Route("giftedTalented/student/{studentId}")]
        [ProducesResponseType(typeof(IEnumerable<GiftedTalentedModel>), 200)]
        public async Task<IActionResult> GetGiftedTalentedByStudent([FromRoute] Guid studentId)
        {
            return await ProcessAsync(async () =>
            {
                var giftedTalented = await Services.Sen.GetGiftedTalentedSubjects(studentId);

                return Ok(giftedTalented);
            });
        }
    }
}
