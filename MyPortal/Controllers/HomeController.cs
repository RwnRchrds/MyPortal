using System.Web.Mvc;
using MyPortal.Processes;

namespace MyPortal.Controllers
{
    [AllowAnonymous]
    public class HomeController : MyPortalController
    {
        [Authorize]
        [Route("User/Home")]
        public ActionResult Home()
        {
            if (Request.IsAuthenticated)
            {
                if (User.HasPermission("AccessStaffPortal")) return RedirectToAction("Index", "Staff");

                if (User.HasPermission("AccessStudentPortal")) return RedirectToAction("Index", "Students");

                return RedirectToAction("RestrictedAccess", "Account");
            }

            return RedirectToAction("Login", "Account");
        }

        public ActionResult Index()
        {
            if (System.Web.HttpContext.Current.User != null &&
                System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
                return RedirectToAction("Home", "Home");
            return View();
        }
    }
}