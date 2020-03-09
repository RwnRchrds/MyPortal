using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Logic.Dictionaries;

namespace MyPortalCore.Areas.Staff.Controllers
{
    [Area("Staff")]
    [Route("[area]/[controller]")]
    [Authorize(Policy = PolicyDictionary.UserType.Staff)]
    public class BaseController : Controller
    {
        
    }
}