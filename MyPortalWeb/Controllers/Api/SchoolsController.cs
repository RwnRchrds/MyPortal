using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Logic.Caching;
using MyPortal.Logic.Interfaces.Services;
using MyPortalWeb.Controllers.BaseControllers;

namespace MyPortalWeb.Controllers.Api
{
    [Route("api/schools")]
    public class SchoolsController : BaseApiController
    {
        private readonly ISchoolService _schoolService;

        public SchoolsController(IUserService userService, IAcademicYearService academicYearService, IRolePermissionsCache rolePermissionsCache, ISchoolService schoolService) : base(userService, academicYearService, rolePermissionsCache)
        {
            _schoolService = schoolService;
        }

        [HttpGet]
        [Route("local/name")]
        [Produces(typeof(string))]
        public async Task<IActionResult> GetLocalSchoolName()
        {
            return await ProcessAsync(async () => Ok(await _schoolService.GetLocalSchoolName()));
        }
    }
}
