using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Logic.Dictionaries;
using MyPortal.Logic.Interfaces;

namespace MyPortalCore.Areas.Staff.Controllers
{
    [Area("Staff")]
    [Route("[area]/[controller]")]
    [Authorize(Policy = PolicyDictionary.UserType.Staff)]
    public class BaseController : Controller
    {
        protected readonly IApplicationUserService _userService;

        public BaseController(IApplicationUserService userService)
        {
            _userService = userService;
        }
    }
}