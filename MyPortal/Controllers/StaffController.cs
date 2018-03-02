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
            return View(students);
        }

        public ActionResult Staff()
        {
            var staff = GetStaff();
            return View(staff);
        }

        private IEnumerable<Student> GetStudents()
        {
            return new List<Student>
            {
                new Student {Id = 1,FirstName = "John", LastName = "Aburn",YearGroup = "Year 7",RegGroup = "7V"},
                new Student {Id = 2,FirstName = "Calum", LastName = "Worthy",YearGroup = "Year 11",RegGroup = "11A"},
                new Student {Id = 3,FirstName = "Haymitch",LastName = "Abernathy",YearGroup = "Year 5",RegGroup = "5S"}
            };     
        }

        private IEnumerable<Staff> GetStaff()
        {
            return new List<Staff>
            {
                new Staff {Id = "GAL",Title = "Mrs",FirstName = "Georgia",LastName = "Alibi"},
                new Staff {Id = "LSP",Title = "Mrs",FirstName = "Lily",LastName = "Sprague"}
            };
        }

        [Route("Staff/Students/{id}")]
        public ActionResult StudentDetails(int id)
        {
            var student = GetStudents().SingleOrDefault(s => s.Id == id);

            if (student == null)
                return HttpNotFound();

            return View(student);
        }

        [Route("Staff/Staff/{id}")]
        public ActionResult StaffDetails(string id)
        {
            var staff = GetStaff().SingleOrDefault(s => s.Id == id);

            if (staff == null)
                return HttpNotFound();

            return View(staff);
        }
    }
}