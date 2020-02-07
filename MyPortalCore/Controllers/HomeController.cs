using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyPortal.Logic.Dictionaries;
using MyPortalCore.Models;

namespace MyPortalCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated && User.HasClaim(ClaimTypeDictionary.UserType, UserTypeDictionary.Staff))
            {
                return RedirectToAction("Index", "Home", new { area = "Staff" });
            }

            else if (User.Identity.IsAuthenticated && User.HasClaim(ClaimTypeDictionary.UserType, UserTypeDictionary.Student))
            {
                // TODO: Enable Student Portal
                // return RedirectToAction("Index", "Home", new { area = "Students" });
            }

            else if (User.Identity.IsAuthenticated && User.HasClaim(ClaimTypeDictionary.UserType, UserTypeDictionary.Parent))
            {
                // TODO: Enable Parent Portal
                // return RedirectToAction("Index", "Home", new { area = "Parents" });
            }

            return Redirect("/Login");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
