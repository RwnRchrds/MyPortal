using System;
using System.Net;
using System.Web.Mvc;
using MyPortal.Models.Database;
using MyPortal.Interfaces;
using MyPortal.Persistence;
using MyPortal.Services;

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