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
        private MyPortalDbContext _context;

        public StaffController()
        {
            _context = new MyPortalDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Students()
        {
            var students = _context.Students.ToList();
            return View(students);
        }

        public ActionResult Staff()
        {
            var staff = _context.Staff.ToList();
            return View(staff);
        }       

        [Route("Staff/Students/{id}")]
        public ActionResult StudentDetails(int id)
        {
            var student = _context.Students.SingleOrDefault(s => s.Id == id);

            if (student == null)
                return HttpNotFound();

            return View(student);
        }

        [Route("Staff/Staff/{id}")]
        public ActionResult StaffDetails(string id)
        {
            var staff = _context.Staff.SingleOrDefault(s => s.Id == id);

            if (staff == null)
                return HttpNotFound();

            return View(staff);
        }
    }
}