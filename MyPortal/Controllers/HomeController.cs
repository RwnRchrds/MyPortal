using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MyPortal.Models;

namespace MyPortal.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {

        private readonly MyPortalDbContext _context;

        public HomeController()
        {
            _context = new MyPortalDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult Index()
        {
            if (System.Web.HttpContext.Current.User != null &&
                System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
                return RedirectToAction("Home", "Home");
            return View();
        }

        [Authorize]
        [Route("User/Home")]
        public ActionResult Home()
        {
            if (User.IsInRole("SeniorStaff") || User.IsInRole("Staff"))
            {
                var userId = User.Identity.GetUserId();
                var userProfile = _context.Staff.Single(x => x.UserId == userId);

                return userProfile == null ? RedirectToAction("NoProfile", "Home") : RedirectToAction("Index", "Staff");
            }

            if (User.IsInRole("Student"))
            {
                var userId = User.Identity.GetUserId();
                var userProfile = _context.Students.Single(x => x.UserId == userId);

                return userProfile == null ? RedirectToAction("NoProfile", "Home") : RedirectToAction("Index", "Staff");
            }

            return RedirectToAction("Login", "Account");
        }

        [Authorize]
        [Route("User/NoProfile")]
        public ActionResult NoProfile()
        {
            if (User.IsInRole("SeniorStaff") || User.IsInRole("Staff"))
            {
                var userId = User.Identity.GetUserId();
                var userProfile = _context.Staff.Single(x => x.UserId == userId);

                return userProfile == null ? RedirectToAction("NoProfile", "Home") : RedirectToAction("Index", "Staff");
            }

            if (User.IsInRole("Student"))
            {
                var userId = User.Identity.GetUserId();
                var userProfile = _context.Students.Single(x => x.UserId == userId);

                return userProfile == null ? RedirectToAction("NoProfile", "Home") : RedirectToAction("Index", "Staff");
            }

            return View();
        }
    }
}