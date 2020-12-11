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
    [Route("api/regGroups")]
    public class RegGroupsController : BaseApiController
    {
        private readonly IRegGroupService _regGroupService;

        public RegGroupsController(IUserService userService, IAcademicYearService academicYearService,
            IRolePermissionsCache rolePermissionsCache, IRegGroupService regGroupService) : base(userService,
            academicYearService, rolePermissionsCache)
        {
            _regGroupService = regGroupService;
        }

        [HttpGet]
        [Route("get")]
        [Produces(typeof(IEnumerable<RegGroupModel>))]
        public async Task<IActionResult> GetRegGroups()
        {
            return await ProcessAsync(async () =>
            {
                var regGroups = await _regGroupService.GetRegGroups();

                return Ok(regGroups);
            });
        }
    }
}
