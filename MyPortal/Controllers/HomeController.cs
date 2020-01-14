using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MyPortal.BusinessLogic.Models.Identity;
using MyPortal.BusinessLogic.Services.Identity;

namespace MyPortal.Controllers
{
    [RoutePrefix("Home")]
    public class HomeController : MyPortalController
    {
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
            if (User != null && User.Identity.IsAuthenticated)
                return RedirectToAction("Home", "Home");
            return RedirectToAction("Login", "Account");
        }
    }
}