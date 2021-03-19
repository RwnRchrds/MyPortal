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
    public class AddressController : BaseApiController
    {
        public AddressController(IAppServiceCollection services, IRolePermissionsCache rolePermissionsCache) : base(services, rolePermissionsCache)
        {
        }

        [HttpGet]
        [Route("person/{personId}")]
        [ProducesResponseType(typeof(IEnumerable<AddressModel>), 200)]
        public async Task<IActionResult> GetAddressesByPerson([FromRoute] Guid personId)
        {
            return await ProcessAsync(async () =>
            {
                var addresses = await Services.Addresses.GetAddressesByPerson(personId);

                return Ok(addresses);
            });
        }
    }
}
