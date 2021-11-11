using System;
using System.Collections.Generic;
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
    [Route("api/regGroups")]
    public class RegGroupsController : BaseApiController
    {
        private IRegGroupService _regGroupService;

        public RegGroupsController(IUserService userService, IRoleService roleService, IRegGroupService regGroupService)
            : base(userService, roleService)
        {
            _regGroupService = regGroupService;
        }

        [HttpGet]
        [Route("get")]
        [ProducesResponseType(typeof(IEnumerable<RegGroupModel>), 200)]
        public async Task<IActionResult> GetRegGroups()
        {
            try
            {
                var regGroups = await _regGroupService.GetRegGroups();

                return Ok(regGroups);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}
