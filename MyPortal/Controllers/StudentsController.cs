using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MyPortal.Models;
using MyPortal.ViewModels;

namespace MyPortal.Controllers
{
    //MyPortal Students Controller --> Controller methods for Student areas
    [System.Web.Mvc.Authorize(Roles = "Student")]
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

        // Student Landing Page
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();

            var student = _context.Students.SingleOrDefault(s => s.UserId == userId);

            if (student == null)
                return View("~/Views/Students/NoProfileIndex.cshtml");

            var logs = _context.Logs.Where(l => l.StudentId == student.Id).OrderByDescending(x => x.Date).ToList();

            var results = _context.Results.Where(r => r.StudentId == student.Id && r.ResultSet.IsCurrent)
                .ToList();

            var upperSchool = student.YearGroupId == 11 || student.YearGroupId == 10;

            var viewModel = new StudentDetailsViewModel
            {
                Logs = logs,
                Student = student,
                Results = results,
                IsUpperSchool = upperSchool
            };
            return View(viewModel);
        }

        //MyResults Page
        [System.Web.Mvc.Route("Students/Results")]
        public ActionResult Results()
        {
            var userId = User.Identity.GetUserId();

            var student = _context.Students.SingleOrDefault(s => s.UserId == userId);

            if (student == null)
                return HttpNotFound();

            var resultSets = _context.ResultSets.ToList();

            var currentResultSet = _context.ResultSets.Single(x => x.IsCurrent);

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

        //Store Page
        [System.Web.Mvc.Route("Students/Store")]
        public ActionResult Store()
        {
            var userId = User.Identity.GetUserId();

            var studentInDb = _context.Students.SingleOrDefault(s => s.UserId == userId);

            if (studentInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var viewModel = new StudentStoreViewModel
            {
                Student = studentInDb
            };

            return View(viewModel);
        }

        //Sales History
        [System.Web.Mvc.Route("Students/SalesHistory")]
        public ActionResult SalesHistory()
        {
            var userId = User.Identity.GetUserId();

            var studentInDb = _context.Students.SingleOrDefault(s => s.UserId == userId);

            if (studentInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var viewModel = new StudentSalesHistoryViewModel
            {
                Student = studentInDb
            };

            return View(viewModel);
        }
    }
}