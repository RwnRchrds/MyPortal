using System;
using System.Net;
using System.Web.Mvc;

namespace MyPortal.Controllers
{
    [Authorize]
    public abstract class MyPortalController : Controller
    {
        protected ActionResult NoAcademicYear()
        {
            return View("~/Views/Error/NoAcademicYear.cshtml");
        }
    }
}