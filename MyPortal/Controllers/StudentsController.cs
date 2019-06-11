﻿using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MyPortal.Models;
using MyPortal.Models.Database;
using MyPortal.ViewModels;

namespace MyPortal.Controllers
{
    //MyPortal Students Controller --> Controller methods for Student areas
    [System.Web.Mvc.Authorize(Roles = "Student")]
    [System.Web.Mvc.RoutePrefix("Students")]
    public class StudentsController : Controller
    {
        private readonly MyPortalDbContext _context;

        public StudentsController()
        {
            _context = new MyPortalDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) _context?.Dispose();

            base.Dispose(disposing);
        }

        #region Store

        //Sales History
        [System.Web.Mvc.Route("Store/SalesHistory")]
        public ActionResult SalesHistory()
        {
            var userId = User.Identity.GetUserId();

            var studentInDb = _context.Students.SingleOrDefault(s => s.Person.UserId == userId);

            if (studentInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var viewModel = new StudentSalesHistoryViewModel
            {
                Student = studentInDb
            };

            return View("~/Views/Students/Store/SalesHistory.cshtml", viewModel);
        }

        //Store Page
        [System.Web.Mvc.Route("Store/Store")]
        public ActionResult Store()
        {
            var userId = User.Identity.GetUserId();

            var studentInDb = _context.Students.SingleOrDefault(s => s.Person.UserId == userId);

            if (studentInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var viewModel = new StudentStoreViewModel
            {
                Student = studentInDb
            };

            return View("~/Views/Students/Store/Store.cshtml", viewModel);
        }

        #endregion

        // Student Landing Page
        [System.Web.Mvc.Route("Home")]
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();

            var student = _context.Students.SingleOrDefault(s => s.Person.UserId == userId);

            if (student == null)
                return View("~/Views/Students/NoProfileIndex.cshtml");            

            var results = _context.AssessmentResults.Where(r => r.StudentId == student.Id && r.AssessmentResultSet.IsCurrent)
                .ToList();            

            var viewModel = new StudentDetailsViewModel
            {                
                Student = student,
                Results = results,                
            };
            return View(viewModel);
        }

        //MyResults Page
        [System.Web.Mvc.Route("Results")]
        public ActionResult Results()
        {
            var userId = User.Identity.GetUserId();

            var student = _context.Students.SingleOrDefault(s => s.Person.UserId == userId);

            if (student == null)
                return HttpNotFound();

            var resultSets = _context.AssessmentResultSets.ToList();

            var currentResultSet = _context.AssessmentResultSets.Single(x => x.IsCurrent);

            if (currentResultSet == null)
                return Content("No result sets exist in database");

            var currentResultSetId = currentResultSet.Id;

            var viewModel = new StudentResultsViewModel
            {
                Student = student,
                ResultSets = resultSets,
                CurrentResultSetId = currentResultSetId
            };

            return View(viewModel);
        }

    }
}