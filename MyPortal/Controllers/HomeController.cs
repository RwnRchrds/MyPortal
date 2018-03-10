using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPortal.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if ((System.Web.HttpContext.Current.User != null) &&
                System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Home", "Home");
            }
            return View();
        }

        [Authorize]
        [Route("User/Home")]
        public ActionResult Home()
        {
            if (User.IsInRole("SeniorStaff") || User.IsInRole("Staff"))
            {
                return RedirectToAction("Index", "Staff");
            }

            if (User.IsInRole("Student"))
            {
                return RedirectToAction("Index", "Students");
            }            

            return RedirectToAction("Login","Account");
        }
    }
}