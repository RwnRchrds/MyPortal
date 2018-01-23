using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyPortal.Models;

namespace MyPortal.Controllers
{
    public class StudentsController : Controller
    {
        // GET: Students
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Random()
        {
            var student = new Student() {Id = 1, FirstName = "Aaron", LastName = "Aardvark"};
            return View(student);
        }

        public ActionResult Edit(int id)
        {
            return Content("id:" + id);
        }

        [Route( "students/results/{resultset}")]
        public ActionResult Results(int? resultSet)
        {
            if (!resultSet.HasValue)
                resultSet = 1;
            return Content("Results from " + resultSet);
        }
    }
}