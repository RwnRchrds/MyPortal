using System;
using System.Net;
using System.Web.Mvc;
using MyPortal.Models.Database;
using MyPortal.Interfaces;
using MyPortal.Persistence;

namespace MyPortal.Controllers
{
    [Authorize]
    public abstract class MyPortalController : Controller
    {
        protected readonly IUnitOfWork UnitOfWork;

        protected MyPortalController()
        {
            UnitOfWork = new UnitOfWork(new MyPortalDbContext());
        }

        protected MyPortalController(IUnitOfWork unitOfWork)
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