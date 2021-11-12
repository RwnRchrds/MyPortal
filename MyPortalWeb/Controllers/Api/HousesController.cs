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
        private IHouseService _houseService;

        public HousesController(IUserService userService, IRoleService roleService, IHouseService houseService) : base(userService, roleService)
        {
            _houseService = houseService;
        }

        [HttpGet]
        [Route("get")]
        [ProducesResponseType(typeof(IEnumerable<HouseModel>), 200)]
        public async Task<IActionResult> GetHouses()
        {
            try
            {
                var houses = await _houseService.GetHouses();

                return Ok(houses);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}
