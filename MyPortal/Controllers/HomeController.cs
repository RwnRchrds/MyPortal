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
                RedirectToAction("Index", "Staff");
            }

            if (User.IsInRole("Student"))
            {
                RedirectToAction("Index", "Students");
            }

            return RedirectToAction("Login", "Account");
        }

    }
}