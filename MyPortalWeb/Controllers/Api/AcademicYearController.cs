using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Logic.Caching;
using MyPortal.Logic.Interfaces.Services;
using MyPortalWeb.Controllers.BaseControllers;

namespace MyPortalWeb.Controllers.Api
{
    [Authorize]
    [Route("api/academicYears")]
    public class AcademicYearController : BaseApiController
    {
        public AcademicYearController(IUserService userService, IAcademicYearService academicYearService,
            IRolePermissionsCache rolePermissionsCache) : base(userService, academicYearService, rolePermissionsCache)
        {

        }
    }
}
