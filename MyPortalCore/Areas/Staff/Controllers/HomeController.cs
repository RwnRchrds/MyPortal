using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Logic.Dictionaries;

namespace MyPortalCore.Areas.Staff.Controllers
{
    [Area("Staff")]
    [Authorize(Policy = PolicyDictionary.UserType.Staff)]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}