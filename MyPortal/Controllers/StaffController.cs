using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using MyPortal.Models;

namespace MyPortal.Controllers
{
    public class StaffController : Controller
    {
        // GET: Staff
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Students()
        {
            var students = GetStudents();
            return View();
        }

        private IEnumerable<Student> GetStudents()
        {
            return new List<Student>
            {
                new Student {Id = 1,FirstName = "John", LastName = "Aburn"},
                new Student {Id = 2,FirstName = "Calum", LastName = "Worthy"}
            };
        }

        [Route("staff/students/{student}")]
        public ActionResult StudentDetails(int id)
        {
            var student = GetStudents().SingleOrDefault(s => s.Id == id);

            if (student == null)
                return HttpNotFound();

            return View(student);
        }
    }
}