using System.Threading.Tasks;
using System.Web.Mvc;
using MyPortal.Models;
using MyPortal.Services;

namespace MyPortal.Controllers
{
    public class HomeController : MyPortalController
    {
        [Route("User/Home", Name = "Home")]
        public async Task<ActionResult> Home()
        {
            if (Request.IsAuthenticated)
            {
                switch (await User.GetUserTypeAsync())
                {
                    case UserType.Staff:
                        return RedirectToAction("Index", "Home", new {area = "Staff"});
                    case UserType.Student:
                        return RedirectToAction("Index", "Home", new {area = "Students"});
                    default:
                        return RedirectToAction("RestrictedAccess", "Account");
                }
            }

            return RedirectToAction("Login", "Account");
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            if (System.Web.HttpContext.Current.User != null &&
                System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
                return RedirectToAction("Home", "Home");
            return View();
        }
    }
}