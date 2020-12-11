using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Database.Permissions;
using MyPortal.Logic.Caching;
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
        private readonly IYearGroupService _yearGroupService;

        public YearGroupsController(IUserService userService, IAcademicYearService academicYearService,
            IRolePermissionsCache rolePermissionsCache, IYearGroupService yearGroupService) : base(userService,
            academicYearService, rolePermissionsCache)
        {
            _yearGroupService = yearGroupService;
        }

        [HttpGet]
        [Route("get")]
        [Produces(typeof(IEnumerable<YearGroupModel>))]
        public async Task<IActionResult> GetYearGroups()
        {
            return await ProcessAsync(async () =>
            {
                var yearGroups = await _yearGroupService.GetYearGroups();

                return Ok(yearGroups);
            });
        }
    }
}
