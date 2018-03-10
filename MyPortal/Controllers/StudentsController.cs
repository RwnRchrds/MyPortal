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

        // GET: Students
        public ActionResult Index()
        {
            var currentUser = Int32.Parse(User.Identity.GetUserId());

            var student = _context.Students.SingleOrDefault(s => s.Id == currentUser);

            if (student == null)
                return HttpNotFound();

            var logs = _context.Logs.Where(l => l.Student == currentUser).ToList();

            var results = _context.Results.Where(r => r.Student == currentUser && r.ResultSet1.IsCurrent == true).ToList();

            var logTypes = _context.LogTypes.ToList();

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
        // StudentsController --> "Random" ActionResult
        public ActionResult Random()
        {
            var student = new Student() {Id = 1, FirstName = "Aaron", LastName = "Aardvark"};
            var results = new List<Result>
            {
                new Result {ResultSet = 1, Student = 1, Subject = 1, Value = "A"},
                new Result {ResultSet = 1, Student = 1, Subject = 2, Value = "C"}
            };

            var viewModel = new RandomStudentViewModel
            {
                Results = results,
                Student = student
            };

            return View(viewModel);
        }

        public ActionResult Edit(int id)
        {
            return Content("id:" + id);
        }

        [Route( "Students/Results/{resultset}")]
        public ActionResult Results(int? resultSet)
        {
            if (!resultSet.HasValue)
                resultSet = 1;
            return Content("Results from " + resultSet);
        }
    }
}