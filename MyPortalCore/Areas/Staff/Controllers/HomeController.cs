using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Logic.Dictionaries;
using MyPortalCore.Areas.Staff.ViewModels.Home;

namespace MyPortalCore.Areas.Staff.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            var viewModel = new HomepageViewModel();
            return View(viewModel);
        }
    }
}