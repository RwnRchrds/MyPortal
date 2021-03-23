using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Entity;
using MyPortalWeb.Controllers.BaseControllers;

namespace MyPortalWeb.Controllers.Api
{
    [Authorize]
    [Route("api/houses")]
    public class HousesController : BaseApiController
    {
        public HousesController(IAppServiceCollection services) : base(services)
        {
        }

        [HttpGet]
        [Route("get")]
        [ProducesResponseType(typeof(IEnumerable<HouseModel>), 200)]
        public async Task<IActionResult> GetHouses()
        {
            return await ProcessAsync(async () =>
            {
                var houses = await Services.Houses.GetHouses();

                return Ok(houses);
            });
        }
    }
}
