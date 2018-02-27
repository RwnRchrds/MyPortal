using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyPortal.Models;
using MyPortal.ViewModels;

namespace MyPortal.Controllers
{
    public class StudentsController : Controller
    {
        // GET: Students
        public ActionResult Index()
        {
            return View();
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