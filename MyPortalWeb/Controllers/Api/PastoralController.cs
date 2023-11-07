using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Data.Curriculum;
using MyPortalWeb.Controllers.BaseControllers;

namespace MyPortalWeb.Controllers.Api
{
    [Authorize]
    [Route("api/pastoral")]
    public class PastoralController : BaseApiController
    {
        private readonly IPastoralService _pastoralService;

        public PastoralController(IUserService userService, IPastoralService pastoralService) : base(userService)
        {
            _pastoralService = pastoralService;
        }

        [HttpGet]
        [Route("houses")]
        [ProducesResponseType(typeof(IEnumerable<HouseModel>), 200)]
        public async Task<IActionResult> GetHouses()
        {
            try
            {
                var houses = await _pastoralService.GetHouses();

                return Ok(houses);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet]
        [Route("regGroups")]
        [ProducesResponseType(typeof(IEnumerable<RegGroupModel>), 200)]
        public async Task<IActionResult> GetRegGroups()
        {
            try
            {
                var regGroups = await _pastoralService.GetRegGroups();

                return Ok(regGroups);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet]
        [Route("yearGroups")]
        [ProducesResponseType(typeof(IEnumerable<YearGroupModel>), 200)]
        public async Task<IActionResult> GetYearGroups()
        {
            try
            {
                var yearGroups = await _pastoralService.GetYearGroups();

                return Ok(yearGroups);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}