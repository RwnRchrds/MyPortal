using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Logic.Dictionaries;

namespace MyPortalCore.Areas.Staff.Controllers
{
    [Area("Staff")]
    [Authorize(Policy = PolicyDictionary.UserType.Staff)]
    public class StudentController : Controller
    {
        
    }
}