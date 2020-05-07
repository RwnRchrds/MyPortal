using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Logic.Constants;
using MyPortal.Logic.Interfaces;

namespace MyPortalCore.Areas.Staff.Controllers
{
    [Area("Staff")]
    [Route("[area]/[controller]")]
    [Authorize(Policy = Policies.UserType.Staff)]
    public class BaseController : Controller
    {
        protected readonly IApplicationUserService _userService;

        public BaseController(IApplicationUserService userService)
        {
            _userService = userService;
        }
    }
}