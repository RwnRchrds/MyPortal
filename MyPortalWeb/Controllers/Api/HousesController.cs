using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Logic.Caching;
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
        private readonly IHouseService _houseService;

        public HousesController(IUserService userService, IAcademicYearService academicYearService,
            IRolePermissionsCache rolePermissionsCache, IHouseService houseService) : base(userService,
            academicYearService, rolePermissionsCache)
        {
            _houseService = houseService;
        }

        [HttpGet]
        [Route("get")]
        [Produces(typeof(IEnumerable<HouseModel>))]
        public async Task<IActionResult> GetHouses()
        {
            return await ProcessAsync(async () =>
            {
                var houses = await _houseService.GetHouses();

                return Ok(houses);
            });
        }
    }
}
