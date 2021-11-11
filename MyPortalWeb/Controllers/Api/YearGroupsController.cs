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
    [Route("api/yearGroups")]
    public class YearGroupsController : BaseApiController
    {
        private IYearGroupService _yearGroupService;

        public YearGroupsController(IUserService userService, IRoleService roleService,
            IYearGroupService yearGroupService) : base(userService, roleService)
        {
            _yearGroupService = yearGroupService;
        }

        [HttpGet]
        [Route("get")]
        [ProducesResponseType(typeof(IEnumerable<YearGroupModel>), 200)]
        public async Task<IActionResult> GetYearGroups()
        {
            try
            {
                var yearGroups = await _yearGroupService.GetYearGroups();

                return Ok(yearGroups);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}
