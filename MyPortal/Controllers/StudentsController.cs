using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MyPortal.Models;
using MyPortal.ViewModels;

namespace MyPortal.Controllers
{
    [Authorize(Roles = "Student")]
    public class StudentsController : Controller
    {
        private MyPortalDbContext _context;

        public StudentsController()
        {
            _context = new MyPortalDbContext();
        }

        // Student Landing Page
        public ActionResult Index()
        {
            var currentUser = Int32.Parse(User.Identity.GetUserId());

            var student = _context.Students.SingleOrDefault(s => s.Id == currentUser);

            if (student == null)
                return HttpNotFound();

            var logs = _context.Logs.Where(l => l.Student == currentUser).OrderByDescending(x => x.Date).ToList();

            var results = _context.Results.Where(r => r.Student == currentUser && r.ResultSet1.IsCurrent == true)
                .ToList();

            bool upperSchool = student.YearGroup == 11 || student.YearGroup == 10;

            var chartData = StaffController.GetChartData(results, upperSchool);

            var viewModel = new StudentDetailsViewModel
            {
                Logs = logs,
                Student = student,
                Results = results,
                IsUpperSchool = upperSchool,
                ChartData = chartData,
            };
            return View(viewModel);
        }

        //MyResults Page
        [Route("Students/MyResults")]
        public ActionResult Results()
        {
            var currentUser = Int32.Parse(User.Identity.GetUserId());

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

    }
}