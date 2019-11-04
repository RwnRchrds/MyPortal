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
        protected readonly IUnitOfWork _unitOfWork;

        public MyPortalController()
        {
            _unitOfWork = new UnitOfWork(new MyPortalDbContext());
        }

        public MyPortalController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        protected override void Dispose(bool disposing)
        {
            _unitOfWork.Dispose();
        }

        protected ActionResult NoAcademicYear()
        {
            return View("~/Views/Error/NoAcademicYear.cshtml");
        }
    }
}