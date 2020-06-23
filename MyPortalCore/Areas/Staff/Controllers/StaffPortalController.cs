using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;
using MyPortal.Logic.Constants;
using MyPortal.Logic.Extensions;
using MyPortal.Logic.Interfaces;
using MyPortalCore.Controllers;

namespace MyPortalCore.Areas.Staff.Controllers
{
    [Area("Staff")]
    [Route("[area]/[controller]")]
    [Authorize(Policy = Policies.UserType.Staff)]
    public abstract class StaffPortalController : BaseController
    {
        public StaffPortalController(IApplicationUserService userService) : base(userService)
        {
            
        }
    }
}