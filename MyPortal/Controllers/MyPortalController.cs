using System;
using System.Net;
using System.Web.Mvc;
using MyPortal.Models.Database;
using MyPortal.Exceptions;
using MyPortal.Interfaces;
using MyPortal.Models.Misc;
using MyPortal.Persistence;

namespace MyPortal.Controllers
{
    [System.Web.Mvc.Authorize]
    public abstract class MyPortalController : Controller
    {
        protected readonly IUnitOfWork UnitOfWork;

        public MyPortalController()
        {
            UnitOfWork = new UnitOfWork(new MyPortalDbContext());
        }

        public MyPortalController(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        protected override void Dispose(bool disposing)
        {
            UnitOfWork.Dispose();
        }

        protected ActionResult NoAcademicYear()
        {
            return View("~/Views/Error/NoAcademicYear.cshtml");
        }
    }
}