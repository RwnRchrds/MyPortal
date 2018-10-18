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
            var currentUser = int.Parse(User.Identity.GetUserId());

            var student = _context.Students.SingleOrDefault(s => s.Id == currentUser);

            if (student == null)
                return View("~/Views/Students/NoProfileIndex.cshtml");

            var logs = _context.Logs.Where(l => l.StudentId == currentUser).OrderByDescending(x => x.Date).ToList();

            var results = _context.Results.Where(r => r.StudentId == currentUser && r.ResultSet.IsCurrent)
                .ToList();

            var upperSchool = student.YearGroupId == 11 || student.YearGroupId == 10;

            var chartData = StaffController.GetChartData(results, upperSchool);

            var viewModel = new StudentDetailsViewModel
            {
                Logs = logs,
                Student = student,
                Results = results,
                IsUpperSchool = upperSchool,
                ChartData = chartData
            };
            return View(viewModel);
        }

        //MyResults Page
        [System.Web.Mvc.Route("Students/Results")]
        public ActionResult Results()
        {
            var currentUser = int.Parse(User.Identity.GetUserId());

            var student = _context.Students.SingleOrDefault(s => s.Id == currentUser);

            if (student == null)
                return HttpNotFound();

            var resultSets = _context.ResultSets.ToList();

            var currentResultSetId = _context.ResultSets.SingleOrDefault(r => r.IsCurrent).Id;

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
            var currentUser = int.Parse(User.Identity.GetUserId());

            var studentInDb = _context.Students.SingleOrDefault(x => x.Id == currentUser);

            if (studentInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var viewModel = new StudentStoreViewModel
            {
                Student = studentInDb
            };

            return View(viewModel);
        }
    }
}